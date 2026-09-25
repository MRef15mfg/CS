#!/usr/bin/env bash
set -e
cd "$(dirname "$0")"
dotnet restore
dotnet build
dotnet run --urls "http://localhost:5000"
