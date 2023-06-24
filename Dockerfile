FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OrderMngmt.API/OrderMngmt.API.csproj", "OrderMngmt.API/"]
RUN dotnet restore "OrderMngmt.API/OrderMngmt.API.csproj"
COPY . .
WORKDIR "/src/OrderMngmt.API"
RUN dotnet build "OrderMngmt.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderMngmt.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderMngmt.API.dll"]