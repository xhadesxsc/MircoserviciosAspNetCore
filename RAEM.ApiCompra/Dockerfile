FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

COPY ./bin/Release/netcoreapp2.2/publish/ /app
ENV ASPNETCORE_URLS=http://+:8080
ENV BusUrl="Endpoint=sb://busgalaxymasp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SVPN05P/2WlAnXa7EonF9tL2DzJUA6kRLdbIAYS6zyw="
ENV BusTopic="pedidosender"
ENV BusTopicSubscriptor="pedidosql"
ENV CnnDbVentas="Data Source=srvventasmasp.database.windows.net;Initial Catalog=dbVentas;User ID=galaxy;Password=A123456789123$"
ENV UrlSeguridad="http://54.198.39.194:8090/"
ENV NombreApi="apiPedido"

EXPOSE 8080

ENTRYPOINT ["dotnet", "/app/ApiPedido.dll"]