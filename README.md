
# Payroll Management API

This repository contains the code for a Payroll API, designed to manage employee payroll processes efficiently. The API provides functionalities for handling various payroll operations, including:

- **Employee management:** Adding, updating, and removing employee details.
- **Salary processing:** Calculating and managing employee salaries, deductions, and bonuses.
- **Payroll generation:** Creating and distributing payroll records for employees.
- **Reporting:** Generating reports for payroll activities and employee earnings.

The Payroll API is built using modern web technologies and follows RESTful principles, ensuring scalability and ease of integration with other systems. It is designed to be secure, reliable, and easy to use, making it a valuable tool for organizations of all sizes.

## Tech Stack

**Server:** .Net Core, JWT, Jenkins, SonarQube,
Git, GitHub, Docker, DockerHub 

**Deployment:** Azure, MSSQL express 

## FrontEnd Reference

For FrontEnd please refer this repository:
[Payroll Web App](https://github.com/naveenbabu4/PayRollWebApp.git)

## Installation
- For running the application locally install
   - .Net 6
   - SQL Server
- For deploying the project install
   - Docker

## Run Locally

Clone the project

```bash
  https://github.com/naveenbabu4/PayrollAPIFinal.git
```

Go to the project directory

```bash
  cd PayRollManagementSystemAPI
```
- Add connection string in appsettings.json
  
Install dependencies

```bash
  add-migration intial
  update database --verbose
```

Build the application

```bash
  dotnet build
```

Run the application

```bash
  dotnet run
```


## Authors

- Github Profile
     - [@Naveen Bahunadham](https://github.com/naveenbabu4/)
-  Contact details:
    - Naveen Bahunadham
      - 📧 bsa.naveenbabu@gmail.com
