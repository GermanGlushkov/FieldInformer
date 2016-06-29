// DlgProxy.cpp : implementation file
//

#include "stdafx.h"
#include "TestXML.h"
#include "DlgProxy.h"
#include "TestXMLDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CTestXMLDlgAutoProxy

IMPLEMENT_DYNCREATE(CTestXMLDlgAutoProxy, CCmdTarget)

CTestXMLDlgAutoProxy::CTestXMLDlgAutoProxy()
{
	EnableAutomation();
	
	// To keep the application running as long as an automation 
	//	object is active, the constructor calls AfxOleLockApp.
	AfxOleLockApp();

	// Get access to the dialog through the application's
	//  main window pointer.  Set the proxy's internal pointer
	//  to point to the dialog, and set the dialog's back pointer to
	//  this proxy.
	ASSERT (AfxGetApp()->m_pMainWnd != NULL);
	ASSERT_VALID (AfxGetApp()->m_pMainWnd);
	ASSERT_KINDOF(CTestXMLDlg, AfxGetApp()->m_pMainWnd);
	m_pDialog = (CTestXMLDlg*) AfxGetApp()->m_pMainWnd;
	m_pDialog->m_pAutoProxy = this;
}

CTestXMLDlgAutoProxy::~CTestXMLDlgAutoProxy()
{
	// To terminate the application when all objects created with
	// 	with automation, the destructor calls AfxOleUnlockApp.
	//  Among other things, this will destroy the main dialog
	if (m_pDialog != NULL)
		m_pDialog->m_pAutoProxy = NULL;
	AfxOleUnlockApp();
}

void CTestXMLDlgAutoProxy::OnFinalRelease()
{
	// When the last reference for an automation object is released
	// OnFinalRelease is called.  The base class will automatically
	// deletes the object.  Add additional cleanup required for your
	// object before calling the base class.

	CCmdTarget::OnFinalRelease();
}

BEGIN_MESSAGE_MAP(CTestXMLDlgAutoProxy, CCmdTarget)
	//{{AFX_MSG_MAP(CTestXMLDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

BEGIN_DISPATCH_MAP(CTestXMLDlgAutoProxy, CCmdTarget)
	//{{AFX_DISPATCH_MAP(CTestXMLDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_DISPATCH_MAP
END_DISPATCH_MAP()

// Note: we add support for IID_ITestXML to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .ODL file.

// {21F7F3BE-425C-4239-BC76-3973486A7DBB}
static const IID IID_ITestXML =
{ 0x21f7f3be, 0x425c, 0x4239, { 0xbc, 0x76, 0x39, 0x73, 0x48, 0x6a, 0x7d, 0xbb } };

BEGIN_INTERFACE_MAP(CTestXMLDlgAutoProxy, CCmdTarget)
	INTERFACE_PART(CTestXMLDlgAutoProxy, IID_ITestXML, Dispatch)
END_INTERFACE_MAP()

// The IMPLEMENT_OLECREATE2 macro is defined in StdAfx.h of this project
// {7FB7A18F-752D-430C-B30D-85732E10D4B5}
IMPLEMENT_OLECREATE2(CTestXMLDlgAutoProxy, "TestXML.Application", 0x7fb7a18f, 0x752d, 0x430c, 0xb3, 0xd, 0x85, 0x73, 0x2e, 0x10, 0xd4, 0xb5)

/////////////////////////////////////////////////////////////////////////////
// CTestXMLDlgAutoProxy message handlers
