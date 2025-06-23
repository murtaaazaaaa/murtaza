# Velvet Leash API

A comprehensive ASP.NET Core Web API backend for the Velvet Leash Pet Care mobile application. This API provides complete functionality for pet owners to find and book pet sitters, manage their pets, and handle all aspects of pet care services.

## Features

### Authentication & Authorization
- JWT-based authentication
- User registration and login
- Social login support (Google, Facebook, Apple)
- Secure password management with BCrypt

### Pet Management
- Add, edit, and delete pets
- Pet profiles with detailed information (type, size, age, behavior)
- Medical conditions and special instructions
- Photo management support

### Pet Sitter Services
- Pet sitter profile creation and management
- Service offerings (boarding, day care, walking, sitting, grooming)
- Location-based search with radius filtering
- Rating and review system
- Availability management

### Booking System
- Create and manage bookings
- Real-time booking status updates
- Messaging between pet owners and sitters
- Payment integration ready

### Notifications
- Customizable notification preferences
- Email and SMS notification support
- Real-time messaging system

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity + JWT
- **ORM**: Entity Framework Core 9.0
- **Mapping**: AutoMapper
- **Password Hashing**: BCrypt.Net
- **API Documentation**: Swagger/OpenAPI

## Database Schema

The API uses the following main entities:

- **Users** (extends ASP.NET Identity)
- **Pets** - Pet information and characteristics
- **PetSitters** - Pet sitter profiles and services
- **Bookings** - Service bookings and appointments
- **Reviews** - Rating and feedback system
- **BookingMessages** - In-app messaging
- **UserNotificationSettings** - User preferences

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/social-login` - Social media login
- `POST /api/auth/logout` - User logout

### Pets
- `GET /api/pets` - Get user's pets
- `POST /api/pets` - Create new pet
- `GET /api/pets/{id}` - Get pet details
- `PUT /api/pets/{id}` - Update pet
- `DELETE /api/pets/{id}` - Delete pet

### Pet Sitters
- `GET /api/petsitters/search` - Search pet sitters
- `GET /api/petsitters/{id}` - Get pet sitter details
- `POST /api/petsitters` - Create pet sitter profile
- `PUT /api/petsitters` - Update pet sitter profile
- `GET /api/petsitters/{id}/reviews` - Get pet sitter reviews
- `POST /api/petsitters/reviews` - Create review

### Bookings
- `GET /api/bookings` - Get user bookings
- `POST /api/bookings` - Create booking
- `GET /api/bookings/{id}` - Get booking details
- `PUT /api/bookings/{id}` - Update booking
- `DELETE /api/bookings/{id}` - Cancel booking

### User Profile
- `GET /api/users/profile` - Get user profile
- `PUT /api/users/profile` - Update user profile

### Notifications
- `GET /api/notifications/settings` - Get notification settings
- `PUT /api/notifications/settings` - Update notification settings

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd VelvetLeashAPI
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure database**
   - Update the connection string in `appsettings.json`
   - Run the SQL script in `Database/CreateDatabase.sql` to create the database
   - Or let Entity Framework create the database automatically on first run

4. **Configure JWT settings**
   - Update JWT settings in `appsettings.json` if needed
   - The default configuration is suitable for development

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`
   - Use Swagger UI to test API endpoints

### Database Setup

#### Option 1: Manual SQL Script
Run the SQL script located at `Database/CreateDatabase.sql` in SQL Server Management Studio or Azure Data Studio.

#### Option 2: Entity Framework (Automatic)
The application is configured to automatically create the database on startup using `context.Database.EnsureCreated()`.

### Configuration

#### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VelvetLeashDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### JWT Settings
```json
{
  "Jwt": {
    "Key": "VelvetLeashSecretKeyForJWTTokenGeneration2024!@#$%",
    "Issuer": "VelvetLeashAPI",
    "Audience": "VelvetLeashApp"
  }
}
```

## Project Structure

```
VelvetLeashAPI/
├── Controllers/           # API Controllers
│   ├── Auth/             # Authentication controllers
│   ├── Pet/              # Pet management controllers
│   ├── Booking/          # Booking controllers
│   ├── Notification/     # Notification controllers
│   └── User/             # User profile controllers
├── Models/               # Entity models
├── DTOs/                 # Data Transfer Objects
├── Services/             # Business logic services
├── Data/                 # Database context
├── Database/             # SQL scripts
└── README.md
```

## API Features

### Search Functionality
- Location-based pet sitter search
- Filter by service type, price range, and availability
- Distance calculation and sorting
- Rating-based recommendations

### Security
- JWT token authentication
- Password hashing with BCrypt
- Input validation and sanitization
- CORS configuration for mobile app integration

### Error Handling
- Comprehensive error responses
- Validation error details
- Consistent API response format

## Development Notes

### Entity Relationships
- Users can have multiple Pets
- Users can be Pet Sitters (one-to-one)
- Pet Sitters can offer multiple Services
- Bookings connect Users, Pet Sitters, and Pets
- Reviews are linked to completed Bookings

### Enums Used
- `PetType`: Dog, Cat, Bird, Fish, Rabbit, Other
- `PetSize`: Small, Medium, Large, ExtraLarge
- `PetAge`: Puppy, Adult
- `ServiceType`: Boarding, DayCare, Walking, Sitting, Grooming
- `BookingStatus`: Pending, Confirmed, InProgress, Completed, Cancelled, Declined

## Testing

The API includes Swagger UI for interactive testing. All endpoints are documented with request/response examples.

## Deployment

The API is ready for deployment to:
- Azure App Service
- AWS Elastic Beanstalk
- Docker containers
- IIS servers

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.