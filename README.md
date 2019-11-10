Migración
*********

1- Establecer como proyecto de inicio en donde tengamos instanciado CA.Domain (cadena de conexión). En mi caso CA.API
2- En la 'Consola del Administrador de Paquetes' establecer como 'Proyecto predeterminado' en donde tengamos instanciados los repositorios. En mi caso CA.DataEFCoreSQL
3- Ejecutamos en Power-Shell 'add-migration FirstMigration'
4- Se creará en CA.Domain una carpeta en donde se creará la migración
5- Construimos la base de datos en donde haya indicado la cadena de conexión ejecutando en Power-Shell 'Update-Database'
6- Corremos la app CA.SeedData

7- Si queremos eliminar la Migración, ejecutamos en Power-Shell 'remove-migration'


Docker
******

Arrancar la imagen del mysql
----------------------------
docker run -d -p 33060:3306 --name mysql-db -e MYSQL_ROOT_PASSWORD=Aceitun@1 --mount src=mysql-db-data,dst=/var/lib/mysql mysql

En el directorio/carpeta de la solucion
----------------------------------------
docker build -f SGDE.API\Dockerfile -t sgde.api .


docker run -p 8000:80 sgde.api

docker-compose up
docker-compose up -d

docker-compose down

// Parar/stop el container por nombre
docker stop $(docker ps -q --filter name=mysql)
docker stop $(docker ps -q --filter ancestor=sgde.api)


docker run                  --name mysql-db -e MYSQL_ROOT_PASSWORD=secret -d mysql:tag
docker run -d -p 33060:3306 --name mysql-db -e MYSQL_ROOT_PASSWORD=secret mysql