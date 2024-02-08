rmdir /S /Q %1temp
mkdir %1temp
set ARC=binaries-for-testing.tar
del %1%ARC%
set SRC=%2
set IS_64=%SRC:~-12,-9%
if %IS_64% == x64 (
set PGM=%2%364.exe
xcopy %2*.dll %1temp
xcopy %4 %1temp
xcopy %1packages\Microsoft.Web.WebView2.1.0.2210.55\runtimes\win-x64\native\WebView2Loader.dll %1temp
xcopy %userprofile%\Downloads\macro-src.html %1temp
cd %1
tar -z -cf %1%ARC%  temp
) else (
set PGM=%2%332.exe
)
cd ..
rmdir /S /Q %1temp