#!/bin/bash

echo "============================================"
echo "   MediTech API - Kill Running Processes"
echo "============================================"
echo ""

echo "Checking for processes on common ports..."
echo ""

# Function to kill processes on a specific port
kill_port() {
    local port=$1
    echo "Killing processes on port $port..."
    
    # Find processes using the port
    local pids=$(lsof -ti :$port 2>/dev/null)
    
    if [ -n "$pids" ]; then
        echo "Found processes with PIDs: $pids"
        for pid in $pids; do
            kill -9 $pid 2>/dev/null
            if [ $? -eq 0 ]; then
                echo "Successfully killed process $pid"
            else
                echo "Failed to kill process $pid or process not found"
            fi
        done
    else
        echo "No processes found on port $port"
    fi
    echo ""
}

# Kill processes on common ports
kill_port 5094  # Main API port
kill_port 5000  # Default ASP.NET port
kill_port 5001  # Default HTTPS port
kill_port 7000  # Alternative port
kill_port 8000  # Alternative port

# Kill all dotnet processes
echo "Killing all dotnet processes..."
dotnet_pids=$(pgrep -f "dotnet" 2>/dev/null)

if [ -n "$dotnet_pids" ]; then
    echo "Found dotnet processes with PIDs: $dotnet_pids"
    pkill -f "dotnet" 2>/dev/null
    echo "All dotnet processes killed"
else
    echo "No dotnet processes found"
fi
echo ""

# Kill all MediTechBackendAPI processes
echo "Killing all MediTechBackendAPI processes..."
meditech_pids=$(pgrep -f "MediTech" 2>/dev/null)

if [ -n "$meditech_pids" ]; then
    echo "Found MediTechBackendAPI processes with PIDs: $meditech_pids"
    pkill -f "MediTech" 2>/dev/null
    echo "All MediTechBackendAPI processes killed"
else
    echo "No MediTechBackendAPI processes found"
fi
echo ""

# Final verification
echo "Verifying all processes are terminated..."
remaining_processes=$(lsof -ti :5094,:5000,:5001 2>/dev/null)

if [ -n "$remaining_processes" ]; then
    echo "Warning: Some processes are still running with PIDs: $remaining_processes"
    echo "Force killing remaining processes..."
    for pid in $remaining_processes; do
        kill -9 $pid 2>/dev/null
        echo "Force killed process $pid"
    done
else
    echo "All target processes successfully terminated"
fi

echo ""
echo "============================================"
echo "    All processes have been terminated"
echo "============================================"
echo ""