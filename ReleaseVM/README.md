### Azure Container Registry
registrydemosession.azurecr.io
Username: registrydemosession
Password: /OWe5RU3uIvvVQf9bvQAbkN22S0wSZdZ

### Login ACR
docker login registrydemosession.azurecr.io -u registrydemosession -p /OWe5RU3uIvvVQf9bvQAbkN22S0wSZdZ

### Ubuntu
Installazione Docker: https://docs.docker.com/install/linux/docker-ce/ubuntu/
Installare Docker-Compose: https://docs.docker.com/compose/install/

### Raspberry
Docker e Docker-compose: https://ludusrusso.cc/2018/06/29/docker-raspberrypi/

### Build Images
docker-compose -f "E:\...\ARMWebApp\docker-compose.yml" -f "E:\...\ARMWebApp\docker-compose.override.yml" -p dockercompose15762502015215390006 --no-ansi build armwebapp

### Push Images
-- Visual Studio Done
docker tag armwebapp registrydemosession.azurecr.io/armwebapp
docker push registrydemosession.azurecr.io/armwebapp
-- Visual Studio Not Done
docker tag simpsonvision.api registrydemosession.azurecr.io/simpsonvision.api
docker push registrydemosession.azurecr.io/simpsonvision.api

### Up and Run Container
docker-compose up