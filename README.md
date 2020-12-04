# Information

This project is meant to show the issue I am having with Dependency Injection using Catel.MVVM.

I am having 2 issues:
1. How do I auto inject the ViewModels into the Views automatically like the application [LogViewer](https://github.com/WildGums/LogViewer)?
   1. Currently the ViewModels are defined in the View like below.
```csharp
<Window.DataContext>
    <localVMs:MainViewModel/>
</Window.DataContext>
```

2. How do I use DI to inject the dependencies into the viewModels?
   1. If DI does not inject the services into the constructor of the ViewModel the title will read `Hello World! - DI NOT Working with MVVM`. 
   If DI does inject services into the ViewModel the title will read `DI is working with MVVM`.

The view and viewmodel is registered with `IViewModelLocator`.

```csharp
var viewModelLocator = _host.Services.GetRequiredService<IViewModelLocator>();
viewModelLocator.Register<MainView, MainViewModel>();
```

Opening MainView like so:
```csharp
var main = _host.Services.GetRequiredService<MainView>();
main.Show();
```

The MVVM naming conventions are followed according to the [Catel docs](https://docs.catelproject.com/vnext/catel-mvvm/locators-naming-conventions/naming-conventions/).

# Built With
* Catel.MVVM
* Catel.Fody
* Microsoft.Extensions.DependencyInjection
* Microsoft.Extensions.Hosting
* Serilog (**Logging is not tested.**)