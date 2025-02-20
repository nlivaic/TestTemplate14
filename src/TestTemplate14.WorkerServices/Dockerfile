#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TestTemplate14.WorkerServices/TestTemplate14.WorkerServices.csproj", "src/TestTemplate14.WorkerServices/"]
COPY ["src/TestTemplate14.Core/TestTemplate14.Core.csproj", "src/TestTemplate14.Core/"]
COPY ["src/TestTemplate14.Common/TestTemplate14.Common.csproj", "src/TestTemplate14.Common/"]
RUN dotnet restore "src/TestTemplate14.WorkerServices/TestTemplate14.WorkerServices.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate14.WorkerServices"
RUN dotnet build "TestTemplate14.WorkerServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate14.WorkerServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/TestTemplate14.WorkerServices/entrypoint.sh", "src/entrypoint.sh"]
COPY ["src/TestTemplate14.WorkerServices/wait-for-it.sh", "src/wait-for-it.sh"]
CMD ["/bin/bash", "src/entrypoint.sh"]
