#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PermissionManager.Api/PermissionManager.API.csproj", "PermissionManager.Api/"]
COPY ["PermissionManager.Core/PermissionManager.Core.csproj", "PermissionManager.Core/"]
COPY ["PermissionManager.Models/PermissionManager.Models.csproj", "PermissionManager.Models/"]
COPY ["PermissionManager.Repositories/PermissionManager.Repositories.csproj", "PermissionManager.Repositories/"]
RUN dotnet restore "PermissionManager.Api/PermissionManager.API.csproj"
COPY . .
WORKDIR "/src/PermissionManager.Api"
RUN dotnet build "PermissionManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PermissionManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermissionManager.API.dll"]