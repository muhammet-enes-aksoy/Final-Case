# Expense Payment System

Expense Payment System is a comprehensive application developed to streamline the management and tracking of expense claims for employees working in the field. The system facilitates the submission, approval, and payment processes for expense claims, ensuring a seamless experience for both employees and managers.

## Project Overview

The Expense Payment System offers the following key features:

### User Roles

**Admin:**
- Manages user accounts and permissions.
- Reviews and approves/rejects expense claims submitted by employees.
- Monitors overall expense activity.

**Employee:**
- Submits expense claims for approval.
- Tracks the status of submitted claims.
- Views personal expense history and details.

### Tech Stack

- **Database:** MSSQL
- **Authentication:** JWT Tokens
- **Documentation:** Swagger 
- **Logging:** 

## Setup Instructions

### Prerequisites

- .NET SDK
- MSSQL Server

### Database Configuration

1. Create a new database on MSSQL Server.
2. Update the connection string in the `appsettings.json` file in the `ExpensePaymentSystem.Api` project.

### JWT Token Configuration

Update JWT token configuration in the `appsettings.json` file in the `ExpensePaymentSystem.Api` project.

### Running the Application

1. Open a terminal and navigate to the `ExpensePaymentSystem.Api` project folder.
2. Run the following command to start the application:
   ```bash
   dotnet run

### Accessing the Application

The application will be accessible at [http://localhost:5188](http://localhost:5188) by default.

### API Documentation

- Swagger: [http://localhost:5188/swagger/index.html](http://localhost:5188/swagger/index.html)

### API Endpoints

![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/71c49409-2577-4b7c-9aee-7a5ea095b394)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/c363b77b-c1da-48c2-8bd9-f1e11ecfb1fa)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/d02bfc9b-50c5-42cc-b186-a48a191506e0)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/694a1b29-3a92-4870-9fc1-d6c37f6322a6)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/b1823f5b-89c0-42ca-8d4f-f38d1addb86f)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/ab29a55a-d9ae-430e-8899-2012cfbb59d0)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/2918118e-3e8c-4654-bda3-be58eea10eac)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/843f3595-92c0-46df-b751-68778c1592f8)
![image](https://github.com/muhammet-enes-aksoy/Final-Case/assets/97848966/10cc94c4-57a7-4c38-a70c-7158a988a36e)

The system provides a set of API endpoints for managing users, expenses, categories, and approvals. For a detailed list of API endpoints and their usage, refer to the Swagger documentation.

