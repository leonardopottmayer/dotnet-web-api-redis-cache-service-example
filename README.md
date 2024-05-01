# Example implementation of a Redis cache service in a .NET 8 ASP.NET Web API

## Prerequirements

* Visual Studio 2022
* .NET Core SDK
* Redis database

## Database configuration

This project accesses a Redis database using the WebApi.Cache.Redis project.
To setup a free Redis database, use the following link (https://app.redislabs.com/#/databases).
After configuring the database, you need to get the connection string and add it do appsettings.json.

## How To Run

* Open solution in Visual Studio 2022
* Set .Api project as Startup Project and build the project.
* Run the application.

## API routes
The web api has 4 routes to test the Redis connection. If running on debug, swagger will show the endpoints.