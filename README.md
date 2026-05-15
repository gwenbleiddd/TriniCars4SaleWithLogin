# 🚗 Vehicle Marketplace System

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET-Core-purple)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![License](https://img.shields.io/badge/License-Academic-lightgrey)

**ITEC 342 – Final Project**

---

## 📌 Project Overview

A web-based vehicle marketplace built using **ASP.NET Core Razor Pages** and **SQL Server**.

The system allows users to:
- Register and manage accounts
- Submit vehicle listings with images
- Edit and update listings
- View approved vehicles

Administrators can:
- Review submissions
- Approve or reject listings
- Manage platform visibility

---

## ✨ Features

- 🔐 User authentication and registration
- 🧑‍⚖️ Role-based authorisation (Admin / User)
- 🚘 Vehicle listing management
- 🖼️ Image upload (thumbnail + gallery)
- ✔️ Admin approval workflow
- ✏️ Edit vehicle submissions
- 📱 Responsive UI
- 🗄️ SQL Server database integration
- ⚙️ Entity Framework Core ORM
- 🔑 ASP.NET Core Identity system

---

## 🛠️ Technology Stack

- ASP.NET Core (Razor Pages)
- C#
- Entity Framework Core
- SQL Server / LocalDB
- ASP.NET Core Identity
- HTML5
- CSS3
- JavaScript

---

## 📸 Screenshots

> Add your images here (recommended for portfolio impact)

### Home Page
![Home Page](screenshots/home.png)

### Vehicle Details
![Details Page](screenshots/details.png)

### Admin Dashboard
![Admin Page](screenshots/admin.png)

---

## 🚀 Setup

### Prerequisites

- Visual Studio 2022 or later
- .NET 8 SDK

### Installation Steps

1. Clone the repository
```bash
git clone <repository-url>
```

2. Open solution in Visual Studio

3. Restore dependencies
```bash
dotnet restore
```

4. Apply database migrations
```bash
dotnet ef database update
```

5. Configure Default Admin Access

   The system seeds default accounts on startup. These are required to access admin features such as vehicle approval.


   Configure the seeded password using user secrets:

```bash
dotnet user-secrets set "SeedUserPW" "Password123!"
```

- **Admin Email:** admin@contoso.com  
- **Manager Email:** manager@contoso.com 


6. Run the application
```bash
dotnet run
```

---

## 👤 Usage

### User Flow
- Register or log in
- Create vehicle listings
- Upload images
- Edit submissions
- Browse approved vehicles

### Admin Flow
- Log in as admin
- Review listings
- Approve or reject vehicles
- Manage visibility

---

## 🧠 Project Notes

- Images are stored in `wwwroot/Images`
- Vehicle listings require admin approval before public visibility
- Identity system handles authentication and role management

---

## 📄 License

This project is developed for academic purposes as part of **ITEC 342**.

## 👤 Author

Dominic Salandy  
Final-year BSc Information Technology Student  
GitHub: https://github.com/gwenbleiddd
