# Mister Robot

Mister Robot is an e-commerce web application built with **ASP.NET Core MVC** for browsing, comparing and purchasing computer hardware.

## Project Purpose

This project was built as a hands-on learning experience to explore how a complete e-commerce application can be designed and developed using the .NET ecosystem.

## Technologies

- .NET 10
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor Views
- Stripe
- HTML / CSS / JavaScript

## Features

- User authentication and authorization
- Admin and user roles
- Product inventory
- Product search and categories
- Shopping cart
- Wishlist
- Stripe checkout
- Order history
- Product reviews and ratings
- Product comparison
- Dynamic product specifications
- Admin product management
- Contact messages and admin replies

## Screenshots

<table>

<tr>

<td width="50%" align="center">
<h3>Home Page</h3>
<img src="Images%20used%20for%20demo/ReadMe/HOME.png" width="100%" />
<p>
Landing page with quick access to the main areas of the application.
</p>
</td>

<td width="50%" align="center">
<h3>Inventory</h3>
<img src="Images%20used%20for%20demo/ReadMe/INVENTORY.png" width="100%" />
<p>
Browse products, search by name, filter by category, add items to the cart or wishlist.
</p>
</td>

</tr>

<tr>

<td width="50%" align="center">
<h3>Product Page</h3>
<img src="Images%20used%20for%20demo/ReadMe/PRODUCT_PAGE.png" width="100%" />
<p>
Detailed product information including price, stock, category and technical specifications.
</p>
</td>

<td width="50%" align="center">
<h3>Reviews</h3>
<img src="Images%20used%20for%20demo/ReadMe/COMPARE_PRODUCT_AND_REVIEW.png" width="100%" />
<p>
Authenticated users can rate products and leave written reviews.
</p>
</td>

</tr>

<tr>

<td width="50%" align="center">
<h3>Product Comparison</h3>
<img src="Images%20used%20for%20demo/ReadMe/COMPARE_PRODUCT_PAGE.png" width="100%" />
<p>
Products from the same category can be compared side by side.
</p>
</td>

<td width="50%" align="center">
<h3>Feature Comparison</h3>
<img src="Images%20used%20for%20demo/ReadMe/COMPARE_PRODUCT_FEATURES.png" width="100%" />
<p>
Technical specifications are dynamically displayed and compared between products.
</p>
</td>

</tr>

<tr>

<td width="50%" align="center">
<h3>Shopping Cart</h3>
<img src="Images%20used%20for%20demo/ReadMe/CART.png" width="100%" />
<p>
Manage product quantities, view the total price and continue to Stripe checkout.
</p>
</td>

<td width="50%" align="center">
<h3>Order History</h3>
<img src="Images%20used%20for%20demo/ReadMe/ORDER_STRIPE.png" width="100%" />
<p>
Completed orders are stored in the database and displayed in the user's order history.
</p>
</td>

</tr>

<tr>

<td width="50%" align="center">
<h3>Inventory Management</h3>
<img src="Images%20used%20for%20demo/ReadMe/INVENTORY_MANAGEMENT.png" width="100%" />
<p>
Admin dashboard for managing products, stock and inventory information.
</p>
</td>

<td width="50%" align="center">
<h3>Add Product</h3>
<img src="Images%20used%20for%20demo/ReadMe/ADD_PRODUCT.png" width="100%" />
<p>
Administrators can add new products with category, supplier, stock, image and pricing information.
</p>
</td>

</tr>

<tr>

<td width="50%" align="center">
<h3>Link Product Features</h3>
<img src="Images%20used%20for%20demo/ReadMe/LINK_FEATURE.png" width="100%" />
<p>
Technical specifications can be dynamically linked to products by administrators.
</p>
</td>

<td width="50%" align="center">
<h3>Contact Messages</h3>
<img src="Images%20used%20for%20demo/ReadMe/CONTACT_ADMIN.png" width="100%" />
<p>
Administrators can view contact messages and reply directly from the admin panel.
</p>
</td>

</tr>

</table>

## Stripe Payments

Stripe is used for checkout and payment processing.

After a successful checkout:

- The order is stored in the database
- Order details are saved
- The user can view the order in their order history

## What I Learned

While building this project I practiced:

- ASP.NET Core MVC
- Entity Framework Core
- Database migrations
- SQL Server relationships
- ASP.NET Core Identity
- Role-based authorization
- Dependency Injection
- Repository and Service patterns
- Stripe integration
- Shopping cart logic
- Wishlist functionality
- Product comparison
- Dynamic product features
- Admin dashboards
- Debugging and refactoring
- Migrating from .NET 8 to .NET 10

## Project Setup

After cloning the repository, configure the database connection and Stripe keys before running the application.

### 1. Clone the repository

```bash
git clone <repository-url>
cd MisterRobot
```

### 2. Restore dependencies

Using .NET CLI:

```bash
dotnet restore
```

### 3. Configure the database

Open `appsettings.json` and update the SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MisterRobotDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

If your SQL Server instance has a different name, update the `Server` value.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR-PC\\SQLEXPRESS;Database=MisterRobotDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Configure Stripe

Add your Stripe test keys in `appsettings.json`:

```json
{
  "Stripe": {
    "PublishableKey": "your_publishable_key",
    "SecretKey": "your_secret_key"
  }
}
```

> Use Stripe test keys during development and do not commit real secret keys to a public repository.

### 5. Create / Update the database

Using .NET CLI:

```bash
dotnet ef database update
```

OR using Package Manager Console:

```powershell
Update-Database
```

To view the available migrations:

Using .NET CLI:

```bash
dotnet ef migrations list
```

OR using Package Manager Console:

```powershell
Get-Migration
```

### 6. Run the application

Using .NET CLI:

```bash
dotnet run
```

OR run the project directly from Visual Studio.

The application will start on the local development URL shown in the terminal or Visual Studio.
