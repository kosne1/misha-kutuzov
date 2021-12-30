FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Reports.Clients/Reports.Clients.csproj", "Reports.Clients/"]
RUN dotnet restore "Reports.Clients/Reports.Clients.csproj"
COPY . .
WORKDIR "/src/Reports.Clients"
RUN dotnet build "Reports.Clients.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reports.Clients.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reports.Clients.dll"]
