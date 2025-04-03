FROM node:20.10-alpine AS clientAppBuild
WORKDIR /app
COPY NEC.Fulf3PL.AdminWebApp/ClientApp/package.json NEC.Fulf3PL.AdminWebApp/ClientApp/package-lock.json ./
RUN npm install
COPY NEC.Fulf3PL.AdminWebApp/ClientApp .
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["NEC.Fulf3PL.AdminWebApp/NEC.Fulf3PL.AdminWebApp.csproj", "NEC.Fulf3PL.AdminWebApp/"]
COPY ["NEC.Fulf3PL.Infrastructure/NEC.Fulf3PL.Infrastructure.csproj", "NEC.Fulf3PL.Infrastructure/"]
COPY ["NEC.Fulf3PL.Application/NEC.Fulf3PL.Application.csproj", "NEC.Fulf3PL.Application/"]
COPY ["NEC.Fulf3PL.Core/NEC.Fulf3PL.Core.csproj", "NEC.Fulf3PL.Core/"]
RUN dotnet restore "NEC.Fulf3PL.AdminWebApp/NEC.Fulf3PL.AdminWebApp.csproj"
COPY . .
WORKDIR "/src/NEC.Fulf3PL.AdminWebApp"
RUN dotnet build "NEC.Fulf3PL.AdminWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NEC.Fulf3PL.AdminWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=clientAppBuild /app/build /app/wwwroot
ENTRYPOINT ["dotnet", "NEC.Fulf3PL.AdminWebApp.dll"]