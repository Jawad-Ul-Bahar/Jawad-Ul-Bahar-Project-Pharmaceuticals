# 🏥 InnovaMedix Pharmora - Pharmaceutical Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-green.svg)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0.6-orange.svg)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Express-lightblue.svg)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)]()

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Database Schema](#database-schema)
- [User Roles](#user-roles)
- [API Endpoints](#api-endpoints)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

**InnovaMedix Pharmora** is a comprehensive pharmaceutical management system built with ASP.NET Core MVC. This web application provides a complete solution for pharmaceutical companies to manage their products, handle customer inquiries, process career applications, and manage administrative tasks efficiently.

The system is designed to streamline pharmaceutical operations with features including product catalog management, customer contact handling, quote requests, career application processing, and a robust admin panel for system management.

## ✨ Features

### 🏠 **Public Features**
- **Homepage** - Modern, responsive landing page
- **About Us** - Company information and mission
- **Product Catalog** - Categorized pharmaceutical products with images
- **Contact Form** - Customer inquiry submission with email notifications
- **Quote Request** - Business quote submission system
- **User Registration & Login** - Secure user authentication
- **Career Applications** - Job application submission with file upload

### 🔐 **User Dashboard**
- **Profile Management** - Update personal information
- **Career Application Tracking** - View application status
- **Session Management** - Secure login/logout functionality

### 👨‍💼 **Admin Panel**
- **Dashboard** - System overview and statistics
- **Product Management** - CRUD operations for products and categories
- **Career Management** - Review, approve/reject applications
- **Contact Management** - View and respond to customer inquiries
- **Quote Management** - Process business quote requests
- **User Management** - Manage registered users
- **Admin Management** - Approve/reject admin registrations
- **Email System** - Send notifications and responses

### 📧 **Email Integration**
- **SMTP Configuration** - Gmail SMTP integration
- **Auto-notifications** - Contact form acknowledgments
- **Admin Alerts** - New submission notifications
- **Career Communications** - Application status updates

## 🛠 Technology Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| **.NET** | 8.0 | Core Framework |
| **ASP.NET Core MVC** | 8.0 | Web Framework |
| **Entity Framework Core** | 9.0.6 | ORM |
| **SQL Server** | Express | Database |
| **C#** | Latest | Programming Language |
| **HTML5/CSS3** | Latest | Frontend |
| **Bootstrap** | Latest | UI Framework |
| **JavaScript** | ES6+ | Client-side Logic |

## 📁 Project Structure

```
Project-Pharmaceuticals/
├── Controllers/                 # MVC Controllers
│   ├── AdminController.cs      # Admin panel functionality
│   ├── HomeController.cs       # Public pages and user features
│   ├── ProductsController.cs   # Product management
│   └── CategoriesController.cs # Category management
├── Models/                     # Data Models
│   ├── Product.cs             # Product entity
│   ├── Category.cs            # Category entity
│   ├── Contact.cs             # Contact form entity
│   ├── Quote.cs               # Quote request entity
│   ├── CareerRequest.cs       # Career application entity
│   ├── CandidateRegistration.cs # User registration
│   └── AdminLogin.cs          # Admin authentication
├── Views/                     # Razor Views
│   ├── Home/                  # Public pages
│   ├── Admin/                 # Admin panel views
│   ├── Products/              # Product management views
│   └── Shared/                # Shared layouts and components
├── Data/                      # Data Access Layer
│   └── CandidateContext.cs    # Entity Framework context
├── wwwroot/                   # Static files
│   ├── css/                   # Stylesheets
│   ├── js/                    # JavaScript files
│   └── uploads/               # File uploads
├── Migrations/                # Database migrations
└── Properties/                # Project properties
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/Project-Pharmaceuticals.git
   cd Project-Pharmaceuticals
   ```

2. **Update connection string**
   ```json
   // appsettings.json
   {
     "ConnectionStrings": {
       "CandidateContext": "Server=YOUR_SERVER;Database=e-project61;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false"
     }
   }
   ```

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Public site: `https://localhost:5001`
   - Admin panel: `https://localhost:5001/Admin/Login`
   - Default admin credentials: `admin@gmail.com` / `admin`

## 🗄 Database Schema

### Core Entities

| Entity | Description | Key Fields |
|--------|-------------|------------|
| **Products** | Pharmaceutical products | Id, Name, Description, Image, CategoryId |
| **Categories** | Product categories | Id, Name |
| **Contacts** | Customer inquiries | Id, Name, Email, Subject, Message |
| **Quotes** | Business quote requests | Id, Full_Name, Company_Name, Address, Email, Phone |
| **Careers** | Job applications | Id, Name, Email, Resume, CoverLetter, Status |
| **Candidates** | Registered users | Id, Name, Email, Phone, Password, Address |
| **AdminLogins** | Admin users | Id, Name, Email, Role, Password |

### Relationships
- Products → Categories (Many-to-One)
- Careers → Candidates (Many-to-One)

## 👥 User Roles

### 🔓 **Public Users**
- Browse products and company information
- Submit contact forms and quote requests
- Register and manage personal accounts
- Apply for career opportunities

### 🔐 **Registered Users**
- Access personal dashboard
- Track career application status
- Update profile information
- Submit career applications with file uploads

### 👨‍💼 **Administrators**
- Full system access
- Manage products and categories
- Process career applications
- Handle customer inquiries
- Manage user accounts
- Send email notifications

### 🔧 **Sub-Admins**
- Limited administrative access
- Product and category management
- Contact and quote management

## 📡 API Endpoints

### Public Endpoints
```
GET  /                    # Homepage
GET  /Home/About          # About page
GET  /Home/Products       # Product catalog
GET  /Home/Contact        # Contact form
GET  /Home/Quote          # Quote request form
GET  /Home/Careers        # Career application form
POST /Home/Contactus      # Submit contact form
POST /Home/Quoteus        # Submit quote request
POST /Home/Create         # User registration
POST /Home/Profile        # User login
```

### Admin Endpoints
```
GET  /Admin/Index         # Admin dashboard
GET  /Admin/Careers       # Career management
GET  /Admin/ContactUs     # Contact management
GET  /Admin/Quotes        # Quote management
GET  /Admin/Products      # Product management
GET  /Admin/CandidateView # User management
POST /Admin/ApproveCareer # Approve career application
POST /Admin/RejectCareer  # Reject career application
POST /Admin/SendCareerEmail # Send career email
```

## 📸 Screenshots

> *Screenshots would be added here showing the homepage, admin panel, product catalog, and other key features*

## 🤝 Contributing

We welcome contributions to improve InnovaMedix Pharmora! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow C# coding conventions
- Add proper error handling
- Include unit tests for new features
- Update documentation as needed

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support and questions:
- Email: jawadulbahar@gmail.com
- Create an issue in the repository
- Check the documentation in the `/docs` folder

## 🏆 Acknowledgments

- Built with ASP.NET Core MVC
- Entity Framework for data access
- Bootstrap for responsive design
- Gmail SMTP for email functionality

---

<div align="center">
  <strong>Built with ❤️ for the Pharmaceutical Industry</strong>
</div>
