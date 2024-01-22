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

The application will be accessible at [http://localhost:5000](http://localhost:5000) by default.

### API Documentation

- Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)

### API Endpoints

The system provides a set of API endpoints for managing users, expenses, categories, and approvals. For a detailed list of API endpoints and their usage, refer to the Swagger documentation.

