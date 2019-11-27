FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
Copy . .

CMD ASPNETCORE_URLS=http//*:$PORT dotnet DotNetCoreHeroku.dll


