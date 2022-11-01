#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SpendingTracker/Server/SpendingTracker.Server.csproj", "SpendingTracker/Server/"]
COPY ["SpendingTracker/Client/SpendingTracker.Client.csproj", "SpendingTracker/Client/"]
COPY ["SpendingTracker/Shared/SpendingTracker.Shared.csproj", "SpendingTracker/Shared/"]
RUN dotnet restore "SpendingTracker/Server/SpendingTracker.Server.csproj"
COPY . .
WORKDIR "/src/SpendingTracker/Server"
RUN dotnet build "SpendingTracker.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpendingTracker.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendingTracker.Server.dll"]