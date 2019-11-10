FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SGDE.API/SGDE.API.csproj", "SGDE.API/"]
COPY ["SGDE.Domain/SGDE.Domain.csproj", "SGDE.Domain/"]
COPY ["SGDE.DataEFCoreMySQL/SGDE.DataEFCoreMySQL.csproj", "SGDE.DataEFCoreMySQL/"]
COPY ["SGDE.DataEFCoreSQL/SGDE.DataEFCoreSQL.csproj", "SGDE.DataEFCoreSQL/"]
RUN dotnet restore "SGDE.API/SGDE.API.csproj"
COPY . .
WORKDIR "/src/SGDE.API"
RUN dotnet build "SGDE.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGDE.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGDE.API.dll"]