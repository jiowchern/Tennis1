

dotnet clean 
dotnet restore


dotnet build ../theirs/regulus/Regulus.Application.Server
dotnet build ../theirs/regulus/Regulus.Application.Protocol.Generator
dotnet build 

rd ..\frontend\Tennis1\Assets\Project\Backend /q /s
mkdir ..\frontend\Tennis1\Assets\Project\Backend

copy Tennis1.User\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend
copy Tennis1.Game\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend
copy ..\theirs\Regulus\Regulus.Remote\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend
copy ..\theirs\Regulus\Regulus.Serialization\bin\Debug\netstandard2.0\*.*  ..\frontend\Tennis1\Assets\Project\Backend

cd ..\theirs\Regulus\Regulus.Application.Protocol.Generator\bin\Debug\netcoreapp2.0

dotnet .\Regulus.Application.Protocol.Generator.dll ..\..\..\..\..\..\backend\Tennis1.Common\bin\Debug\netstandard2.0\Tennis1.Common.dll ..\..\..\..\..\..\frontend\Tennis1\Assets\Project\Backend\Tennis1.Protocol.dll

pause