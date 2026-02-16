FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

COPY . ./
# Установка библиотек
RUN dotnet restore

# Ран
ENTRYPOINT ["dotnet", "run"]