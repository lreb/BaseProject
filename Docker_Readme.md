# Docker

## Terminology

- Images: The file system and configuration of our application which are used to create containers.
- Containers: Running instances of Docker images
- Docker Daemon:  The background service running on the host that manages building, running and distributing Docker containers.
- Docker Client: The command line tool that allows the user to interact with the Docker daemon.
- Docker Store: A registry of Docker images, where you can find trusted and enterprise ready containers, plugins, and Docker editions. You'll be using this later in this tutorial.

## Docker base command options

`app*` Docker Application
`assemble*` Framework-aware builds (Docker Enterprise)
`builder` Manage builds
`cluster` Manage Docker clusters (Docker Enterprise)
`config` Manage Docker configs
`context` Manage contexts
`engine` Manage the docker Engine
`image` Manage images
`network` Manage networks
`node` Manage Swarm nodes
`plugin` Manage plugins
`registry*` Manage Docker registries
`secret` Manage Docker secrets
`service` Manage services
`stack` Manage Docker stacks
`swarm` Manage swarm
`system` Manage Docker
`template*` Quickly scaffold services (Docker Enterprise)
`trust` Manage trust on Docker images
`volume` Manage volumes

## Build

show images
`docker images`

List all images that are locally stored with the Docker Engine
`docker image ls`

Delete an image from the local image store
`docker image rm alpine:3.4`

## Share
Pull an image from a registry
`docker pull myimage:1.0`

Retag a local image with a new image name and tag
`docker tag myimage:1.0 myrepo/myimage:2.0`

Push an image to a registry
`docker push myrepo/myimage:2.0`

## Containers

Run a container from the Alpine version 3.9 image, name the running container “web” and expose port 5000 externally, mapped to port 80 inside the container.
`docker container run --name web -p 5000:80 alpine:3.9`

- `-d`: will create a container with the process detached from our terminal
- `-p`: will publish all the exposed container ports to random ports on the Docker host
- `-e`: is how you pass environment variables to the container
- `--name`: allows you to specify a container name
- `AUTHOR`: is the environment variable name and Your Name is the value that you can pass

Running the `run` command with the `-it` flags attaches us to an interactive tty in the container.  is short for `--interactive + --tty`

Stop a running container through SIGTERM
`docker container stop web`

Stop a running container through SIGKILL
`docker container kill web`

List the running containers (add `--all` to include stopped containers)
`docker container ls`

`docker ps`

Stop and remove all containers
`docker container stop $(docker container ls -aq)`

Delete all running and stopped containers
`docker container rm -f $(docker ps -aq)`

Allows you to remove containers based on a certain condition using the --filter option.
`docker container prune --filter "until=12h"`

Print the last 100 lines of a container’s logs
`docker container logs --tail 100 web`


## Ports

See the ports by running the docker port command.
`docker port <CONTAINER_NAME>`
## Network

The default Docker network configuration allows for the isolation of containers on the Docker host. This feature enables you to build and configure apps that can communicate securely with each other.
Docker provides three pre-configured network configurations:

- Bridge
- Host
- none

List the networks
`docker network ls`

## Volumes vs Bind Mounts

- With Bind Mount, a file or directory on the host machine is mounted into a container. The file or directory is referenced by its full or relative path on the host machine.
- With Volume, a new directory is created within Docker's storage directory on the host machine, and Docker manages that directory's content.

List all the volumes known to Docker.

`docker volume ls`
Create a volume
`docker volume create`

Display detailed information on one or more volumes
`docker volume inspect`

List volumes
`docker volume ls`

Remove all unused local volumes
`docker volume prune`

Remove one or more volumes
`docker volume rm`