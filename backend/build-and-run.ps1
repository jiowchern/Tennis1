$msbuildPath = & ".\tools\Resolve-MSBuild.ps1"

& $msbuildPath 

if (test-path "./bin"  ) {
	Remove-Item "./bin" -Recurse
}

mkdir "./bin"
mkdir "./bin/server"
copy "./assets/server/*.*" "./bin/server"

copy "../theirs/regulus/tool/server/bin/debug/*.*" "./bin/server"
copy "./Regulus.Game.Tennis1.Protocol/bin/debug/*.*" "./bin/server"
copy "./Regulus.Game.Tennis1.Game/bin/debug/*.*" "./bin/server"

cd "./bin/server"
& "./Regulus.Application.Server.exe" "launchini" "config.ini"
pause
