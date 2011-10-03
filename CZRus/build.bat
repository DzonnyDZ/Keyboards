@echo off
ECHO i386
"c:\Program Files (x86)\Microsoft Keyboard Layout Creator 1.4\bin\i386\kbdutool.exe" -n -x -u %1.klc
move /Y %1.dll %1\i386\%1.dll
ECHO ia64
"c:\Program Files (x86)\Microsoft Keyboard Layout Creator 1.4\bin\i386\kbdutool.exe" -n -i -u %1.klc
move /Y %1.dll %1\ia64\%1.dll
ECHO amd64
"c:\Program Files (x86)\Microsoft Keyboard Layout Creator 1.4\bin\i386\kbdutool.exe" -n -m -u %1.klc
move /Y %1.dll %1\amd64\%1.dll
ECHO wow64
"c:\Program Files (x86)\Microsoft Keyboard Layout Creator 1.4\bin\i386\kbdutool.exe" -n -o -u %1.klc
move /Y %1.dll %1\wow64\%1.dll
ECHO src
"c:\Program Files (x86)\Microsoft Keyboard Layout Creator 1.4\bin\i386\kbdutool.exe" -n -s -u %1.klc