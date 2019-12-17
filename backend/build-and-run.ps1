# $msbuildPath = & ".\tools\Resolve-MSBuild.ps1"

# & $msbuildPath 

dotnet clean 
dotnet restore

# dotnet build ../theirs/regulus/Regulus.Remote.Client
dotnet build ../theirs/regulus/Regulus.Application.Server
dotnet build ../theirs/regulus/Regulus.Application.Protocol.Generator
dotnet build 

if (test-path "./bin"  ) {
	Remove-Item "./bin" -Recurse
}

mkdir "./bin"
mkdir "./bin/server"
copy "./assets/server/*.*" "./bin/server"

copy "../theirs/regulus/Regulus.Application.Server/bin/debug/netcoreapp2.0/*.*" "./bin/server"
copy "./Tennis1.Common/bin/debug/netcoreapp2.0/*.*" "./bin/server"
copy "./Tennis1.Game/bin/debug/netcoreapp2.0/*.*" "./bin/server"



cd "./bin/server"
& dotnet ./Regulus.Application.Server.dll "launchini" "config.ini"
pause
