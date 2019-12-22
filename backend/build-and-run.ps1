# $msbuildPath = & ".\tools\Resolve-MSBuild.ps1"

# & $msbuildPath 

dotnet clean 
dotnet restore

dotnet build 

if (test-path "./bin"  ) {
	Remove-Item "./bin" -Recurse
}

mkdir "./bin"
mkdir "./bin/server"
copy "./assets/server/*.*" "./bin/server"

dotnet publish ./Tennis1.Host -o ./bin/server
copy "./Tennis1.Common/bin/debug/netstandard2.0/*.*" "./bin/server"
copy "./Tennis1.Game/bin/debug/netstandard2.0/*.*" "./bin/server"



cd "./bin/server"
& dotnet ./Tennis1.Host.dll "launchini" "config.ini"
pause
