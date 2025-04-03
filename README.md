# Prism MVVM

## Overview
A simple WPF application built using the MVVM pattern with Prism, designed to showcase the capabilities of Prism.WPF, Prism.Core, and Prism.DryIoc. This project serves as a reference for building modular, maintainable, and testable WPF applications by leveraging Prism’s built-in functionalities such as dependency injection, event aggregation, and command handling.

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

## Technologies Used
- .NET Core
- WPF with MVVM Pattern
- Prism.WPF, Prism.Core, Prism.DryIoc

## Project Structure
```
PrismMVVM/
│-- App.xaml, App.xaml.cs
│-- EventAggregator/
│   ├── EmployeeEvent.cs
│-- Models/
│   ├── Employee.cs
│-- Services/
│   ├── IEmployeeService.cs
│   ├── EmployeeService.cs
│-- ViewModels/
│     │-- Dialogs/
│       ├── DialogViewModel.cs
│   ├── ISumViewModel.cs
│   ├── LoginViewModel.cs
│   ├── MainViewModel.cs
    ├── SumViewModel.cs
│-- ViewResources/
│   ├── Styles.xaml
│-- Views/
│     │-- Dialogs/
│       ├── DialogView.xaml
│   ├── LoginView.xaml
│   ├── MainView.xaml
│   ├── SumView.xaml
```

## Installation
### Prerequisites
- .NET SDK 6.0 or later

### Steps
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd PrismMVVM
   ```
2. Install dependencies:
   ```sh
   dotnet restore
   ```

## Usage
1. Run the project:
   ```sh
   dotnet run --project PrismMVVM
   ```
2. Login with any username/password.
