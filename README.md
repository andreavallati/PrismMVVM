# Prism MVVM

## Overview
A simple WPF application built using the MVVM pattern with Prism, designed to showcase the capabilities of Prism.WPF, Prism.Core, and Prism.DryIoc. This project serves as a reference for building modular, maintainable, and testable WPF applications by leveraging Prism’s built-in functionalities such as dependency injection, event aggregation, and command handling.

---

## Features
- Prism Packages: Uses Prism.Core, Prism.WPF, and Prism.DryIoc to demonstrate Prism’s capabilities in WPF application development.
- MVVM Architecture: Follows the Model-View-ViewModel (MVVM) pattern for a structured and maintainable codebase.
- BindableBase Implementation: ViewModels inherit from BindableBase to enable property change notifications.
- Event Aggregator: Uses Prism’s EventAggregator to facilitate communication between ViewModels without tight coupling.
- Command Handling: Implements Prism’s DelegateCommand for handling WPF commands in a clean and maintainable way.
- Dialog Service: Utilizes Prism’s IDialogAware interface for managing dialogs in a structured manner.
- Dependency Injection: Uses Prism’s built-in DI container to manage services and ViewModels efficiently.
- ViewModel Locator: Uses ViewModelLocator.AutoWireViewModel to automatically associate Views with their ViewModels.
- External Styles File: Collects and manages WPF styles in a separate Styles.xaml file for better UI consistency and maintainability.

---

## Feature Demonstrations

### 1. Dependency Injection (DI)

**Location:** `App.xaml.cs`, All ViewModels

- Services registered in `RegisterTypes()` method
- Constructor injection throughout the application
- Singleton and Transient lifetime management

```csharp
// Registration
containerRegistry.RegisterSingleton<IEmployeeService, EmployeeService>();

// Injection
public MainViewModel(IEmployeeService employeeService) { ... }
```

### 2. Event Aggregator Pattern

**Location:** `MainViewModel.cs` (Publisher), `SumViewModel.cs` (Subscriber)

- **Publisher:** MainViewModel publishes employee data when button is clicked
- **Subscriber:** SumViewModel receives and processes the data
- **Benefit:** No direct coupling between ViewModels

```csharp
// Publishing
_eventAggregator.GetEvent<EmployeeEvent>().Publish(Employees);

// Subscribing
_eventAggregator.GetEvent<EmployeeEvent>().Subscribe(OnEmployeeEvent);
```

**Try it:** Click "Send Event" button to see SumView update with calculated values.

### 3. Dialog Service

**Location:** `MainViewModel.cs`, `DialogViewModel.cs`

- MVVM-friendly modal dialogs
- Parameter passing to dialogs
- Result handling with callbacks
- Return values from dialogs

```csharp
_dialogService.ShowDialog(nameof(DialogView), parameters, result =>
{
    if (result.Result == ButtonResult.OK)
    {
        // Handle dialog result
    }
});
```

**Try it:** Click "Show Dialog" button to see modal dialog with parameter passing.

### 4. DelegateCommand with Validation

**Location:** `LoginViewModel.cs`

- Commands with CanExecute logic
- Automatic refresh using `ObservesProperty`
- Button enabling/disabling based on validation

```csharp
LoginCommand = new DelegateCommand(Login, CanLogin)
    .ObservesProperty(() => UserName)
    .ObservesProperty(() => Password);
```

**Try it:** Enter username and password to enable the Login button.

### 5. ViewModelLocator

**Location:** All XAML Views

- Automatic View-ViewModel wiring
- No code-behind required
- Convention-based naming resolution

```xaml
<Window prism:ViewModelLocator.AutoWireViewModel="True">
```

---

## Code Walkthrough

### Application Startup Flow

1. **App.xaml.cs** - Prism bootstraps the application
2. **RegisterTypes()** - Services and dialogs registered with DI container
3. **CreateShell()** - LoginView created as initial window
4. **ViewModelLocator** - LoginViewModel automatically injected
5. User logs in → **MainView** shown via container resolution
6. **MainViewModel** loads employees and sets up event handlers

### Key Prism Interfaces

| Interface | Purpose | Implementation |
|-----------|---------|----------------|
| `BindableBase` | Property change notification | All ViewModels |
| `IDialogAware` | Dialog lifecycle management | DialogViewModel |
| `IContainerProvider` | Service resolution | ViewModels |
| `IEventAggregator` | Pub/sub messaging | Main & Sum ViewModels |
| `IDialogService` | Modal dialog hosting | MainViewModel |

---

## Technologies Used
- .NET Core
- WPF with MVVM Pattern
- Prism.WPF, Prism.Core, Prism.DryIoc

---

## Project Structure
```
PrismMVVM/
├── Models/
│   └── Employee.cs                 # Data models
├── ViewModels/
│   ├── MainViewModel.cs            # Main window ViewModel (DI, Events, Dialogs)
│   ├── LoginViewModel.cs           # Login ViewModel (Commands, Validation)
│   ├── SumViewModel.cs             # Subscriber ViewModel (Event Aggregator)
│   ├── Dialogs/
│   │   └── DialogViewModel.cs      # Dialog ViewModel (IDialogAware)
│   └── Interfaces/
│       └── ISumViewModel.cs        # ViewModel interface
├── Views/
│   ├── MainView.xaml               # Main window View
│   ├── LoginView.xaml              # Login View (Shell)
│   ├── SumView.xaml                # Child UserControl View
│   └── Dialogs/
│       └── DialogView.xaml         # Dialog View
├── Services/
│   ├── IEmployeeService.cs         # Service interface
│   └── EmployeeService.cs          # Service implementation
├── EventAggregator/
│   └── EmployeeEvent.cs            # Custom PubSubEvent
├── ViewResources/
│   └── Styles.xaml                 # Shared styles
└── App.xaml.cs                     # Prism bootstrapping
```

---

## Installation
### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later
- Basic knowledge of C# and WPF

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/andreavallati/PrismMVVM.git
   cd PrismMVVM
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Login with any username/password**

---

<div align="center">

**Happy Coding!**

</div>
