# NSW RFS Web API base template

This is the NSW RFS Web API base template. It provides the following features:

* Auth0 integration.
* Status (ping and health) endpoints.
* Secrets.config for storing secret data not to be committed to a repository [see here](http://www.mattburkedev.com/keep-your-azure-secrets-safely-out-of-git/).
* WebDeploy Package setup for AWS Elastic Beanstalk application.
* CORS is enabled for *.rfs.nsw.gov.au.

## General rules

1. All api methods should have a return type of `IHttpActionResult`.
2. All app settings are persisted in `Secrets.Config`. This file is not committed to the git repository.
3. Do not include anything in the application code which you would not be happy with being publicly available.
4. Routes are specified using attributes and are versioned (e.g. /api/v1/controller/action).
5. We deal with JSON return types by default.

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
	if (!IsValid(viewmodel))
	{
		return this.UnprocessibleEntity();
	}

	...
}
```

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

## Setting up your application

1. Set up an Auth0 application.
2. Put the Auth0 client secret and id into secrets.config. This is excluded by the `.gitignore`.
3. Put your controllers in the correct version folder under the `Controllers` folder (e.g. V1).
4. Put your view models in the correct version folder under the `Models` folder (e.g. V1).