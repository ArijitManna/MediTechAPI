@echo off
echo ============================================
echo      MediTech API - Start Application
echo ============================================
echo.

echo Navigating to project directory...
cd /d "%~dp0MediTechBackendAPI"

echo Building the application...
dotnet build

if %errorlevel% neq 0 (
    echo Build failed! Please check the errors above.
    pause
    exit /b 1
)

echo.
echo Starting the application...
echo Application will be available at: http://localhost:5094
echo Press Ctrl+C to stop the application
echo.

dotnet run

pause