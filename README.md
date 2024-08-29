# Backend Sales API

This project is a backend API for managing sales data, including entities such as pizzas, pizza types, orders, and order details. It provides endpoints for uploading CSV files and processing data.

## Clone the Repository

To get started, clone the repository using the following command:

```bash
git clone https://github.com/justin2592/Backend.Sales.API.git


Here's a README.md file tailored for your project:

markdown
Copy code
# Backend Sales API

This project is a backend API for managing sales data, including entities such as pizzas, pizza types, orders, and order details. It provides endpoints for uploading CSV files and processing data.

## Clone the Repository

To get started, clone the repository using the following command:

```bash
git clone https://github.com/justin2592/Backend.Sales.API.git
Setup
1. Set Up the Database
Navigate to the Backend.Sales.API/Scripts directory and execute the SQL scripts using Microsoft SQL Server Management Studio (SSMS) to set up the database and required tables.

2. Run the Solution
Open the solution in Visual Studio or your preferred IDE, and run the project.

Testing the API
You can test the API using Postman, Thunder Client, or any other HTTP client application.

Example curl Command
To upload a CSV file and process it, use the following curl command:

curl -X 'POST' \
  'https://localhost:7213/upload/3' \
  -H 'accept: */*' \
  -H 'Content-Type: multipart/form-data' \
  -H 'SMS-API-KEY: 6308f3d6-8ff7-4dea-99fd-82b54c2fbc97' \
  -F 'file=@/path/to/your/pizza.csv'

API Key Authentication
To interact with the API, include the SMS-API-KEY header in your requests. The API key used in the example is 6308f3d6-8ff7-4dea-99fd-82b54c2fbc97.

**Uploading Files**
To upload files, you can use tools like Postman or Thunder Client, or any HTTP client that supports multipart form-data.

**Using Postman**
Set the request type to POST.
Enter the URL: https://localhost:7213/upload/{fileType}
In the Headers section, add:
accept: */*
Content-Type: multipart/form-data
SMS-API-KEY: {your-api-key}
In the Body section:
Select form-data
Set the key as file and type as File.
Upload the file from Backend.Sales.API\CSV.

Troubleshooting
If you encounter any issues:

Ensure that the SQL scripts have been executed correctly.
Verify that the API is running and accessible.
Check the logs for any error messages and consult the documentation or support for further assistance.
