rmdir /S /Q %1temp
replace /U /S %1sources\*.rtf %2..\Release
replace /U /S %1sources\*.rtf %2..\Debug
replace /U /S %1sources\*.html %2..\Release
replace /U /S %1sources\*.html %2..\Debug