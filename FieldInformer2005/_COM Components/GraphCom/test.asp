<%
Set g_gc=Server.CreateObject( "GraphCom.Graph" )
'''''''''''''''''''''''''''''''''''''''
nS=Clng(4)
nP=Clng(3)

  ReDim DataV(nS * nP) 
  ReDim sLegArray(nS) 
  ReDim sLabelArray(nP)
  For j = 0 To nS - 1
    For i = 0 To nP - 1
      If (j = 0) Then
        sLabs = sLabs & "lab" & CStr(i + 1) & Chr(9)
        sLabelArray(i) = "Lab_A" & CStr(i)
      End If
     DataV(i * nS + j) = CDbl(Int((100 - 0 + 1) * Rnd + 0))
    Next 
    sLegs = sLegs & "leg " & CStr(j + 1) & Chr(9)
    sLegArray(j) = "Leg_f_A" & CStr(j + 1)
Next 

  g_gc.setValuesVAR Clng(nS), Clng(nP), DataV(0)
  g_gc.setLegendArrayVAR Clng(nS), sLegArray(0)
  g_gc.setLabelArrayVAR Clng(nP), sLabelArray(0)
on error resume next
  g_gc.WriteFileVAR "C:\Inetpub\wwwroot\FieldInformer.NET\COM\GraphCom\test.jpg", Clng(1)

response.write err.description
Set g_gc=Nothing

Response.write "donE"

%>