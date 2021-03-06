# Exam
Exam 2C2P


This is solution template for creating a single page app(SPA) with angular 8 and .net core 3 following  the principles of clean architecture.

## Technologies
* .NET Core 3
* ASP .NET Core 3
* Entity Framework Core 3
* Angular 8


### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.


### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### WebUI

This layer is a single page application based on Angular 8 and ASP.NET Core 3. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.
