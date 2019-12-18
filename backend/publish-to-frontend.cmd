

dotnet clean 
dotnet restore


dotnet build ../theirs/regulus/Regulus.Application.Server
dotnet build ../theirs/regulus/Regulus.Application.Protocol.TextWriter
dotnet build 

rd ..\frontend\Tennis1\Assets\Project\Backend /q /s
mkdir ..\frontend\Tennis1\Assets\Project\Backend
mkdir ..\frontend\Tennis1\Assets\Project\Backend\Plugins
mkdir ..\frontend\Tennis1\Assets\Project\Backend\Plugins\Source

copy Tennis1.User\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend\Plugins
copy Tennis1.Game\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend\Plugins
copy ..\theirs\Regulus\Regulus.Remote\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend\Plugins
copy ..\theirs\Regulus\Regulus.Serialization\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend\Plugins


cd ..
dotnet .\theirs\Regulus\Regulus.Application.Protocol.TextWriter\bin\Debug\netcoreapp2.2\Regulus.Application.Protocol.TextWriter.dll Tennis1.ProtocolProvider .\frontend\Tennis1\Assets\Project\Backend\Plugins\Tennis1.Common.dll .\frontend\Tennis1\Assets\Project\Backend\Plugins\Source

pause