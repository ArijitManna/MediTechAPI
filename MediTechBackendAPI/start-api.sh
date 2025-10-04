#!/bin/bash

echo "============================================"
echo "     MediTech API - Start Application"
echo "============================================"
echo ""

# Get the directory where the script is located
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_DIR="$SCRIPT_DIR/MediTechBackendAPI"

echo "Navigating to project directory: $PROJECT_DIR"
cd "$PROJECT_DIR" || {
    echo "Error: Could not navigate to project directory"
    exit 1
}

echo "Building the application..."
dotnet build

if [ $? -ne 0 ]; then
    echo "Build failed! Please check the errors above."
    exit 1
fi

echo ""
echo "Starting the application..."
echo "Application will be available at: http://localhost:5094"
echo "Press Ctrl+C to stop the application"
echo ""

# Start the application
dotnet run