# Consulte https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Oficina.ApiGateway/Oficina.ApiGateway.csproj", "Oficina.ApiGateway/"]
COPY ["Oficina.Cadastro.Endpoints/Oficina.Cadastro.Endpoints.csproj", "Oficina.Cadastro.Endpoints/"]
COPY ["Oficina.Cadastro.Application/Oficina.Cadastro.Application.csproj", "Oficina.Cadastro.Application/"]
COPY ["Oficina.Cadastro.Domain/Oficina.Cadastro.Domain.csproj", "Oficina.Cadastro.Domain/"]
COPY ["Oficina.Messaging/Oficina.Messaging.csproj", "Oficina.Messaging/"]
COPY ["Oficina.Observability/Oficina.Observability.csproj", "Oficina.Observability/"]
COPY ["Oficina.Cadastro.Infrastructure/Oficina.Cadastro.Infrastructure.csproj", "Oficina.Cadastro.Infrastructure/"]
COPY ["Oficina.SharedKernel/Oficina.SharedKernel.csproj", "Oficina.SharedKernel/"]
RUN dotnet restore "Oficina.ApiGateway/Oficina.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Oficina.ApiGateway"
RUN dotnet build "Oficina.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Oficina.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oficina.ApiGateway.dll"]

