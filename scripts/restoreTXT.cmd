del /q ..\bin\x64\release\*macros.txt
xcopy /y ..\bin\x64\debug\*macros.txt ..\bin\x64\release\*macros.txt
copy /y ..\bin\x64\debug\signatures.txt ..\bin\x64\release\signatures.txt
xcopy /y /I ..\bin\x64\debug\*.png ..\bin\x64\release\
xcopy /y /I ..\bin\x64\debug\*.rtf ..\bin\x64\release\
