@echo off
echo ============================================
echo  AutoRecall MiscRobots - Compilando mod...
echo ============================================
echo.

cd /d "%~dp0Source\AutoRecall_MiscRobots"

dotnet build AutoRecall_MiscRobots.csproj --configuration Release

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] La compilacion fallo. Revisa los errores arriba.
    pause
    exit /b 1
)

echo.
echo [OK] Compilacion exitosa!
echo La DLL se ha generado en: 1.6\Assemblies\AutoRecall_MiscRobots.dll

echo Creando carpeta de distribucion (Mod_Listo_Para_Instalar)...
cd /d "%~dp0"
if exist "Mod_Listo_Para_Instalar" rmdir /s /q "Mod_Listo_Para_Instalar"
mkdir "Mod_Listo_Para_Instalar\AutoRecall Misc Robots on Raid"
xcopy "About" "Mod_Listo_Para_Instalar\AutoRecall Misc Robots on Raid\About\" /E /I /Y >nul
xcopy "1.6" "Mod_Listo_Para_Instalar\AutoRecall Misc Robots on Raid\1.6\" /E /I /Y >nul

echo.
echo ==============================================================================
echo [EXITO] Tu mod esta listo en la carpeta: "Mod_Listo_Para_Instalar\AutoRecall Misc Robots on Raid"
echo Solo tienes que copiar esa carpeta a: "C:\Users\Audur\Desktop\Games NVM\RimWorld\Mods"
echo ==============================================================================
exit /b 0
