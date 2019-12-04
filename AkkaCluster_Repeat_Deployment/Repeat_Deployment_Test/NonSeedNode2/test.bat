:loop
SET /a a+=1
IF %a%==30 GOTO break

TASKKILL /im NonSeedNode2.exe /t

TIMEOUT 5

SET CURDIR="%~dp0%"

START /d %CURDIR%bin\Release\ NonSeedNode2.exe

TIMEOUT 5

GOTO loop

:break

PAUSE
