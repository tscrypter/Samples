# Kubernetes Native Service Discovery

ASP.NET Core sample app illustrating how to use Kubernetes native service discovery for registering micro services. The fortune-teller.yml registers the fortune-teller-service and fortune-teller-ui services with Kubernetes upon deployment.

Note: You require a kubernetes cluster that supports a LoadBalancer service type.

## Pre-requisites - Kubernetes

1. Installed *kubectl* configured to connect to your k8s cluster

## Publish App to a public registry

1. `export DOCKER_REGISTRY=registry/`
1. `cd samples/Discovery/src/Kubernetes/`
1. `docker-compose build`
1. `docker push registry/fortunetellerservicek8s:latest`
1. `docker push registry/fortunetelleruik8s:latest`
1. `kubectl apply -f fortune-teller.yml`

## What to expect - Kubernetes

After building and running the app, you should see something like the following in the logs.

To see the logs of the service as you startup and use the app: `kubectl logs -f deployment.apps/fortune-teller-service`
To see the logs of the ui as you startup and use the app: `kubectl logs -f deployment.apps/fortune-teller-ui`

From the service, you should see something like this during startup:

```bash
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 3.1.0 initialized 'FortuneContext' using provider 'Microsoft.EntityFrameworkCore.InMemory' with options: StoreName=Fortunes 
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 0 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 50 entities to in-memory store.
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/1.1 GET http://10.244.0.18:80/actuator/health/liveness  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished in 54.3886ms 200 application/json;charset=UTF-8
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/1.1 GET http://10.244.0.18:80/actuator/health/readiness  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished in 4.0587ms 200 application/json;charset=UTF-8
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/1.1 GET http://fortune-teller-service/api/fortunes/random  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint 'FortuneTellerService.Controllers.FortunesController.Random (Fortune-Teller-Service)'
info: Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker[3]
      Route matched with {action = "Random", controller = "Fortunes"}. Executing controller action with signature FortuneTellerService.Models.Fortune Random() on controller FortuneTellerService.Controllers.FortunesController (Fortune-Teller-Service).
```

From the ui, you should see something like this during startup:

```bash

```

At this point the Fortune Teller Service and Fortune Teller UI is up and running.

### See the Official [Steeltoe Service Discovery Documentation](https://steeltoe.io/docs/steeltoe-service-discovery) for a more in-depth walkthrough of the samples and more detailed information
