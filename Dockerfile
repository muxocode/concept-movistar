FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/webApi/host/Offers.webApi/Offers.webApi.csproj", "src/webApi/host/Offers.webApi/"]
COPY ["src/tools.bussines/movistar.userPreferences/movistar.userPreferences.csproj", "src/tools.bussines/movistar.userPreferences/"]
COPY ["src/entities/model.bussines/movistar.model.bussines.csproj", "src/entities/model.bussines/"]
COPY ["src/database/canalonline.data/canalonline.data.csproj", "src/database/canalonline.data/"]
COPY ["src/crossapp.packages/crossapp.common/crossapp.common.csproj", "src/crossapp.packages/crossapp.common/"]
COPY ["src/entities/model.entities/model.entities.csproj", "src/entities/model.entities/"]
COPY ["src/tools.packages/LogHelper/LogHelper.csproj", "src/tools.packages/LogHelper/"]
COPY ["src/crossapp.packages/crossapp.file/crossapp.log.csproj", "src/crossapp.packages/crossapp.file/"]
COPY ["src/webApi/controllers/Offers.controllers/Offers.controllers.csproj", "src/webApi/controllers/Offers.controllers/"]
COPY ["src/webApi/controllers/controllers.common/controllers.common.csproj", "src/webApi/controllers/controllers.common/"]
COPY ["src/webApi/domain/domain/domain.csproj", "src/webApi/domain/domain/"]
RUN dotnet restore "src/webApi/host/Offers.webApi/Offers.webApi.csproj"
COPY . .
WORKDIR "/src/src/webApi/host/Offers.webApi"
RUN dotnet build "Offers.webApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Offers.webApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Offers.webApi.dll"]