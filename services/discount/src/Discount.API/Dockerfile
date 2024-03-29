#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/discount/src/Discount.API/Discount.API.csproj", "services/discount/src/Discount.API/"]
COPY ["services/discount/src/Discount.Common/Discount.Common.csproj", "services/discount/src/Discount.Common/"]
RUN dotnet restore "services/discount/src/Discount.API/Discount.API.csproj"
COPY . .
WORKDIR "/src/services/discount/src/Discount.API"
RUN dotnet build "Discount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]