FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

COPY ./bin/Release/netcoreapp2.2/publish/ /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "/app/ApiSeguridad.dll"]