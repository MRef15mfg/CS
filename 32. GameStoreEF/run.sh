#!/usr/bin/env bash
set -e
dotnet restore
dotnet build
dotnet run --project GameStore.App
