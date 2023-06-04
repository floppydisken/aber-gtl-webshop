#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src
RUN dotnet restore "Webshop.Customer.Api/Webshop.Customer.Api.csproj"
RUN dotnet build "Webshop.Customer.Api/Webshop.Customer.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Webshop.Customer.Api/Webshop.Customer.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Webshop.Customer.Api.dll"]
