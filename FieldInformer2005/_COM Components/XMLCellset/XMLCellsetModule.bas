Attribute VB_Name = "XMLCellsetModule"
Option Explicit

Private m_csActiveCellset As ADOMD.Cellset



Sub Main()


    Dim dicCmdLineParams As New Dictionary
    Dim varDicItem As Variant
    Dim varDicItem1 As Variant
    Dim i As Integer
    Dim j As Integer

    Dim strCommand As String
    Dim iStartPosition As Integer
    Dim iEndPosition As Integer
    Dim iTempEndPosition As Integer


    strCommand = Command()

    '--- getting expected parameters from command line ---
    dicCmdLineParams.Add "/outpath", ""
    dicCmdLineParams.Add "/connstring", ""
    dicCmdLineParams.Add "/mdx", ""

    For Each varDicItem In dicCmdLineParams
        iStartPosition = InStr(1, strCommand, varDicItem)
        If iStartPosition <> 0 Then
            iStartPosition = iStartPosition + Len(varDicItem)
            iEndPosition = 0
            For Each varDicItem1 In dicCmdLineParams
                iTempEndPosition = InStr(iStartPosition, strCommand, varDicItem1)
                If varDicItem <> varDicItem1 And iStartPosition < iTempEndPosition And (iTempEndPosition < iEndPosition Or iEndPosition = 0) Then
                    iEndPosition = iTempEndPosition
                End If
            Next
            If iEndPosition > iStartPosition Then
                dicCmdLineParams.Item(varDicItem) = Trim(Mid(strCommand, iStartPosition, iEndPosition - iStartPosition))
            Else
                dicCmdLineParams.Item(varDicItem) = Trim(Mid(strCommand, iStartPosition, Len(strCommand) - iStartPosition + 1))
            End If
        End If
    Next

    '--------------------------------------------------------

    Dim conn As New ADODB.Connection
    Set m_csActiveCellset = New ADOMD.Cellset
    
    conn.Open dicCmdLineParams.Item("/connstring")
    m_csActiveCellset.Open dicCmdLineParams.Item("/mdx"), conn
    BuildXML (dicCmdLineParams.Item("/outpath"))
    m_csActiveCellset.Close
    Set m_csActiveCellset = Nothing
    
    conn.Close
    Set conn = Nothing





End Sub





Public Sub BuildXML(strOutFilePath As String)


    Dim i As Integer
    Dim j As Integer
    Dim mem As ADOMD.Member
    Dim Axis0 As ADOMD.Axis
    Dim Axis1 As ADOMD.Axis
    Dim curCell As ADOMD.Cell


    Dim iAx0PosCount As Long
    Dim iAx1PosCount As Long
    Dim iAx0MemCount As Integer
    Dim iAx1MemCount As Integer

    Dim stream As New ADODB.stream




    Set Axis0 = m_csActiveCellset.Axes(0)
    Set Axis1 = m_csActiveCellset.Axes(1)

    iAx0PosCount = Axis0.Positions.Count
    iAx0MemCount = Axis0.DimensionCount
    iAx1PosCount = Axis1.Positions.Count
    iAx1MemCount = Axis1.DimensionCount

    'stream.Charset = "ascii"
    stream.Open

    stream.WriteText "<OlapCellsetData xmlns=""http://tempuri.org/OlapCellsetData.xsd"">"


    For i = 0 To iAx0PosCount - 1
        For j = 0 To iAx0MemCount - 1

            Set mem = Axis0.Positions(i).Members(j)

            stream.WriteText "<Mem><Axs>0</Axs><Pos>"
            stream.WriteText i
            stream.WriteText "</Pos><MPos>"
            stream.WriteText j
            stream.WriteText "</MPos><UName>"
            stream.WriteText XMLEscape(mem.UniqueName)
            stream.WriteText "</UName><Name>"
            stream.WriteText XMLEscape(mem.Caption)
            stream.WriteText "</Name><CCnt>"
            stream.WriteText mem.ChildCount
            stream.WriteText "</CCnt><Lvl>"
            stream.WriteText mem.LevelDepth
            stream.WriteText "</Lvl></Mem>"

        Next
    Next

    For i = 0 To iAx1PosCount - 1
        For j = 0 To iAx1MemCount - 1

            Set mem = Axis1.Positions(i).Members(j)

            stream.WriteText "<Mem><Axs>1</Axs><Pos>"
            stream.WriteText i
            stream.WriteText "</Pos><MPos>"
            stream.WriteText j
            stream.WriteText "</MPos><UName>"
            stream.WriteText XMLEscape(mem.UniqueName)
            stream.WriteText "</UName><Name>"
            stream.WriteText XMLEscape(mem.Caption)
            stream.WriteText "</Name><CCnt>"
            stream.WriteText mem.ChildCount
            stream.WriteText "</CCnt><Lvl>"
            stream.WriteText mem.LevelDepth
            stream.WriteText "</Lvl></Mem>"

        Next
    Next


    For i = 0 To iAx0PosCount - 1
        For j = 0 To iAx1PosCount - 1

            Set curCell = m_csActiveCellset(i, j)
            stream.WriteText "<Cl><Ax0>"
            stream.WriteText i
            stream.WriteText "</Ax0><Ax1>"
            stream.WriteText j
            stream.WriteText "</Ax1><Val>"
            stream.WriteText sGetCellValue(curCell)
            stream.WriteText "</Val><FVal>"
            stream.WriteText sGetCellFormattedValue(curCell)
            stream.WriteText "</FVal></Cl>"

        Next j
    Next i

    stream.WriteText "</OlapCellsetData>"

    stream.Position = 0
    stream.SaveToFile strOutFilePath, adSaveCreateOverWrite
    stream.Close

    Set stream = Nothing


End Sub






Private Function XMLEscape(strIn As String) As String
    Dim strOut As String
    strOut = Replace(strIn, "&", "&amp;")
    strOut = Replace(strOut, "'", "&apos;")
    strOut = Replace(strOut, ">", "&gt;")
    strOut = Replace(strOut, "<", "&lt;")
    strOut = Replace(strOut, """", "%22;")
    XMLEscape = strOut
End Function






Private Function sGetCellValue(cl As ADOMD.Cell) As String
    On Error GoTo sGetCellValueErr

    sGetCellValue = cl.Value & ""

    Exit Function
sGetCellValueErr:
    sGetCellValue = "#ERR" '* Value to display in a cell for which an error occured
End Function

Private Function sGetCellFormattedValue(cl As ADOMD.Cell) As String
    On Error GoTo sGetCellValueErr

    sGetCellFormattedValue = cl.FormattedValue & ""

    Exit Function
sGetCellValueErr:
    sGetCellFormattedValue = "#ERR" '* Value to display in a cell for which an error occured
End Function



