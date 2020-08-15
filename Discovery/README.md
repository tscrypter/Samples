# Steeltoe Discovery Sample Applications

This repo tree contains sample apps illustrating how to use the Steeltoe [Discovery](https://github.com/SteeltoeOSS/Discovery) packages.

### Eureka for service discovery
* `Eureka/src/Fortune-Teller-Service` - ASP.NET Core microservice illustrating how to use [Spring Cloud Eureka Server](https://projects.spring.io/spring-cloud/docs/1.0.3/spring-cloud.html#spring-cloud-eureka-server) for service registration.
* `Eureka/src/Fortune-Teller-UI` - ASP.NET Core MVC app illustrating how to use [Spring Cloud Eureka Server](https://projects.spring.io/spring-cloud/docs/1.0.3/spring-cloud.html#spring-cloud-eureka-server) for service discovery.

### Kubernetes native service discovery
* `Kubernetes/src/Fortune-Teller-Service` - ASP.NET Core microservice illustrating how to use [native Kubernetes](https://kubernetes.io/docs/concepts/services-networking/service/#discovering-services) service siscovery
* `Kuberentes/src/Fortune-Teller-UI` - ASP.NET Core MVC app illustrating how to use [native Kubernetes](https://kubernetes.io/docs/concepts/services-networking/service/#discovering-services) for service discovery.
* `Kubernetes/docker-compose.yaml` - A docker compose file used to build run a local registry, build images and push to the local registry. 

## Building & Running

See the Readme for instructions on building and running each app.

### See the Official [Steeltoe Service Discovery Documentation](https://steeltoe.io/docs/steeltoe-discovery/) for a more in-depth walkthrough of the samples and more detailed information
