G:
cd ..
cd "G:\AutoSyncWithParis-Server\My Programs\dotNet\BlackOwl\Projects\bin\Debug"
xcopy ".\Projects.dll" "G:\AutoSyncWithParis-Server\My Programs\dotNet\BlackOwl\BlackOwl\Modules\Projects" /E /Y
cd ..
cd ..
xcopy ".\Views" "G:\AutoSyncWithParis-Server\My Programs\dotNet\BlackOwl\BlackOwl\Modules\Projects\Views" /E /Y
pause