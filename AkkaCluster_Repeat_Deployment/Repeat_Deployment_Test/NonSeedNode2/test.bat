:loop
SET /a a+=1
IF %a%==50 GOTO break

TASKKILL /im NonSeedNode2.exe /t

TIMEOUT 3

SET CURDIR="%~dp0%"

START /d %CURDIR%bin\Release\ NonSeedNode2.exe

TIMEOUT 3

GOTO loop

:break

PAUSE
