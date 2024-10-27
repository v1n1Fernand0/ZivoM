FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY ZivoM.Api/*.csproj ./ZivoM.Api/
COPY ZivoM.Application/*.csproj ./ZivoM.Application/
COPY ZivoM.Domain/*.csproj ./ZivoM.Domain/
COPY ZivoM.Infrastructure/*.csproj ./ZivoM.Infrastructure/
COPY ZivoM.Tests/*.csproj ./ZivoM.Tests/

RUN dotnet restore

COPY ZivoM.Api/. ./ZivoM.Api/
COPY ZivoM.Application/. ./ZivoM.Application/
COPY ZivoM.Domain/. ./ZivoM.Domain/
COPY ZivoM.Infrastructure/. ./ZivoM.Infrastructure/
COPY ZivoM.Tests/. ./ZivoM.Tests/

WORKDIR /app/ZivoM.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/ZivoM.Api/out ./

ENTRYPOINT ["dotnet", "ZivoM.Api.dll"]
