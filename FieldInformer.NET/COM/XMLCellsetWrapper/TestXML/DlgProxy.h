// DlgProxy.h : header file
//

#if !defined(AFX_DLGPROXY_H__F1057334_8647_4FD5_A7AA_7DD62BF7D305__INCLUDED_)
#define AFX_DLGPROXY_H__F1057334_8647_4FD5_A7AA_7DD62BF7D305__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CTestXMLDlg;

/////////////////////////////////////////////////////////////////////////////
// CTestXMLDlgAutoProxy command target

class CTestXMLDlgAutoProxy : public CCmdTarget
{
	DECLARE_DYNCREATE(CTestXMLDlgAutoProxy)

	CTestXMLDlgAutoProxy();           // protected constructor used by dynamic creation

// Attributes
public:
	CTestXMLDlg* m_pDialog;

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestXMLDlgAutoProxy)
	public:
	virtual void OnFinalRelease();
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual ~CTestXMLDlgAutoProxy();

	// Generated message map functions
	//{{AFX_MSG(CTestXMLDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
	DECLARE_OLECREATE(CTestXMLDlgAutoProxy)

	// Generated OLE dispatch map functions
	//{{AFX_DISPATCH(CTestXMLDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_DISPATCH
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DLGPROXY_H__F1057334_8647_4FD5_A7AA_7DD62BF7D305__INCLUDED_)
