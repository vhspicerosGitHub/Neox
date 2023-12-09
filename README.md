# Neox
Prueba de desarrollo para proceso de selección de Neox, el requerimiento se encuentra en /Docs/Backend_.net _prueba.pdf

## Resumen
En general se requiere generar una API en .Net Core para administración de clientes. 

## Base de datos
Se encuentra en este mismo repositorio en la carpeta /Database

## Comandos
### Como compilar el proyecto
```
dotnet build
```

### Como ejecutar el proyecto
```
dotnet run --project  .\Neox.Web\Neox.Web.csproj
```

### Como ejecutar las pruebas
```
dotnet test

```

## Pruebas manuales

Se puede hacer via swagger localmente (https://localhost:7294/swagger/index.html) , por postman o por consola. 
Los comandos para probar son:

```
-- Obtiene la lista de clientes
curl -X 'GET'  'https://localhost:7294/Client'  -i

-- Obtiene un cliente en especifico
curl -X 'GET'  'https://localhost:7294/Client/1' -i

-- Crea un cliente
curl -X 'POST'  'https://localhost:7294/Client' -i  -H 'Content-Type: application/json'  -d '{ "email": "Ema@gmail.com",  "name": "Ema Saavedra"}'

-- Elimina un cliente
curl -X 'DELETE' -i 'https://localhost:7294/Client/4'

-- Actualiza un cliente
curl -X 'PATCH' 'https://localhost:7294/Client/5' -i -H 'Content-Type: application/json'  -d '{"email": "sara@gmail.com", "name": "Sara Saavedra"}'

```

## Implementación. 
Para el acceso de datos se utlizo el patron repository con Dapper, para las pruebas Nunit y la libreria .MOQ. Y por último para el Log utilizo SeriLog


