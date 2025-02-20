#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TestTemplate14.Api/TestTemplate14.Api.csproj", "src/TestTemplate14.Api/"]
COPY ["src/TestTemplate14.Data/TestTemplate14.Data.csproj", "src/TestTemplate14.Data/"]
COPY ["src/TestTemplate14.Common/TestTemplate14.Common.csproj", "src/TestTemplate14.Common/"]
COPY ["src/TestTemplate14.Core/TestTemplate14.Core.csproj", "src/TestTemplate14.Core/"]
RUN dotnet restore "src/TestTemplate14.Api/TestTemplate14.Api.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate14.Api"
RUN dotnet build "TestTemplate14.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate14.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTemplate14.Api.dll"]