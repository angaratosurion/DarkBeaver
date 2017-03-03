G:
cd ..
cd "G:\AutoSyncWithParis-Server\My Programs\dotNet\BlackOwl\Projects\bin"
xcopy ".\Projects.dll" "\My Programs\dotNet\BlackOwl\BlackOwl\Modules\Projects" /E /Y
cd ..
xcopy ".\Views" "\My Programs\dotNet\BlackOwl\BlackOwl\Modules\Projects\Views" /E /Y
pause