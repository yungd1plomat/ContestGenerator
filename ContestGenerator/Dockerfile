#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContestGenerator/ContestGenerator.csproj", "ContestGenerator/"]
RUN dotnet restore "ContestGenerator/ContestGenerator.csproj"
COPY . .
WORKDIR "/src/ContestGenerator"
RUN dotnet build "ContestGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContestGenerator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContestGenerator.dll"]