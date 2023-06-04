#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src
RUN dotnet restore "Webshop.Payment.Api/Webshop.Payment.Api.csproj"
RUN dotnet build "Webshop.Payment.Api/Webshop.Payment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webshop.Payment.Api/Webshop.Payment.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Webshop.Payment.Api.dll"]