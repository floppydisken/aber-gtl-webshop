#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Webshop.Order.Api/Webshop.Order.Api.csproj"
RUN dotnet build "Webshop.Order.Api/Webshop.Order.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Webshop.Order.Api/Webshop.Order.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Webshop.Order.Api.dll"]