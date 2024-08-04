mkdir %1temp
set ARC=binaries-for-testing.tar
del %1%ARC%
set SRC=%2
set IS_64=%SRC:~-12,-9%
if %IS_64% == x64 (
set PGM=%2%364.exe
xcopy %2*agil*.dll %1temp
xcopy %2*macros.txt %1temp
xcopy %2BiosSimulators.txt %1temp
xcopy %2LOCALIMAGEFILE-*.png %1temp
xcopy /Y /I %2*macros.txt %2..\Debug
replace /U /S %2..\Release\*.rtf %1sources
replace /U /S %2..\Release\*.html %1sources
xcopy /Y /I %1sources\*.rtf %2..\Release
xcopy /Y /I %1sources\*.rtf %2..\Debug
xcopy /Y /I %1sources\*.rtf %1temp
xcopy /Y /I %1sources\SiteMap.html %2..\Release
xcopy /Y /I %1sources\SiteMap.html %2..\Debug
xcopy %1sources\*.html %1temp
xcopy %2signatures.txt %1temp
xcopy /Y %2signatures.txt %2..\Debug
xcopy /Y %2Macro*.txt %2..\Debug
xcopy /Y %2BiosSimulators.txt %2..\Debug
xcopy %4 %1temp
xcopy %userprofile%\Downloads\macros.html %1temp
cd %1
tar -z -cf %1%ARC%  temp
) else (
set PGM=%2%332.exe
)
cd ..
rmdir /S /Q %1temp