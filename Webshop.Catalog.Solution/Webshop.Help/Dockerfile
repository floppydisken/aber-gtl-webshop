#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Webshop.Help/Webshop.Help.csproj"
RUN dotnet build "Webshop.Help/Webshop.Help.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webshop.Help/Webshop.Help.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Webshop.Help.dll"]