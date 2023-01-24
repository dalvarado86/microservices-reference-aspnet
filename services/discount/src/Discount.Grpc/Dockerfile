#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/discount/src/Discount.Grpc/Discount.Grpc.csproj", "services/discount/src/Discount.Grpc/"]
COPY ["services/discount/src/Discount.Common/Discount.Common.csproj", "services/discount/src/Discount.Common/"]
RUN dotnet restore "services/discount/src/Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/services/discount/src/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]