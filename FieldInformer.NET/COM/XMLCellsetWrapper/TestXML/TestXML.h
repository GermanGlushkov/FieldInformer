// TestXML.h : main header file for the TESTXML application
//

#if !defined(AFX_TESTXML_H__3C38E1D3_786E_4D33_B91D_5C50996050E1__INCLUDED_)
#define AFX_TESTXML_H__3C38E1D3_786E_4D33_B91D_5C50996050E1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CTestXMLApp:
// See TestXML.cpp for the implementation of this class
//

class CTestXMLApp : public CWinApp
{
public:
	CTestXMLApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestXMLApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CTestXMLApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTXML_H__3C38E1D3_786E_4D33_B91D_5C50996050E1__INCLUDED_)
