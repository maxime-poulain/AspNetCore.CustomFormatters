# Custom JSON Formatters in ASP.NET Core with Newtonsoft.Json

This repository demonstrates how to add custom JSON formatters to an 
ASP.NET Core application. These formatters specifically target a single 
assembly and are based on the `Newtonsoft.Json library`. They provide 
fine-grained control over the serialization and deserialization process.

## Why is this important?

In scenarios where we deal with classes with circular references and 
polymorphism (like in our 'Facade' assembly), standard serialization and 
deserialization can lead to issues.

We can use the `PreserveReferencesHandling` and `TypeNameHandling` settings 
of our JSON serializer to handle these situations. However, we might not want 
these settings to be applied globally. Our custom formatters come into play here.

## How does it work?

We create and implement custom input and output formatters in ASP.NET Core 
to apply specific serialization and deserialization rules to a given assembly. 
In this case, we apply these rules to the 'Facade' assembly.

Here's an overview of the classes involved:

1. `FacadeInputFormatter` & `FacadeOutputFormatter`: These are the custom 
   formatters that inherit from `NewtonsoftJsonInputFormatter` and 
   `NewtonsoftJsonOutputFormatter`. They ensure their use is restricted to 
   actions within the 'Facade' assembly.

2. `NewtonsoftMvcOptionsRegistration`: This class configures the MVC options 
   to insert our custom formatters at the beginning of the formatter lists.
