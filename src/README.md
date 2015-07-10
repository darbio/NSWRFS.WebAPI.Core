# NSW RFS base WebAPI

## Setting up your application

1. Set up an Auth0 application.
2. Put the Auth0 client secret, Auth0 id and Exceptionless API key into secrets.config. This is excluded by the `.gitignore`.

```
<?xml version="1.0" encoding="utf-8"?>
<appSettings>
  <add key="Auth0.ClientID" value="" />
  <add key="Auth0.ClientSecret" value="" />
  <add key="ExceptionLess.ApiKey" value="" />
</appSettings>
```

3. Put your controllers in the correct version folder under the `Controllers` folder (e.g. V1).
4. Put your view models in the correct version folder under the `Models` folder (e.g. V1).