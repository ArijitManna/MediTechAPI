#!/bin/bash

# MediTech API - Quick Commands
# Add this to your ~/.zshrc or ~/.bashrc for global access:
# alias meditech-kill="~/path/to/your/project/MediTechBackendAPI/quick-commands.sh kill"
# alias meditech-start="~/path/to/your/project/MediTechBackendAPI/quick-commands.sh start"
# alias meditech-restart="~/path/to/your/project/MediTechBackendAPI/quick-commands.sh restart"

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

case "$1" in
    "kill")
        echo "🛑 Killing all MediTech API processes..."
        "$SCRIPT_DIR/kill-processes.sh"
        ;;
    "start")
        echo "🚀 Starting MediTech API..."
        "$SCRIPT_DIR/start-api.sh"
        ;;
    "restart")
        echo "🔄 Restarting MediTech API..."
        "$SCRIPT_DIR/kill-processes.sh"
        echo "Waiting 3 seconds..."
        sleep 3
        "$SCRIPT_DIR/start-api.sh"
        ;;
    "status")
        echo "📊 Checking MediTech API status..."
        echo ""
        echo "Processes on port 5094:"
        lsof -ti :5094 2>/dev/null | head -5
        echo ""
        echo "Running dotnet processes:"
        pgrep -f "dotnet" | head -5
        echo ""
        echo "Running MediTech processes:"
        pgrep -f "MediTech" | head -5
        ;;
    *)
        echo "MediTech API - Quick Commands"
        echo ""
        echo "Usage: $0 {kill|start|restart|status}"
        echo ""
        echo "Commands:"
        echo "  kill     - Kill all running processes"
        echo "  start    - Start the API"
        echo "  restart  - Kill and restart the API"
        echo "  status   - Check running processes"
        echo ""
        echo "Examples:"
        echo "  $0 kill"
        echo "  $0 start"
        echo "  $0 restart"
        echo "  $0 status"
        ;;
esac