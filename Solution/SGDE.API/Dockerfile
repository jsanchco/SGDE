FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app

COPY solution.sln .
COPY sgde.api/*.csproj ./sgde.api/
COPY sgde.dataefcoremysql/*.csproj ./sgde.sgde.dataefcoremysql/
COPY sgde.dataefcoresql/*.csproj ./sgde.sgde.dataefcoresql/
COPY sgde.domain/*.csproj ./sgde.sgde.domain/
RUN dotnet restore

RUN dotnet restore solution.sln

COPY sgde.api/. ./sgde.api/
WORKDIR /app/sgde.api
RUN dotnet publish -c Release -o out

WORKDIR /app
WORKDIR /app
COPY --from=build /app/sgde.api/out ./
ENTRYPOINT ["dotnet", "sgde.api.dll"]