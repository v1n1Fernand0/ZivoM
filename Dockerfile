# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de solução e restaura as dependências
COPY *.sln .
COPY ZivoM.Api/*.csproj ./ZivoM.Api/
COPY ZivoM.Application/*.csproj ./ZivoM.Application/
COPY ZivoM.Domain/*.csproj ./ZivoM.Domain/
COPY ZivoM.Infrastructure/*.csproj ./ZivoM.Infrastructure/
COPY ZivoM.Tests/*.csproj ./ZivoM.Tests/
RUN dotnet restore

# Copia o restante dos arquivos e publica o aplicativo
COPY ZivoM.Api/. ./ZivoM.Api/
COPY ZivoM.Application/. ./ZivoM.Application/
COPY ZivoM.Domain/. ./ZivoM.Domain/
COPY ZivoM.Infrastructure/. ./ZivoM.Infrastructure/
COPY ZivoM.Tests/. ./ZivoM.Tests/
WORKDIR /app/ZivoM.Api
RUN dotnet publish -c Release -o out

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/ZivoM.Api/out ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "ZivoM.Api.dll"]
