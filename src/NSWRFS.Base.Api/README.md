# NSW RFS Web API base template

This is the NSW RFS Web API base template. It provides the following features:

* Auth0 integration.
* Status (ping and health) endpoints.
* Secrets.config for storing secret data not to be committed to a repository [see here](http://www.mattburkedev.com/keep-your-azure-secrets-safely-out-of-git/).

## General rules

1. All api methods should have a return type of `IHttpActionResult`.
2. All app settings are persisted in `Secrets.Config`. This file is not committed to the git repository.
3. Do not include anything in the application code which you would not be happy with being publicly available.
4. Routes are specified using attributes and are versioned (e.g. /api/v1/controller/action).

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