
XMLCellsetWrapperps.dll: dlldata.obj XMLCellsetWrapper_p.obj XMLCellsetWrapper_i.obj
	link /dll /out:XMLCellsetWrapperps.dll /def:XMLCellsetWrapperps.def /entry:DllMain dlldata.obj XMLCellsetWrapper_p.obj XMLCellsetWrapper_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del XMLCellsetWrapperps.dll
	@del XMLCellsetWrapperps.lib
	@del XMLCellsetWrapperps.exp
	@del dlldata.obj
	@del XMLCellsetWrapper_p.obj
	@del XMLCellsetWrapper_i.obj
