# Use the official ASP.NET Core runtime image as base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Mc2.CrudTest.Presentation/Client/Mc2.CrudTest.Presentation.Client.csproj", "Mc2.CrudTest.Presentation/Client/"]
RUN dotnet restore "./Mc2.CrudTest.Presentation/Client/./Mc2.CrudTest.Presentation.Client.csproj"

COPY . .
WORKDIR "/src/Mc2.CrudTest.Presentation/Client"
RUN dotnet build "./Mc2.CrudTest.Presentation.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Mc2.CrudTest.Presentation.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Client.dll"]