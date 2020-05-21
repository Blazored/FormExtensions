# FormExtensions
A library with some Form Extensions for Blazor:

- LabelText

## Project

[![Build Status](https://dev.azure.com/blazored/FormExtensions/_apis/build/status/Blazored.FormExtensions?branchName=master)](https://dev.azure.com/blazored/FormExtensions/_build/latest?definitionId=15&branchName=master)

[![NuGet Badge Blazored.FormExtensions](https://buildstats.info/nuget/Blazored.FormExtensions)](https://www.nuget.org/packages/Blazored.FormExtensions)

### Installing

You can install from Nuget using the following command:

`Install-Package Blazored.FormExtensions`

Or via the Visual Studio NuGet package manager.

## Usage
Start by add the following using statement to your root `_Imports.razor`.

```csharp
@using Blazored.FormExtensions
```

## Usage - LabelText
You can use this within a `EditForm` component:

``` html
<EditForm Model="@Model">
    <p>
        <LabelText For="@(() => Model.First)" />
        <InputText @bind-Value="@Model.First" />
    </p>

    <p>
        <LabelText For="@(() => Model.Last)" />
        <InputText @bind-Value="@Model.Last" />
    </p>

    <p>
        <LabelText For="@(() => Model.EmailAddress)" />
        <InputText @bind-Value="@Model.EmailAddress" />
    </p>
</EditForm>

@code {
    Person Model { get; set; } = new Person();
}
```

## Configuration

### ServiceSide Blazor
Make sure to setup and configure the `IStringLocalizer` correctly in the `Startup.cs` like:
``` diff
public void ConfigureServices(IServiceCollection services)
{
+   services.AddRazorPages().AddViewLocalization(options => options.ResourcesPath = "Resources");
+   services.AddLocalization(options => options.ResourcesPath = "Resources");
+   services.AddSingleton(typeof(IStringLocalizer), typeof(StringLocalizer<SharedLocalization.SharedResources>));
}
```

### ClientSide (WebAssembly) Blazor
Make sure to setup and configure the `IStringLocalizer` correctly in the `Program.cs` like:
``` diff
public void ConfigureServices(IServiceCollection services)
{
+   builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
+   builder.Services.AddSingleton(typeof(IStringLocalizer), typeof(StringLocalizer<SharedLocalization.SharedResources>));
}
```
