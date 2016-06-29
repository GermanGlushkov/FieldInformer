

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0361 */
/* at Fri Jun 20 11:43:04 2003
 */
/* Compiler settings for .\XMLCellsetWrapper.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __XMLCellsetWrapper_h__
#define __XMLCellsetWrapper_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IXMLCellset_FWD_DEFINED__
#define __IXMLCellset_FWD_DEFINED__
typedef interface IXMLCellset IXMLCellset;
#endif 	/* __IXMLCellset_FWD_DEFINED__ */


#ifndef __XMLCellset_FWD_DEFINED__
#define __XMLCellset_FWD_DEFINED__

#ifdef __cplusplus
typedef class XMLCellset XMLCellset;
#else
typedef struct XMLCellset XMLCellset;
#endif /* __cplusplus */

#endif 	/* __XMLCellset_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

#ifndef __IXMLCellset_INTERFACE_DEFINED__
#define __IXMLCellset_INTERFACE_DEFINED__

/* interface IXMLCellset */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IXMLCellset;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("4EBC672A-352F-4D0E-96B0-4025E513AC6D")
    IXMLCellset : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Increment( 
            /* [out] */ int *outval) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE BuildCellset( 
            /* [in] */ BSTR Mdx,
            /* [in] */ BSTR ConnString,
            /* [in] */ BSTR OutFilePath) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IXMLCellsetVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IXMLCellset * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IXMLCellset * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IXMLCellset * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IXMLCellset * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IXMLCellset * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IXMLCellset * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IXMLCellset * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Increment )( 
            IXMLCellset * This,
            /* [out] */ int *outval);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *BuildCellset )( 
            IXMLCellset * This,
            /* [in] */ BSTR Mdx,
            /* [in] */ BSTR ConnString,
            /* [in] */ BSTR OutFilePath);
        
        END_INTERFACE
    } IXMLCellsetVtbl;

    interface IXMLCellset
    {
        CONST_VTBL struct IXMLCellsetVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IXMLCellset_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IXMLCellset_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IXMLCellset_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IXMLCellset_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IXMLCellset_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IXMLCellset_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IXMLCellset_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IXMLCellset_Increment(This,outval)	\
    (This)->lpVtbl -> Increment(This,outval)

#define IXMLCellset_BuildCellset(This,Mdx,ConnString,OutFilePath)	\
    (This)->lpVtbl -> BuildCellset(This,Mdx,ConnString,OutFilePath)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IXMLCellset_Increment_Proxy( 
    IXMLCellset * This,
    /* [out] */ int *outval);


void __RPC_STUB IXMLCellset_Increment_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IXMLCellset_BuildCellset_Proxy( 
    IXMLCellset * This,
    /* [in] */ BSTR Mdx,
    /* [in] */ BSTR ConnString,
    /* [in] */ BSTR OutFilePath);


void __RPC_STUB IXMLCellset_BuildCellset_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IXMLCellset_INTERFACE_DEFINED__ */



#ifndef __XMLCELLSETWRAPPERLib_LIBRARY_DEFINED__
#define __XMLCELLSETWRAPPERLib_LIBRARY_DEFINED__

/* library XMLCELLSETWRAPPERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_XMLCELLSETWRAPPERLib;

EXTERN_C const CLSID CLSID_XMLCellset;

#ifdef __cplusplus

class DECLSPEC_UUID("D2456D0D-43CF-4CC1-BBF5-F8347F758990")
XMLCellset;
#endif
#endif /* __XMLCELLSETWRAPPERLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


