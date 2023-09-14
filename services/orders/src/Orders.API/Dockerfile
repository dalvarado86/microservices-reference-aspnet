#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["services/orders/src/Orders.API/Orders.API.csproj", "services/orders/src/Orders.API/"]
COPY ["services/orders/src/Orders.Application/Orders.Application.csproj", "services/orders/src/Orders.Application/"]
COPY ["services/orders/src/Orders.Domain/Orders.Domain.csproj", "services/orders/src/Orders.Domain/"]
COPY ["services/orders/src/Orders.Infrastructure/Orders.Infrastructure.csproj", "services/orders/src/Orders.Infrastructure/"]
RUN dotnet restore "services/orders/src/Orders.API/Orders.API.csproj"
COPY . .
WORKDIR "/src/services/orders/src/Orders.API"
RUN dotnet build "Orders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orders.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.API.dll"]