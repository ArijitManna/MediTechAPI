# MediTech API - Management Scripts

This directory contains utility scripts to manage the MediTech Backend API application.

## Available Scripts

### 🛑 Kill Processes Scripts

**For Windows:**
```cmd
kill-processes.bat
```

**For macOS/Linux:**
```bash
./kill-processes.sh
```

**What it does:**
- Kills all processes running on ports 5094, 5000, 5001, 7000, 8000
- Terminates all `dotnet` processes
- Terminates all `MediTechBackendAPI` processes
- Provides detailed feedback on each operation
- Force kills any remaining stubborn processes

### 🚀 Start API Scripts

**For Windows:**
```cmd
start-api.bat
```

**For macOS/Linux:**
```bash
./start-api.sh
```

**What it does:**
- Navigates to the correct project directory
- Builds the application
- Starts the API server
- Shows the application URL (http://localhost:5094)

## Usage Examples

### Quick Kill and Restart Workflow

**On macOS/Linux:**
```bash
# Kill all processes
./kill-processes.sh

# Wait a moment
sleep 2

# Start the API
./start-api.sh
```

**On Windows:**
```cmd
REM Kill all processes
kill-processes.bat

REM Start the API
start-api.bat
```

### Testing the Kill Script

Since you're on macOS, test the kill script:
```bash
./kill-processes.sh
```

### Starting the API

```bash
./start-api.sh
```

## Script Locations

All scripts are located in:
```
/Users/arijitmanna/Documents/Meditech/API/MediTechAPI/MediTechBackendAPI/
```

## Troubleshooting

1. **Permission Denied (macOS/Linux):**
   ```bash
   chmod +x kill-processes.sh
   chmod +x start-api.sh
   ```

2. **Script Not Found:**
   - Make sure you're in the correct directory
   - Use `./script-name.sh` instead of just `script-name.sh`

3. **Ports Still Occupied:**
   - Run the kill script twice if needed
   - Check for any IDEs or other applications using the ports
   - Restart your terminal/command prompt

## Features

- ✅ Cross-platform compatibility (Windows/macOS/Linux)
- ✅ Detailed process information and feedback
- ✅ Force kill stubborn processes
- ✅ Automatic build before start
- ✅ Error handling and validation
- ✅ Color-coded output for better readability

## Notes

- The scripts target common .NET development ports
- Always run the kill script before starting if you encounter port conflicts
- The start script will automatically build the project before running
- Press `Ctrl+C` to stop the running API