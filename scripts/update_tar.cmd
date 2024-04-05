tar -xf  ..\binaries-for-testing.tar
copy /y ..\bin\x64\Release\*.txt temp
tar -z -cf ..\binaries-for-testing.tar temp
rmdir /s temp