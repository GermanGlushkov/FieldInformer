// XMLCellset.h : Declaration of the CXMLCellset

#ifndef __XMLCELLSET_H_
#define __XMLCELLSET_H_

#include "resource.h"       // main symbols
#include <mtx.h>



//#import "C:\Documents and Settings\Administrator\My Documents\Visual Studio Projects\FI_CS_new\XMLAnalysisConPoolWrapper\ADOConnPoolWrapper.dll"
//#import "..\XMLCellset\FIXMLCellset.dll" // raw_interfaces_only
//#import "C:\Program Files\Common Files\System\ado\msado15.dll" \
  // rename( "EOF", "adoEOF" )


//using namespace ADOConnPoolWrapper;


/////////////////////////////////////////////////////////////////////////////
// CXMLCellset
class ATL_NO_VTABLE CXMLCellset : 
	public CComObjectRootEx<CComMultiThreadModel>,	//<CComSingleThreadModel>,
	public CComCoClass<CXMLCellset, &CLSID_XMLCellset>,
	public IObjectControl,
	public IDispatchImpl<IXMLCellset, &IID_IXMLCellset, &LIBID_XMLCELLSETWRAPPERLib>
{
public:
	CXMLCellset()
	{
		Cellset.CreateInstance("FIXMLCellset.XMLCellsetClass");
		//ADOConnPool.CreateInstance("ADOConnPoolWrapper.ADOCOnnPool");
		m_counter=0;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_XMLCELLSET)

DECLARE_PROTECT_FINAL_CONSTRUCT()

//DECLARE_NOT_AGGREGATABLE(CXMLCellset)
DECLARE_AGGREGATABLE(CXMLCellset)

BEGIN_COM_MAP(CXMLCellset)
	COM_INTERFACE_ENTRY(IXMLCellset)
	COM_INTERFACE_ENTRY(IObjectControl)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

// IObjectControl
public:
	STDMETHOD(Activate)();
	STDMETHOD_(BOOL, CanBePooled)();
	STDMETHOD_(void, Deactivate)();

	CComPtr<IObjectContext> m_spObjectContext;

// IXMLCellset
public:
	STDMETHOD(BuildCellset)(/*[in]*/ BSTR Mdx , /*[in]*/ _ConnectionPtr pCon, /*[in]*/ BSTR OutFilePath);
	STDMETHOD(Increment)(/*[out]*/ int *outval);
private:
	FIXMLCellset::_XMLCellsetClassPtr Cellset;
	//ADOConnPoolWrapper::_ADOConnPoolPtr ADOConnPool;
	int m_counter;
};

#endif //__XMLCELLSET_H_
