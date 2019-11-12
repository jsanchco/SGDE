FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SGDE.WebClient/SGDE.WebClient.csproj", "SGDE.WebClient/"]
RUN dotnet restore "SGDE.WebClient/SGDE.WebClient.csproj"
COPY . .
WORKDIR "/src/SGDE.WebClient"
RUN dotnet build "SGDE.WebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGDE.WebClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGDE.WebClient.dll"]