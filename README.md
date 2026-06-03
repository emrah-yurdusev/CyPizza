# 🍕 Restaurant Management System

A desktop-based restaurant management system developed using **C# WinForms** and **MySQL**. The application is designed to manage restaurant operations through separate interfaces for administrators, cashiers, chefs, and inventory management. The system includes role-based authentication, order processing, kitchen queue management, and stock control.

---

## 🚀 Features

### 🔐 Role-Based Authentication System

The application uses a database-driven login system.

Employees log in using credentials stored in the MySQL database. After successful authentication, the system automatically opens the appropriate interface according to the employee's role.

Supported roles:

* Administrator
* Cashier
* Chef

This ensures that users only have access to the functions required for their responsibilities.

---

### 👨‍💼 Administrator Panel

The administrator panel is used to manage restaurant employees and system access.

Features:

* Add new employees
* Update employee information
* Remove employees
* Assign job roles
* Manage user accounts
* Access other system modules
* View employee records

---

### 💰 Cashier Panel

The cashier panel is responsible for customer order management and payment operations.

Features:

* Create new customer orders
* Select menu items
* Add extra products
* Add drinks
* Calculate total order cost
* Process customer payments
* Calculate change automatically
* Send orders to the kitchen queue
* View completed orders

---

### 👨‍🍳 Chef Panel

The chef panel allows kitchen staff to monitor and manage incoming orders.

Features:

* View active order queue
* Monitor incoming customer orders
* Track pending kitchen tasks
* Mark orders as completed
* Remove finished orders from the queue
* Refresh queue data in real time

---

### 📦 Stock Management Panel

The stock management panel helps administrators manage restaurant inventory.

Features:

* Add products to inventory
* Update product information
* Remove products
* Manage product quantities
* Manage product prices
* Monitor available stock
* View inventory records

---

## 🏗️ System Workflow

1. Employees log into the system using their credentials.
2. The application verifies user information through the MySQL database.
3. Based on the employee's role, the corresponding panel is opened automatically.
4. Cashiers create customer orders.
5. Orders are sent to the chef queue.
6. Chefs prepare and complete orders.
7. Administrators manage employees and inventory through dedicated panels.
8. All information is stored and synchronized using the MySQL database.

---

## 🛠️ Technologies Used

* C#
* Windows Forms (WinForms)
* MySQL
* ADO.NET
* Visual Studio

---

## 🗄️ Database

The application uses a MySQL database to store:

* Employee information
* Login credentials
* User roles
* Product information
* Inventory records
* Customer orders
* Order status information

A SQL database script is included in the repository to simplify setup and deployment.

## ⚙️ Installation

### Requirements

* Visual Studio
* MySQL Server
* .NET Framework

### Setup

1. Clone the repository:

```bash
git clone https://github.com/yourusername/restaurant-management-system.git
```

2. Import the provided SQL file into MySQL.

3. Update the database connection string if necessary.

4. Open the solution file (.sln) in Visual Studio.

5. Build and run the project.

---

## 🎯 Educational Purpose

This project was developed as an academic software engineering project to demonstrate:

* Desktop application development
* Database integration
* Role-based access control
* Inventory management
* Order processing systems
* Multi-user restaurant workflow management

---

## 👨‍💻 Author

**Emrah Yurdusev**

Software Engineering Student

European University of Lefke

**Ahmet Özçelebi**

Software Engineering Student

European University of Lefke

