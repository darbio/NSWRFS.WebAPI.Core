# Web API base template

[![Build status](https://ci.appveyor.com/api/projects/status/1v6ski9047dcbm0j/branch/develop?svg=true)](https://ci.appveyor.com/project/NSWRuralFireService38646/nswrfs-webapi-core/branch/develop)

This is the Web API base template. It provides the following features:

* Auth0 integration.
* Status (ping and health) endpoints.
* Secrets.config for storing secret data not to be committed to a repository [see here](http://www.mattburkedev.com/keep-your-azure-secrets-safely-out-of-git/).
* WebDeploy Package setup for AWS Elastic Beanstalk application.
* CORS is enabled for URLs identified in the Web.Config CorsAllowedOrigins setting.
* JSON underscores serializer for viewmodels.
* NLog with file (TRACE) and Exceptionless (INFO) targets.
* Implemented WebAPI `IHttpActionResult`s for use in the API.

## General REST rules

This base API adhered to the [REST standards](REST-Rules.md).

## General .Net WebAPI rules

1. All api methods should have a return type of `IHttpActionResult`.
2. All app settings are persisted in `Secrets.Config`. This file is not committed to the git repository.
3. Do not include anything in the application code which you would not be happy with being publicly available.
4. Routes are specified using attributes and are versioned (e.g. /api/v1/controller/action).
5. We deal with JSON return types by default.

## IHttpActionResults 

The following `IHttpActionResult` methods are defined in the `BaseApiController`:

HTTP Status Code | Controller method | Description
-----------------|-------------------|------------
200 OK | OkList<T>(T list) | Returns a 200 OK with a list and populated headers
304 Not Modified | NotModified() | Used when HTTP caching headers are in play
405 Method not allowed | MethodNotAllowed() | When a HTTP method is being requested which is not allowed by the current user
410 Gone | Gone() | The resource at this endpoint is no longer available (Not to be confused with a 404).
415 Unsupported Media Type | UnsupportedMediaType() | If incorrect content type was provided as part of the request
422 Unprocessable Entity | UnsupportedEntity() | If validation failed

### List Response

This is a paginated list response with the following headers:

* `X-Total-Page-Count` the total count of pages which the full dataset represents.
* `X-Current-Page` the current page index represented as an integer of the total page count.
* `X-Total-Count` the total number of items in a dataset.
* `Link` a link which contains the pagniation options for the first, last, previous and next pages in a dataset.

### Validation & Unprocessable Entity Response

Validation is performed using the in built `ModelState` binder.

```
[HttpPost]
public IHttpActionResult CreatePerson(PersonViewModel_POST model)
{
    if (!ModelState.IsValid)
    {
        // Send a 422 response
        return this.UnprocessableEntity();
    }

  // Do the do
  ...

    // Return
    return this.Created(Uri("https://www.rfs.nsw.gov.au/api/v1/people/1234"), person)
}
```

The Unprocessable Entity Response includes the ModelState errors. It is automatically created when `this.UnprocessableEntity()` is called.

The JSON response looks like this:

```
{
  "message" : "A validation error occurred.",
  "description" : "Validation Failed",
  "errors" : [
  {
    "field_name" : "first_name",
  "message" : "First Name must begin with J"
  },
  {
    "field_name" : "first_name",
  "message" : "First Name can't be too fancy"
  }]
}
```

## Logging

This project uses NLog for logging to the following targets:

* Local file (TRACE and above)
* ExceptionLess (INFO and above)

The `BaseApiController` has an `NLog` property which can be used for writing logs.

An exception logger is registered into the web application which will log all unhandled exceptions to NLog. This is implemented in `NLogExceptionHandler`.

```
public IHttpActionResult Post(EntityViewModel_POST viewmodel)
{
    ...

    // Make a log entry
  Nlog.Log(LogLevel.Info, "I'm a genie in a bottle, baby.");

  ...
}
```

In addition, each request and response from the API are logged as an `Trace` message by the `LogAllActionFilter`.

## Error responses

A global exception filter `ExceptionHandlingAttribute` is implemented on all `ApiControllers`, this will process a 500 error response to the Api client.

If the exception type thrown is inherant from `BusinessException` the exception message will be shown to the client. If not, a generic 500 error is shown. This is by design and protects the internals of the application.

## API Methods

All API methods should return `IHttpActionResults`. The `BaseApiController` defines an extended number of methods to help with this.

For example:

```
public IHttpActionResult Validate(EntityViewModel_POST viewmodel)
{
    // Validate the viewmodel
  if (!ModelState.IsValid)
  {
    return this.UnprocessibleEntity(ModelState);
  }

  ...
}
```

## CORS

Generally Web APIs will be accessed from websites other than themselves. To allow this, this API project has a Web.Config setting (`NSWRFS.CorsAllowedOrigins`) which allows you to specify which hosts are allowed to request data from this API.

The `NSWRFS.CorsAllowedOrigins` setting is a comma separated string of URLs, which are added to the API CORS allowed origins on startup. When the application is run in debug mode, all origins are allowed. This is implemented using compiler directives.

For example: `https://www.rfs.nsw.gov.au,https://incidents.rfs.nsw.gov.au`.

## Models

All controllers should transfer data via a view model, unless a specific type (e.g. string, bool, int etc.) is more pragmatic.

The viewmodel should follow this general naming convention:

* `{Name}ViewModel_{HTTPMETHOD}`

An example of a good return object:

```
namespace NSWRFS.Base.Api.Models
{
    public class HealthViewModel_GET
    {
        /// <summary>
        /// Overall health status. True if all downstream systems are healthy, False is any downstream systems are not healthy.
        /// </summary>
        public bool IsHealthy { get; set; }
    }
}
```

When the object is serialized into JSON, the serializer uses underscore format. For example `IsHealthy` becomes `is_healthy`. This is performed by the `LowerCaseDelimitedPropertyNamesContractResolver`.

## Authentication

By default, all API controllers require authorization.
