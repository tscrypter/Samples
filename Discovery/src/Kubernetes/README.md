# Kubernetes Native Service Discovery

ASP.NET Core sample app illustrating how to use [Kubernetes native service discovery](https://kubernetes.io/docs/concepts/services-networking/service/#discovering-services) for registering micro services. 
The fortune-teller.yml registers the fortune-teller-service and fortune-teller-ui services with Kubernetes upon deployment.

**Note:** You require a kubernetes cluster that supports a LoadBalancer service type.  
**Note:** You must register your service's discovery name to match the name of the Service resource in Kubernetes

## Pre-requisites - Kubernetes

1. Installed *kubectl* configured to connect to your k8s cluster
1. The service account of your containers must have the correct RoleBindings if RBAC is enabled ([see fortune-teller.yml](fortune-teller.yml))

## Publish App to a public registry

1. `cd samples/Discovery/src/Kubernetes/`
1. `DOCKER_REGISTRY=registry/ docker-compose build`
1. `docker push registry/fortunetellerservicek8s:latest`
1. `docker push registry/fortunetelleruik8s:latest`
1. `kubectl apply -f fortune-teller.yml`
    - Alter this yaml file to match the registry name you defined in step 2

## What to expect - Kubernetes

After building and running the app, you should see something like the following in the logs.

To see the logs of the service as you startup and use the app: `kubectl logs -f deployment.apps/fortune-teller-service`
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
      Request starting HTTP/1.1 GET http://10.244.0.21:80/actuator/health/liveness  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished in 58.0106ms 200 application/json;charset=UTF-8
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/1.1 GET http://10.244.0.21:80/actuator/health/readiness  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint '/actuator/health/{**_} HTTP: Get'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished in 5.4058ms 200 application/json;charset=UTF-8  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint 'FortuneTellerService.Controllers.FortunesController.Random (Fortune-Teller-Service)'
info: Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker[3]
      Route matched with {action = "Random", controller = "Fortunes"}. Executing controller action with signature FortuneTellerService.Models.Fortune Random() on controller FortuneTellerService.Controllers.FortunesController (Fortune-Teller-Service).
```

To see the logs of the ui as you startup and use the app: `kubectl logs -f deployment.apps/fortune-teller-ui`
From the ui, you should see something like this during startup:

```bash
warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
      Storing keys in a directory '/root/.aspnet/DataProtection-Keys' that may not be persisted outside of the container. Protected data will be unavailable when container is destroyed.
info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using '/root/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[58]
      Creating key {3a880999-9b14-49ba-990b-00b8788d5ab4} with creation date 2020-08-14 23:05:16Z, activation date 2020-08-14 23:05:16Z, and expiration date 2020-11-12 23:05:16Z.
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {3a880999-9b14-49ba-990b-00b8788d5ab4} may be persisted to storage in unencrypted form.
info: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[39]
      Writing data to file '/root/.aspnet/DataProtection-Keys/key-3a880999-9b14-49ba-990b-00b8788d5ab4.xml'.
Hosting environment: Production
Content root path: /app
Now listening on: http://[::]:80
Application started. Press Ctrl+C to shut down.
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/1.1 GET http://52.247.23.142/  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint 'Fortune_Teller_UI.Controllers.HomeController.Index (Fortune-Teller-UI)'
info: Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker[3]
      Route matched with {action = "Index", controller = "Home"}. Executing controller action with signature Microsoft.AspNetCore.Mvc.IActionResult Index() on controller Fortune_Teller_UI.Controllers.HomeController (Fortune-Teller-UI).
info: Microsoft.AspNetCore.Mvc.ViewFeatures.ViewResultExecutor[1]
      Executing ViewResult, running view Index.
info: Microsoft.AspNetCore.Mvc.ViewFeatures.ViewResultExecutor[4]
      Executed ViewResult - view Index executed in 67.7613ms.
info: Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker[2]
      Executed action Fortune_Teller_UI.Controllers.HomeController.Index (Fortune-Teller-UI) in 172.9401ms
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint 'Fortune_Teller_UI.Controllers.HomeController.Index (Fortune-Teller-UI)'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished in 236.8718ms 200 text/html; charset=utf-8
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/1.1 GET http://52.247.23.142/random  
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
```

At this point the Fortune Teller Service and Fortune Teller UI is up and running.

### See the Official [Steeltoe Service Discovery Documentation](https://steeltoe.io/docs/steeltoe-service-discovery) for a more in-depth walkthrough of the samples and more detailed information