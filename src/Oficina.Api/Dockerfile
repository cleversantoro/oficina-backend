FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Oficina.Api/Oficina.Api.csproj", "src/Oficina.Api/"]
RUN dotnet restore "src/Oficina.Api/Oficina.Api.csproj"
COPY . .
WORKDIR "/src/src/Oficina.Api"
RUN dotnet publish "Oficina.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Oficina.Api.dll"]
