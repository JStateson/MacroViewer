rmdir /S /Q %1temp
replace /U /S %1sources\*.rtf %2..\Release
replace /U /S %1sources\*.rtf %2..\Debug
replace /U /S %1sources\SiteMap.html %2..\Release
replace /U /S %1sources\SiteMap.html %2..\Debug
xcopy /Y /I %2..\Release\LO*.png %2..\Debug
del %2..\Debug\MacroChanges.txt
xcopy /Y /I %2..\Release\MacroChanges.txt %2..\Debug
cmd c exit 0