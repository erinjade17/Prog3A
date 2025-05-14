# AgriEnergyConnect Prototype README

## Overview

AgriEnergyConnect is a prototype web application designed to connect farmers and potentially employees within an agricultural energy ecosystem. This application allows farmers to manage their products and employees to oversee farmer information and product listings.

This README provides step-by-step instructions for setting up the development environment, building and running the prototype, and understanding its functionalities and user roles.

## Table of Contents

1.  Setting Up the Development Environment
    * Prerequisites
    * Installation Steps
2.  Building and Running the Prototype
    * Building the Application
    * Running the Application
3.  System Functionalities and User Roles
    * Farmer Role
    * Employee Role
    * Public Access
4.  Database Configuration
5.  Logging
6.  Contributing
7.  License

## 1. Setting Up the Development Environment

This section guides you through the necessary steps to prepare your development environment to run the AgriEnergyConnect prototype.

### Prerequisites

Before you begin, ensure you have the following software installed on your machine:

* **Operating System:** Windows, macOS, or Linux.
* **.NET SDK:** Version 7.0 or later. You can download it from the official [.NET website](https://dotnet.microsoft.com/download).
* **Integrated Development Environment (IDE):**
    * **Visual Studio 2022 or later (Recommended for Windows and macOS):** Download from the [Visual Studio website](https://visualstudio.microsoft.com/). Make sure to include the ASP.NET and web development workload during installation.
    * **Visual Studio Code (Cross-platform):** Download from the [Visual Studio Code website](https://code.visualstudio.com/). Install the C# extension from the marketplace.
* **SQL Server LocalDB (if using the default database configuration):** This is usually installed with Visual Studio. If you are using a different SQL Server instance, ensure it is installed and configured.

### Installation Steps

Follow these steps to set up your development environment:

1.  **Clone the Repository:**
    If you have the project code in a Git repository, clone it to your local machine using a Git client or the command line:
    ```bash
    git clone <repository_url>
    cd <repository_directory>
    ```

2.  **Restore NuGet Packages:**
    The project relies on several NuGet packages (libraries). These need to be downloaded and installed.
    * **Using Visual Studio:** Open the solution file (`.sln`). Visual Studio should automatically restore the packages. If not, go to `Tools` > `NuGet Package Manager` > `Package Manager Console` and run the command:
        ```powershell
        Update-Package -reinstall
        ```
    * **Using Visual Studio Code:** Open the project folder. If prompted to restore packages, click `Yes`. Alternatively, open the terminal (`Ctrl+\`` or `Cmd+\``) and run the command:
        ```bash
        dotnet restore
        ```

3.  **Database Setup:**
    The prototype is configured to use SQL Server LocalDB by default.
    * **Check Connection String:** Open the `appsettings.json` file in the project root. Verify the `DefaultConnection` string under the `ConnectionStrings` section:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AgriEnergyConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true"
        }
        ```
        If you are using a different SQL Server instance, update this connection string accordingly.
    * **Apply Migrations:** Entity Framework Core Migrations are used to create and update the database schema. Open the Package Manager Console in Visual Studio (or the terminal in VS Code) and run the following commands in order:
        ```powershell
        Add-Migration InitialCreate
        Update-Database
        ```
        These commands will create the `AgriEnergyConnectDB` database (if it doesn't exist) and the necessary tables based on the application's models.

## 2. Building and Running the Prototype

This section explains how to build the application and run it on your local development server.

### Building the Application

* **Using Visual Studio:**
    * Open the solution file (`.sln`).
    * Go to `Build` > `Build Solution` (or press `Ctrl+Shift+B` / `Cmd+Shift+B`).
    * Check the `Output` window for any build errors.

* **Using Visual Studio Code:**
    * Open the project folder.
    * Open the terminal (`Ctrl+\`` or `Cmd+\``).
    * Run the build command:
        ```bash
        dotnet build
        ```
    * Check the terminal output for any build errors.

### Running the Application

* **Using Visual Studio:**
    * Ensure the project is selected in the Solution Explorer.
    * Press `F5` or go to `Debug` > `Start Debugging`. This will build the application (if needed) and launch it in your default web browser. The application will typically run on a local development server address (e.g., `https://localhost:xxxx`).

* **Using Visual Studio Code:**
    * Open the terminal (`Ctrl+\`` or `Cmd+\``).
    * Navigate to the project directory if you are not already there.
    * Run the run command:
        ```bash
        dotnet run
        ```
    * The terminal will display the URL where the application is running (e.g., `Now listening on: https://localhost:xxxx`). Open this URL in your web browser.

## 3. System Functionalities and User Roles

The AgriEnergyConnect prototype implements basic functionalities for different user roles:

### Farmer Role

Users registered with the "Farmer" role can:

* **View My Products:** See a list of the products they have added to the system. This is accessible via the "My Products" link in the navigation bar.
* **Add Product:** Add new products to the system, specifying the product name, category, and production date. This is accessible via the "Add Product" link in the navigation bar.

### Employee Role

Users registered with the "Employee" role can:

* **View Farmers:** See a list of all farmers registered in the system. This is accessible via the "View Farmers" link in the navigation bar.
* **Add Farmer:** Add new farmer profiles to the system, including their name, email, phone number, and location. This is accessible via the "Add Farmer" link in the navigation bar.
* **Filter Products:** Filter the list of all products based on category and/or a date range (from and to production dates). This functionality is available on the "Filter Products" page.

### Public Access

Users who are not logged in can:

* **View the Home Page:** A general landing page for the application.
* **Login:** Access the login page to sign in with their credentials.
* **Register:** Access the registration page to create a new account, choosing either the "Farmer" or "Employee" role.

## 4. Database Configuration

The application uses Entity Framework Core to interact with the database. The database connection string is located in the `appsettings.json` file. By default, it is configured to use SQL Server LocalDB.

You can modify the `DefaultConnection` string to connect to a different SQL Server instance if needed. Ensure that the specified database server is running and accessible.

The database schema is managed through Entity Framework Core Migrations. The `InitialCreate` migration sets up the initial tables for farmers and products. Any subsequent database schema changes will require creating new migrations and applying them using the `Add-Migration` and `Update-Database` commands in the Package Manager Console or terminal.

## 5. Logging

The application utilizes the built-in .NET logging framework. It is currently configured to log information to the console and the debug output during development.

You can configure different logging providers (e.g., file logging, external logging services) by modifying the `Program.cs` file. The `ILogger<AccountController>` instance in the `AccountController` demonstrates how to log events like successful logins and logouts.

## 6. Contributing

If you wish to contribute to this prototype, please follow these guidelines:

1.  Fork the repository on GitHub.
2.  Create a new branch for your feature or bug fix.
3.  Make your changes and ensure they are well-documented.
4.  Submit a pull request with a clear description of your changes.

## 7. License

This prototype is for demonstration purposes only and does not have a specific license at this time. Please contact the developers for any licensing inquiries.

---

Thank you for exploring the AgriEnergyConnect prototype!
