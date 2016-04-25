# Tracing.NET
<img src="https://raw.githubusercontent.com/thomasgalliker/Tracing.NET/master/Tracing.png" width="100" height="100" alt="Tracing.NET" align="right">
Tracing.NET provides basic logging/tracing infrastructure such as an abstracted logger interface ITracer as well as platform specific tracer implementations.

### Download and Install Tracing.NET
This library is available on NuGet: https://www.nuget.org/packages/Tracing.NET/
Use the following command to install Tracing.NET using NuGet package manager console:

    PM> Install-Package Tracing

You can use this library in any .Net project which is compatible to PCL (e.g. Xamarin Android, iOS, Windows Phone, Windows Store, Universal Apps, etc.)

### API Usage
ITracer is the ultimate abstraction of the tracing implementation. 

#### Create an ITracer instance
A static class ```Tracer``` can be used to create instances of ITracer. 
```
// Create an ITracer with 'this' as target name
ITracer tracer = Tracer.Create(this);

// Create an ITracer with MyClass as generic target name
ITracer tracerGeneric = Tracer.Create<MyClass>();

// Create an ITracer with "MyClass" string as target name
ITracer tracerStringName = Tracer.Create("MyClass");
```

#### Inject ITracer instances using dependency injection
Instead of using the static ```Tracer```, it is recommended to use a dependency injection framework in order to create and inject ITracer instances into your classes.

### License
This project is Copyright &copy; 2016 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.
