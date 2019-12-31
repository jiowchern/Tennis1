

FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app


RUN apt-get update && apt-get install -y git 

RUN git clone --recurse-submodules https://github.com/jiowchern/Tennis1.git

RUN dotnet publish  ./Tennis1/backend/Tennis1.Common -o /app -c Release -f netstandard2.0
RUN dotnet publish  ./Tennis1/backend/Tennis1.Game -o /app -c Release -f netstandard2.0
RUN dotnet publish  ./Tennis1/backend/Tennis1.Host -o /app -c Release 


 

ENTRYPOINT ["dotnet", "Tennis1.Host.dll","launchini","./Tennis1/backend/assets/server/config.ini"]