# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo .csproj e restaurar as dependências
COPY ["ColetaApi/ColetaApi.csproj", "ColetaApi/"]
RUN dotnet restore "ColetaApi/ColetaApi.csproj"

# Copiar todo o código do projeto e publicar
COPY . .
WORKDIR "/src/ColetaApi"
RUN dotnet build "ColetaApi.csproj" -c Release -o /app/build
RUN dotnet publish "ColetaApi.csproj" -c Release -o /app/publish

# Etapa 2: Runtime da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ColetaApi.dll"]
