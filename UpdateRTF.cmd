rmdir /S /Q %1temp
replace /U /S %1sources\*.rtf %2..\Release
replace /U /S %1sources\*.rtf %2..\Debug
replace /U /S %1sources\SiteMap.html %2..\Release
replace /U /S %1sources\SiteMap.html %2..\Debug