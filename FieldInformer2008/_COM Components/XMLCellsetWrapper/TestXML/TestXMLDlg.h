// TestXMLDlg.h : header file
//

#if !defined(AFX_TESTXMLDLG_H__F7AEC8C6_1BD3_4470_B022_DAF3311F98AA__INCLUDED_)
#define AFX_TESTXMLDLG_H__F7AEC8C6_1BD3_4470_B022_DAF3311F98AA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CTestXMLDlgAutoProxy;

/////////////////////////////////////////////////////////////////////////////
// CTestXMLDlg dialog

class CTestXMLDlg : public CDialog
{
	DECLARE_DYNAMIC(CTestXMLDlg);
	friend class CTestXMLDlgAutoProxy;

// Construction
public:
	CTestXMLDlg(CWnd* pParent = NULL);	// standard constructor
	virtual ~CTestXMLDlg();

// Dialog Data
	//{{AFX_DATA(CTestXMLDlg)
	enum { IDD = IDD_TESTXML_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestXMLDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	CTestXMLDlgAutoProxy* m_pAutoProxy;
	HICON m_hIcon;

	BOOL CanExit();

	// Generated message map functions
	//{{AFX_MSG(CTestXMLDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnClose();
	virtual void OnOK();
	virtual void OnCancel();
	afx_msg void OnButton1();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTXMLDLG_H__F7AEC8C6_1BD3_4470_B022_DAF3311F98AA__INCLUDED_)
