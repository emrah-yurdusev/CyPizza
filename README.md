# 🍕 Pizza Management System

A desktop-based restaurant management system developed using **C# WinForms** and **MySQL**. The system is designed to simplify restaurant operations by providing separate interfaces for administrators, cashiers, chefs, and stock management.

## 🚀 Features

### 👨‍💼 Administrator Panel

* Employee management (Add, Update, Remove)
* User account management
* Staff role assignment
* Access to different system modules
* Secure admin-only controls

### 💰 Cashier Panel

* Create customer orders
* Select menu items, extras, and drinks
* Calculate order totals automatically
* Manage payments and change calculations
* Send orders to the kitchen queue

### 👨‍🍳 Chef Panel

* View active order queue in real time
* Monitor incoming customer orders
* Mark completed orders as finished
* Remove completed orders from the queue

### 📦 Stock Management Panel

* Add new products
* Update product information
* Remove products from inventory
* Manage product quantities and pricing
* Monitor available stock

## 🏗️ System Architecture

The application consists of four main modules:

| Module        | Description                           |
| ------------- | ------------------------------------- |
| Admin Panel   | Employee and system management        |
| Cashier Panel | Order creation and payment operations |
| Chef Panel    | Kitchen order queue management        |
| Stock Panel   | Inventory and product management      |

All modules communicate with a MySQL database to ensure data consistency across the system.

## 🛠️ Technologies Used

* C#
* WinForms
* MySQL
* ADO.NET
* Visual Studio

## 🗄️ Database

The application uses a MySQL database for:

* Employee records
* User authentication
* Product information
* Order tracking
* Inventory management

The database creation script is included in the repository.

## 📷 Screenshots

### Administrator Panel

![Admin Panel](screenshots/admin_panel.png)

### Cashier Panel

![Cashier Panel](screenshots/cashier_panel.png)

### Chef Panel

![Chef Panel](screenshots/chef_panel.png)

### Stock Management Panel

![Stock Panel](screenshots/stock_panel.png)

## ⚙️ Installation

1. Clone the repository.
2. Import the provided SQL file into MySQL.
3. Update the connection string if required.
4. Open the solution file in Visual Studio.
5. Build and run the application.

## 🎯 Purpose

This project was developed as an academic software engineering project to demonstrate desktop application development, database integration, role-based access control, and restaurant workflow management.
