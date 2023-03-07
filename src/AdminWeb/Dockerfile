FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AdminWeb/AdminWeb.csproj", "src/AdminWeb/"]
RUN dotnet restore "src/AdminWeb/AdminWeb.csproj"
COPY . .
WORKDIR "/src/src/AdminWeb"
RUN dotnet build "AdminWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminWeb.dll"]
