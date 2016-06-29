// XMLCellset.cpp : Implementation of CXMLCellset
#include "stdafx.h"
#include "XMLCellsetWrapper.h"
#include "XMLCellset.h"
#include "COMDEF.H"

/////////////////////////////////////////////////////////////////////////////
// CXMLCellset

HRESULT CXMLCellset::Activate()
{
	HRESULT hr = GetObjectContext(&m_spObjectContext);
	if (SUCCEEDED(hr))
		return S_OK;
	return hr;
} 

BOOL CXMLCellset::CanBePooled()
{
	return TRUE;
} 

void CXMLCellset::Deactivate()
{
	m_spObjectContext.Release();
} 



STDMETHODIMP CXMLCellset::Increment(int* outval)
{
	// TODO: Add your implementation code here
	m_counter++;
	*outval=m_counter;
	return S_OK;
}


STDMETHODIMP CXMLCellset::BuildCellset(BSTR Mdx, ADODB::_ConnectionPtr pCon , BSTR OutFilePath)
{
	// TODO: Add your implementation code here
	CComBSTR mdx;
	CComBSTR connstr;
	CComBSTR filepath;
	mdx=Mdx;
	filepath=OutFilePath;

	/*
	HRESULT hr=ADOConnPool.CreateInstance("ADOConnPoolWrapper.ADOCOnnPool");
	if (FAILED(hr))
		return hr;
	*/

	Cellset->BuildCellset(&(mdx.m_str ), (CComBSTR)pCon , &(filepath.m_str ));

	/*
	//IDispatch FAR* pdisp = (IDispatch FAR*)NULL;
	void *pdisp;
	pdisp=ADOConnPool->GetConnection( &bstr_connstr );

	//struct IDispatch *conn=ADOConnPool->GetConnection(&ConnString);

	__try
	{
		//Cellset->BuildCellset(&(Mdx), &(ConnString));
	}
	__finally
	{
		ADOConnPool->ReturnConnection(&pdisp);
	}
	*/

	return S_OK;
}

