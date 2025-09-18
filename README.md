# Old School Runescape Project

A full-stack monorepo project featuring an ASP.NET Core backend and React frontend for Old School Runescape related functionality. Fully containerized with Docker and optimized for development and production.

## Project Structure

```
├── BackEnd/                          # ASP.NET Core Web API
│   ├── Controllers/
│   │   └── WeatherForecastController.cs  # Sample API controller
│   ├── appsettings.json              # Application settings
│   ├── appsettings.Development.json  # Development settings
│   ├── Program.cs                    # Application entry point
│   └── Properties/
│       └── launchSettings.json
├── FrontEnd/                         # React + TypeScript + Vite
│   ├── src/                          # Source code
│   ├── public/                       # Static assets
│   ├── components.json               # shadcn/ui configuration
│   ├── package.json                  # Frontend dependencies
│   ├── pnpm-lock.yaml                # Lockfile for pnpm
│   ├── tsconfig.json                 # TypeScript configuration
│   ├── vite.config.ts                # Vite configuration
│   ├── vitest.config.ts              # Vitest configuration
│   ├── Dockerfile                    # Frontend containerization
│   └── .dockerignore                 # Docker ignore rules
├── docker-compose.yml                # Multi-service orchestration
├── Dockerfile                        # Backend containerization
├── .dockerignore                     # Docker ignore rules
├── .editorconfig                     # Code formatting standards
├── .gitignore                        # Git ignore rules
├── Old School Runescape Project.csproj  # .NET project file
├── Old School Runescape Project.sln     # Visual Studio solution
└── README.md                         # This file
```

## Tech Stack

### Backend
- **Framework**: ASP.NET Core 8.0
- **Language**: C#
- **Runtime**: .NET 8.0
- **API**: RESTful Web API with Controllers
- **Documentation**: Swagger/OpenAPI
- **Container**: Docker (Linux-based)
- **Build Tool**: .NET SDK

### Frontend
- **Framework**: React 19
- **Language**: TypeScript 5.7
- **Build Tool**: Vite 6.3
- **Styling**: Tailwind CSS 4.0
- **Routing**: TanStack Router 1.130
- **Package Manager**: pnpm
- **UI Components**: shadcn/ui with Tailwind
- **Testing**: Vitest + Testing Library
- **Container**: Docker (Node.js + Nginx)

### Development Tools
- **Container Orchestration**: Docker Compose
- **Code Quality**: EditorConfig, TypeScript strict mode
- **Version Control**: Git with comprehensive .gitignore
- **IDE**: VS Code with workspace settings

## Quick Start with Docker

### Prerequisites
- Docker Desktop installed and running
- Git (for cloning the repository)

### Run the Full Stack
```bash
# Clone the repository (if not already done)
git clone <repository-url>
cd Osrs-Projects

# Build and start all services
docker-compose up --build

# Access the applications:
# Frontend: http://localhost:3000
# Backend API: http://localhost:8080
# Swagger UI: http://localhost:8080/swagger
```

### Stop the Services
```bash
docker-compose down
```

## Development Setup

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ and pnpm
- Git

### Backend Setup
```bash
# Navigate to backend directory
cd BackEnd

# Restore dependencies
dotnet restore

# Run the backend
dotnet run
```
**API Endpoints:**
- Base URL: `http://localhost:8080`
- Weather Forecast: `GET /WeatherForecast`
- Swagger UI: `http://localhost:8080/swagger`

### Frontend Setup
```bash
# Navigate to frontend directory
cd FrontEnd

# Install dependencies
pnpm install

# Start development server
pnpm dev
```
**Access:** `http://localhost:3000`

### Available Scripts
```bash
# Frontend commands
pnpm dev          # Start development server
pnpm build        # Build for production
pnpm serve        # Preview production build
pnpm test         # Run tests

# Backend commands
dotnet build      # Build the project
dotnet run        # Run the application
dotnet test       # Run tests (when added)
```

## Docker Architecture

### Multi-Stage Builds
- **Backend**: .NET SDK → ASP.NET Core Runtime
- **Frontend**: Node.js → Nginx Alpine

### Services
- **backend**: ASP.NET Core API on ports 8080/8081
- **frontend**: React app served by Nginx on port 3000
- **Network**: Isolated bridge network for service communication

### Container Optimization
- Linux-based containers for smaller size
- Multi-stage builds to reduce final image size
- Proper .dockerignore files to exclude unnecessary files
- Frozen lockfiles for reproducible builds

## API Documentation

### Current Endpoints

#### WeatherForecast
```http
GET /WeatherForecast
```

**Response:**
```json
[
  {
    "date": "2025-09-19",
    "temperatureC": 25,
    "temperatureF": 77,
    "summary": "Warm"
  }
]
```

### Swagger Documentation
When running the backend, visit `/swagger` for interactive API documentation.

## Development Workflow

### Code Quality
- **EditorConfig**: Consistent formatting across editors
- **TypeScript**: Strict mode enabled
- **Git Hooks**: Pre-commit checks (can be added)

### Building for Production
```bash
# Build backend
dotnet publish -c Release

# Build frontend
cd FrontEnd && pnpm build

# Build all containers
docker-compose build
```

### Testing
```bash
# Frontend tests
cd FrontEnd && pnpm test

# Backend tests (when added)
dotnet test
```

## Configuration

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Development/Production
- Custom settings in `appsettings.json`

### Ports
- Backend: 8080 (HTTP), 8081 (HTTPS)
- Frontend: 3000 (development), 80 (production container)

## Deployment

### Docker Deployment
```bash
# Build production images
docker-compose build

# Run in detached mode
docker-compose up -d

# Scale services if needed
docker-compose up -d --scale backend=2
```

### Production Considerations
- Environment variables for configuration
- HTTPS certificates
- Database connections
- Logging and monitoring
- Health checks

## Contributing

1. **Setup**: Follow the development setup instructions
2. **Code Style**: Follow EditorConfig and TypeScript standards
3. **Testing**: Ensure all tests pass before submitting
4. **Documentation**: Update README and API docs as needed
5. **Commits**: Use clear, descriptive commit messages

### Adding New Features
1. **Backend**: Add controllers in `BackEnd/Controllers/`
2. **Frontend**: Add components in `FrontEnd/src/`
3. **Rebuild**: Run `docker-compose up --build` to test changes

## Troubleshooting

### Common Issues

**Docker Build Fails**
```bash
# Clear Docker cache
docker system prune -a

# Rebuild without cache
docker-compose build --no-cache
```

**Port Conflicts**
```bash
# Check what's using ports
netstat -ano | findstr :8080
netstat -ano | findstr :3000

# Change ports in docker-compose.yml if needed
```

**Permission Issues**
```bash
# On Windows, ensure Docker Desktop is running
# On Linux/Mac, ensure Docker daemon is running
```

## License

[Add your license information here]

---

**Built with ❤️ for Old School Runescape enthusiasts**