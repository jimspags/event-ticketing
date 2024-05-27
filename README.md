# Project Title

## Description
This project is a web application built using C# Web API for the backend and AngularJS for the frontend. It integrates with the Stripe API for handling payments. The system utilizes MSSQL as its database. After a customer purchases a ticket, they will receive an email containing a receipt.

## Features
- User-friendly interface powered by AngularJS.
- Secure payment processing using the Stripe API.
- Seamless integration with MSSQL for efficient data management.
- Automated email receipts for customers after purchasing a ticket.

## Installation
1. Clone the repository: `git clone https://github.com/jimspags/event-ticketing`
2. Navigate to the backend directory: `cd API`
3. Install dependencies: `dotnet restore`
4. Configure the MSSQL database connection string in the `appsettings.json` file located in the backend project.
5. Apply EF migrations to update the database schema: 
   - Navigate to the backend directory if you're not already there: `cd API`
   - Run the following command to apply migrations: `dotnet ef database update`
6. Configure the Stripe API keys in the `appsettings.json` file.
7. Build and run the backend: `dotnet build` and `dotnet run`
8. Navigate to the frontend directory: `cd ../UI`
9. Install dependencies: `npm install`
10. Configure the backend API URL in the AngularJS environment file.
11. Build and run the frontend: `ng serve`

## Usage
1. Open your web browser and navigate to the URL where the frontend is hosted.
2. Browse the available events and select the desired ticket.
3. Proceed to checkout and enter your payment information.
4. After successful payment, you will receive an email containing your receipt.

## Contributors
- [James Paul Paguirigan][(https://github.com/yourusername)](https://github.com/jimspags)

## License
This project is licensed under the [MIT License](LICENSE).

## Acknowledgments
- Stripe API Documentation: [Link](https://stripe.com/docs)
- AngularJS Documentation: [Link](https://angularjs.org/)
- Microsoft Docs for C# Web API: [Link](https://docs.microsoft.com/en-us/aspnet/web-api/)

