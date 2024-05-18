FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["Educator.Api/Educator.Api.csproj", "Educator.Api/"]
COPY ["Educator.ExercisesGenerators/Educator.ExercisesGenerators.csproj", "Educator.ExercisesGenerators/"]
COPY ["Educator.Application/Educator.Application.csproj", "Educator.Application/"]
COPY ["Educator.Domain/Educator.Domain.csproj", "Educator.Domain/"]
COPY ["Educator.Ui/Educator.Ui.csproj", "Educator.Ui/"]
COPY ["Educator.Shared.Models/Educator.Shared.Models.csproj", "Educator.Shared.Models/"]

RUN dotnet restore "Educator.Api/Educator.Api.csproj"
COPY . .

WORKDIR "/src/Educator.Api"
RUN dotnet build "Educator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Educator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS finalll
WORKDIR /app
COPY --from=publish /app/publish .

#RUN mkdir -p /app/certificates
#COPY ["certificates/educator.pfx", "/app/certificates"]
ENTRYPOINT ["dotnet", "Educator.Api.dll"]