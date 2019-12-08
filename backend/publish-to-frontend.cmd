rd ..\frontend\Tennis1\Assets\Project\Backend /q /s
mkdir ..\frontend\Tennis1\Assets\Project\Backend

copy Regulus.Game.Tennis1.User\bin\Debug\*.*  ..\frontend\Tennis1\Assets\Project\Backend
copy Regulus.Game.Tennis1.Game\bin\Debug\*.*  ..\frontend\Tennis1\Assets\Project\Backend
copy ..\theirs\Regulus\Library\Remoting\bin\Debug\*.*  ..\frontend\Tennis1\Assets\Project\Backend
copy ..\theirs\Regulus\Library\Regulus.Serialization\bin\Debug\*.*  ..\frontend\Tennis1\Assets\Project\Backend

cd ..\theirs\Regulus\Tool\GhostProviderGenerator\bin\Debug

Regulus.Application.Protocol.Generator.exe ..\..\..\..\..\..\backend\Regulus.Game.Tennis1.Protocol\bin\Debug\Regulus.Game.Tennis1.Protocol.dll ..\..\..\..\..\..\frontend\Tennis1\Assets\Project\Backend\Regulus.Game.Tennis1.ProtocolTmpl.dll

pause