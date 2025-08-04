# PlanDiario Solution Quick Reference

## Project Structure
- **PlanDiario.Domain**: Core business entities
- **PlanDiario.Application**: DTOs and application contracts
- **PlanDiario.Infrastructure**: Data access layer with Entity Framework
- **PlanDiario.API**: ASP.NET Core Web API
- **PlanDiario.Frontend**: Blazor WebAssembly client application

## Getting Started

### 1. Database Setup
`ash
cd PlanDiario.API
dotnet ef migrations add InitialCreate
dotnet ef database update
`

### 2. Run the API
`ash
cd PlanDiario.API
dotnet run
`
API will be available at: https://localhost:7000

### 3. Run the Frontend
`ash
cd PlanDiario.Frontend  
dotnet run
`
Frontend will be available at: https://localhost:5001

## API Endpoints

### Customers
- GET /api/customers - Get all customers
- GET /api/customers/{id} - Get customer by ID
- GET /api/customers/by-dni/{dni} - Get customer by DNI
- POST /api/customers - Create new customer
- PUT /api/customers/{id} - Update customer
- DELETE /api/customers/{id} - Delete customer

### Orders
- GET /api/orders - Get all orders
- GET /api/orders/{id} - Get order by ID
- GET /api/orders/by-customer/{customerId} - Get orders by customer
- POST /api/orders - Create new order
- PUT /api/orders/{id} - Update order
- DELETE /api/orders/{id} - Delete order

### Products
- GET /api/products - Get all products
- GET /api/products/{id} - Get product by ID
- GET /api/products/by-subcategory/{subcategoryId} - Get products by subcategory

### Lookups
- GET /api/lookups/localities - Get all localities
- GET /api/lookups/departments - Get all departments
- GET /api/lookups/collectors - Get all collectors
- GET /api/lookups/salesmen - Get all salesmen
- GET /api/lookups/business-types - Get all business types
- GET /api/lookups/periods - Get all periods
- GET /api/lookups/subcategories - Get all subcategories
- GET /api/lookups/categories - Get all categories
- GET /api/lookups/lines - Get all lines

## Configuration

### Connection String
Update the connection string in PlanDiario.API/appsettings.json:
`json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PlanDiarioDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
`

### CORS Configuration
The API is configured to allow requests from:
- https://localhost:7001
- http://localhost:5001

Update the CORS policy in PlanDiario.API/Program.cs if using different ports.

## Key Features Implemented
- Clean Architecture pattern
- Entity Framework Core with SQL Server
- Complete CRUD operations for all entities
- RESTful API design
- Blazor WebAssembly frontend
- Responsive Bootstrap UI
- Service layer abstraction
- Proper error handling
- Navigation and routing

## Domain Model
The solution includes these main entities:
- Customer (with locality, collector, salesman relationships)
- Order (with customer, period, business type relationships)  
- Product (with subcategory hierarchy)
- OrderProduct (many-to-many relationship)
- Supporting lookup entities (Department, Locality, Collector, Salesman, etc.)
