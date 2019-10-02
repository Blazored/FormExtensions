# FormExtensions
A library with some Form Extensions for Blazor:

- LabelText

[![Build Status](https://dev.azure.com/blazored/FormExtensions/_apis/build/status/Blazored.FormExtensions?branchName=master)](https://dev.azure.com/blazored/FormExtensions/_build/latest?definitionId=11&branchName=master)

![Nuget](https://img.shields.io/nuget/v/blazored.formextensions.svg)

### Installing

You can install from Nuget using the following command:

`Install-Package Blazored.FormExtensions`

Or via the Visual Studio NuGet package manager.

## Usage
Start by add the following using statement to your root `_Imports.razor`.

```csharp
@using Blazored.FormExtensions
```

You can then use it as follows within a `EditForm` component.

```html
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

Also make sure to setup and configure the `IStringLocalizer` correctly like:
``` diff
public void ConfigureServices(IServiceCollection services)
{
+   services.AddRazorPages().AddViewLocalization(options => options.ResourcesPath = "Resources");
+	services.AddLocalization(options => options.ResourcesPath = "Resources");
+   services.AddSingleton(typeof(IStringLocalizer), typeof(StringLocalizer<SharedLocalization.SharedResources>));

	// more ...
}
```