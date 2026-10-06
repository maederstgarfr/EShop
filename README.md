# EShop

A full-featured e-commerce web application built with **ASP.NET Core MVC** and **Entity Framework Core**.

The project follows a **layered architecture** and uses the **Repository Pattern** to separate business logic, data access, and presentation layers.

## Features

* User registration and authentication
* Role-based authorization and permission management
* Product management
* Category and brand management
* Product variants and attributes
* Shopping cart
* Checkout
* Online payment
* Order management and order tracking
* Product reviews and comments
* Product discount management
* Article and blog management
* Admin dashboard
* User panel
* Image upload and management

## Technologies

* **C#**
* **ASP.NET Core MVC**
* **Entity Framework Core**
* **SQL Server**
* **Bootstrap**
* **JavaScript**
* **HTML5 / CSS3**

## Architecture

The project is organized into three main layers:

```text
EShop
│
├── EShop.Web
│   ├── Areas
│   ├── Controllers
│   ├── Views
│   └── wwwroot
│
├── EShop.Application
│   ├── Services
│   ├── Interfaces
│   ├── DTOs
│   └── Utilities
│
└── EShop.Data
    ├── Entities
    ├── Repositories
    ├── DbContext
    └── Migrations
```

### EShop.Web

The presentation layer containing MVC controllers, views, areas, and static files.

### EShop.Application

Contains business logic, application services, interfaces, DTOs, and application utilities.

### EShop.Data

Contains entities, Entity Framework Core configuration, repositories, database context, and migrations.

## Getting Started

### Prerequisites

Make sure you have the following installed:

* **Visual Studio 2019 or later**
* **.NET 5 SDK**
* **SQL Server**

### 1. Clone the repository

```bash
git clone https://github.com/USERNAME/EShop.git
cd EShop
```

### 2. Configure the database

The database itself is **not included in this repository**.

Open:

```text
EShop.Web/appsettings.json
```

and configure the SQL Server connection string for your local environment.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=EShopDb;Trusted_Connection=True;"
  }
}
```

> Do not use or commit real passwords, API keys, payment credentials, or other sensitive information.

### 3. Create the database

The project's Entity Framework Core migrations are included in the repository.

Open **Package Manager Console** in Visual Studio and run:

```powershell
Update-Database
```

This will create the database and apply the existing migrations.

### 4. Run the application

Set **EShop.Web** as the startup project and run the application from Visual Studio.

```text
Ctrl + F5
```

## Configuration

Depending on your local environment, you may need to configure:

* SQL Server connection string
* Payment gateway settings
* Authentication settings
* Other external service configurations

Sensitive configuration values should be stored locally and should not be committed to the repository.

## User Panel

Registered users can:

* Create an account
* Log in
* Browse products
* Add products to the shopping cart
* Complete checkout
* Place orders
* View their order history
* Track order status
* Submit product reviews

## Admin Panel

Administrators can:

* Manage products
* Manage categories
* Manage brands
* Manage product variants
* Manage orders
* Manage users
* Manage permissions
* Manage product comments
* Manage discounts
* Manage articles and blog content

## Database

The database is intentionally excluded from the repository.

Entity Framework Core migrations are included so the database can be created locally using:

```powershell
Update-Database
```

## Screenshots

Screenshots of the application will be added here.

## Notes

This project was developed as a portfolio and educational project to practice building a real-world e-commerce application with **ASP.NET Core MVC**, **Entity Framework Core**, layered architecture, repository pattern, authentication, authorization, and order management.

## License

This project is intended for educational and portfolio purposes.
