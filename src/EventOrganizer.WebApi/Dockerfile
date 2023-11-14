#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/EventOrganizer.WebApi/EventOrganizer.WebApi.csproj", "src/EventOrganizer.WebApi/"]
COPY ["src/EventOrganizer.Core/EventOrganizer.Core.csproj", "src/EventOrganizer.Core/"]
COPY ["src/EventOrganizer.Domain/EventOrganizer.Domain.csproj", "src/EventOrganizer.Domain/"]
COPY ["src/EventOrganizer.EF.MySql/EventOrganizer.EF.MySql.csproj", "src/EventOrganizer.EF.MySql/"]
COPY ["src/EventOrganizer.EF/EventOrganizer.EF.csproj", "src/EventOrganizer.EF/"]
RUN dotnet restore "src/EventOrganizer.WebApi/EventOrganizer.WebApi.csproj"
COPY . .
WORKDIR "/src/src/EventOrganizer.WebApi"
RUN dotnet build "EventOrganizer.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventOrganizer.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventOrganizer.WebApi.dll"]