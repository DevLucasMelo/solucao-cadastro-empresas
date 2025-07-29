FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./projeto-cadastro-empresas/ ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80

ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "CadastroEmpresasApp.dll"]
