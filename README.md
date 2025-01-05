# Customer Registration and Management System

## Overview

The **Customer Registration and Management System** is a web application designed to manage customer data efficiently. The system allows users to register new customers, update existing profiles, and manage customer information, including their city of residence.

The application is built using **ASP.NET MVC**, and the database is managed using **SQL Server**. It leverages HTML Helpers for form generation and uses a `ViewModel` to bind data between the views and controllers.

## Features

- **Customer Registration**: 
  - Users can register new customers with details such as full name, username, password, email ID, phone number, gender, and city.
  - The city selection is provided through a dynamically populated dropdown list.

- **Customer Update**:
  - The system allows users to update customer profiles, with pre-filled data to simplify the process.
  - Cities dropdown is also available on the update form to select or change the city.

- **Data Management**:
  - Customer data is fetched from a **SQL Server** database.
  - The city list for the dropdown is dynamically populated from the `Cities` table in the database.

- **Error Handling**:
  - The application handles scenarios such as missing customer records and database connection issues gracefully, providing meaningful messages to the user.

## Technologies Used

- **ASP.NET MVC**: Framework used for developing the web application.
- **C#**: Programming language used for backend logic.
- **SQL Server**: Database used to store customer and city information.
- **HTML & CSS**: For frontend layout and styling.
- **Bootstrap**: For responsive design.
- **GitHub**: For version control and project management.

## Database Schema

- **Customers Table**:
  - `CustId`: Primary key.
  - `FullName`, `UserName`, `Password`, `EmailId`, `PhoneNo`, `Gender`: Customer details.
  - `CId`: Foreign key linking to the `Cities` table.

- **Cities Table**:
  - `CId`: Primary key.
  - `CName`: Name of the city.

## Setup Instructions

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/CustomerManagementSystem.git
   ```
2. Open the project in Visual Studio: Launch Visual Studio and open the solution file (.sln) from the cloned repository.
4. Update the connection : In Visual Studio, locate the Web.config . Modify the connection string to match your local SQL Server setup:

        <connectionStrings>
            <add name="DefaultConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YourDatabaseName;Integrated Security=True;" providerName="System.Data.SqlClient" />
        </connectionStrings>

5. Replace YourDatabaseName with the actual name of your database.

6. Create the database and tables: Ensure the tables are populated with initial data (cities).

7. Run the application: Press Ctrl + F5 or click on the Run button in Visual Studio to start the application. The application will open in your browser, and you can start interacting with the customer registration and management system.
