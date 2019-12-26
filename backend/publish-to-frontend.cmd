

rd ..\frontend\Tennis1\Assets\Project\Backend /q /s
mkdir ..\frontend\Tennis1\Assets\Project\Backend
mkdir ..\frontend\Tennis1\Assets\Project\Backend\Plugins
mkdir ..\frontend\Tennis1\Assets\Project\Backend\Plugins\Source


dotnet publish ./Tennis1.Game -f netstandard2.0 -o ..\frontend\Tennis1\Assets\Project\Backend\Plugins
dotnet publish ./Tennis1.User -f netstandard2.0 -o ..\frontend\Tennis1\Assets\Project\Backend\Plugins

dotnet run -f netcoreapp2.0 --project ./Tennis1.Protocol.Outputer Tennis1.Tennis1 ..\frontend\Tennis1\Assets\Project\Backend\Plugins\Tennis1.Common.dll ..\frontend\Tennis1\Assets\Project\Backend\Plugins\Source

pause