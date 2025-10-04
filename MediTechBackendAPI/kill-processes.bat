@echo off
echo ============================================
echo    MediTech API - Kill Running Processes
echo ============================================
echo.

echo Checking for processes on common ports...
echo.

REM Kill processes on port 5094 (main API port)
echo Killing processes on port 5094...
for /f "tokens=5" %%a in ('netstat -ano ^| findstr :5094') do (
    echo Found process with PID: %%a
    taskkill /PID %%a /F >nul 2>&1
    if !errorlevel! == 0 (
        echo Successfully killed process %%a
    ) else (
        echo Failed to kill process %%a or process not found
    )
)

REM Kill processes on port 5000 (default ASP.NET port)
echo Killing processes on port 5000...
for /f "tokens=5" %%a in ('netstat -ano ^| findstr :5000') do (
    echo Found process with PID: %%a
    taskkill /PID %%a /F >nul 2>&1
    if !errorlevel! == 0 (
        echo Successfully killed process %%a
    ) else (
        echo Failed to kill process %%a or process not found
    )
)

REM Kill processes on port 5001 (default HTTPS port)
echo Killing processes on port 5001...
for /f "tokens=5" %%a in ('netstat -ano ^| findstr :5001') do (
    echo Found process with PID: %%a
    taskkill /PID %%a /F >nul 2>&1
    if !errorlevel! == 0 (
        echo Successfully killed process %%a
    ) else (
        echo Failed to kill process %%a or process not found
    )
)

REM Kill all dotnet processes
echo.
echo Killing all dotnet processes...
tasklist | findstr /I "dotnet.exe" >nul 2>&1
if !errorlevel! == 0 (
    taskkill /IM "dotnet.exe" /F >nul 2>&1
    echo All dotnet processes killed
) else (
    echo No dotnet processes found
)

REM Kill all MediTechBackendAPI processes
echo Killing all MediTechBackendAPI processes...
tasklist | findstr /I "MediTechBackendAPI" >nul 2>&1
if !errorlevel! == 0 (
    taskkill /IM "MediTechBackendAPI*" /F >nul 2>&1
    echo All MediTechBackendAPI processes killed
) else (
    echo No MediTechBackendAPI processes found
)

echo.
echo ============================================
echo    All processes have been terminated
echo ============================================
echo.
pause