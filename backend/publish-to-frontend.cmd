

dotnet clean 
dotnet restore
dotnet build 

rd ..\frontend\Tennis1\Assets\Project\Backend /q /s
mkdir ..\frontend\Tennis1\Assets\Project\Backend
mkdir ..\frontend\Tennis1\Assets\Project\Backend\Plugins
mkdir ..\frontend\Tennis1\Assets\Project\Backend\Plugins\Source




dotnet publish ./Tennis1.Game -f netstandard2.0 -o ..\frontend\Tennis1\Assets\Project\Backend\Plugins
dotnet publish ./Tennis1.User -f netstandard2.0 -o ..\frontend\Tennis1\Assets\Project\Backend\Plugins



cd ..
dotnet .\tools\ProtocolBuilder\Regulus.Application.Protocol.CodeWriter.dll Tennis1.ProtocolProvider .\frontend\Tennis1\Assets\Project\Backend\Plugins\Tennis1.Common.dll .\frontend\Tennis1\Assets\Project\Backend\Plugins\Source

pause