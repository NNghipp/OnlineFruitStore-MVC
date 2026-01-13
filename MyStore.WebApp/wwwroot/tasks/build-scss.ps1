# Build Script - Compile SCSS to CSS
# Usage: PowerShell .\build-scss.ps1

Write-Host "=== MyStore Build Script ===" -ForegroundColor Green

# Check if sass is installed
$sassCheck = Get-Command sass -ErrorAction SilentlyContinue
if (-not $sassCheck) {
    Write-Host "Installing sass..." -ForegroundColor Yellow
    npm install -g sass
}

# Compile SCSS
Write-Host "Compiling SCSS..." -ForegroundColor Cyan
sass ../scss/main.scss ../css/main.css

Write-Host "Build completed!" -ForegroundColor Green
