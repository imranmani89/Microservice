# MarketApp - Microservices Architecture

A distributed microservices application built with .NET 8.0 that demonstrates a scalable, message-driven architecture for handling orders with Email and SMS notifications.

## 🏗️ Project Architecture

MarketApp consists of four main components:

### Core Services

- **MainApp** - The main application service handling orders and orchestrating communication between services
- **EmailApp** - Dedicated microservice for processing and sending email notifications
- **SMSApp** - Dedicated microservice for processing and sending SMS notifications
- **Shared** - Shared libraries containing common models, constants, and utilities

## 🔧 Technology Stack

- **.NET 8.0** - Framework for all services
- **RabbitMQ** - Message broker for asynchronous communication between services
- **ASP.NET Core** - Web framework for MainApp
- **Hosted Services** - Background services for processing messages

## 📋 Project Structure

```
MarketApp/
├── MainApp/                    # Main application service
│   ├── Controllers/           # API endpoints
│   ├── Program.cs             # Application entry point
│   └── appsettings.json       # Configuration
├── EmailApp/                  # Email notification service
│   ├── BackgroundServices/    # Email processing service
│   ├── Program.cs             # Application entry point
│   └── appsettings.json       # Configuration
├── SMSApp/                    # SMS notification service
│   ├── BackgroundServices/    # SMS processing service
│   ├── Program.cs             # Application entry point
│   └── appsettings.json       # Configuration
├── Shared/                    # Shared library
│   ├── Models/               # Domain models (Order, etc.)
│   └── Constants/            # Shared constants
└── MarketApp.sln             # Solution file
```

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- RabbitMQ server running locally or accessible via network
- Visual Studio or any .NET IDE (VS Code recommended)

### Building the Project

```bash
dotnet build MarketApp.sln
```

### Running the Services

Each service can be run independently:

```bash
# Terminal 1: Start MainApp
dotnet run --project MainApp/MainApp.csproj

# Terminal 2: Start EmailApp
dotnet run --project EmailApp/EmailApp.csproj

# Terminal 3: Start SMSApp
dotnet run --project SMSApp/SMSApp.csproj
```

## 💬 Communication Flow

1. **MainApp** receives an order through its API
2. Orders are published to RabbitMQ message queues
3. **EmailApp** and **SMSApp** listen to their respective queues
4. Each microservice independently processes and sends notifications
5. Services are loosely coupled and can scale independently

## ⚙️ Configuration

Each service has an `appsettings.json` file where you can configure:

- RabbitMQ connection strings
- Logging levels
- Service-specific settings
- Environment-specific configurations

Example:
```json
{
  "RabbitMQ": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest"
  }
}
```

## 🐳 Docker Deployment

Dockerfiles are included for each service for containerized deployment:

```bash
# Build Docker images
docker build -t marketapp-main ./MainApp
docker build -t marketapp-email ./EmailApp
docker build -t marketapp-sms ./SMSApp
```

## 📦 Dependencies

- **RabbitMQ.Client** - For message queue operations
- **Microsoft.Extensions.Configuration** - Configuration management
- **Microsoft.Extensions.Hosting** - Dependency injection and hosting
- **Microsoft.Extensions.Configuration.EnvironmentVariables** - Environment variable support
- **Microsoft.Extensions.Configuration.Json** - JSON configuration support

## 🧪 Development

### Key Features

- **Asynchronous Processing** - Non-blocking message processing
- **Scalable Architecture** - Services can be deployed and scaled independently
- **Loose Coupling** - Services communicate via message queues, not direct calls
- **Shared Models** - Common domain models in the Shared project
- **Background Services** - Long-running background tasks for message processing

## 🤝 Contributing

When adding new features:

1. Keep services focused on a single responsibility
2. Use the Shared project for common models and constants
3. Follow existing naming conventions and code structure
4. Ensure all services build successfully: `dotnet build MarketApp.sln`

## 📝 Notes

- This is a demonstration of microservices architecture patterns
- Services use background workers to process messages asynchronously
- The architecture supports horizontal scaling of individual services

---

**Last Updated:** May 2026
