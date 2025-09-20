# Old School RuneScape Project

A full-stack application with .NET 8 Web API backend, React TypeScript frontend, and PostgreSQL database, orchestra### Admin
- `GET /api/admin/stats` - Get application statistics

## 📁 Project Structure

### Backend (`BackEnd/`)
```
BackEnd/
├── Controllers/           # API endpoint controllers
│   ├── AdminController.cs
│   ├── TaskListsController.cs
│   └── WeatherForecastController.cs
├── Models/               # Entity Framework models
│   ├── ApplicationDbContext.cs  # Database context
│   ├── TaskList.cs             # Task list model
│   └── README.md               # Models documentation
├── Migrations/           # Database migrations
│   ├── [Date]_*.cs       # Migration files
│   └── README.md         # Migration management guide
├── Properties/
│   └── launchSettings.json
├── appsettings.json      # Application configuration
├── Dockerfile           # Container definition
└── Program.cs           # Application entry point
```

### Frontend (`FrontEnd/`)
```
FrontEnd/
├── public/              # Static assets
├── src/
│   ├── components/      # Reusable React components
│   ├── lib/            # Utility functions and configurations
│   ├── routes/         # Application routes
│   ├── styles.css      # Global styles
│   └── main.tsx        # Application entry point
├── Dockerfile          # Container definition
├── package.json        # Dependencies and scripts
├── vite.config.ts      # Vite configuration
└── tsconfig.json       # TypeScript configuration
```

### Root Directory
```
├── .env                # Environment variables (configure database)
├── docker-compose.yml  # Development containers
├── docker-compose.prod.yml  # Production containers
├── Makefile           # Build automation
└── README.md          # This file
```

## 📋 Available Commandswith Docker and managed through a unified Makefile.

## 🏗️ Architecture

```
osrs-projects/
├── Makefile                 # Unified command interface
├── docker-compose.yml       # Development orchestration
├── docker-compose.prod.yml  # Production overrides
├── .env                     # Environment configuration
├── BackEnd/                 # .NET 8 Web API with EF Core
│   ├── Dockerfile          # Backend container definition
│   ├── Program.cs
│   ├── Controllers/        # API endpoints
│   ├── Models/             # Entity Framework models
│   │   ├── ApplicationDbContext.cs
│   │   ├── TaskList.cs
│   │   └── README.md       # Models documentation
│   └── Migrations/         # Database migrations
│       └── README.md       # Migration management guide
└── FrontEnd/               # React + TypeScript + Vite
    ├── Dockerfile          # Frontend container definition
    ├── package.json
    ├── src/
    │   ├── components/
    │   ├── lib/
    │   └── routes/
    └── public/
```

## 🚀 Quick Start

### Prerequisites
- Docker and Docker Compose
- **Windows**: PowerShell (included) or Make (install via `choco install make`)
- **Linux/macOS**: Make (usually pre-installed)
- Optional: .NET 8 SDK and Node.js 18+ for local development

### Environment Setup
1. Copy `.env` file and configure your database credentials:
```bash
# Required environment variables
DB_SERVER=your-postgres-server
DB_PORT=5432
DB_NAME=your-database-name
DB_USER=your-username
DB_PASSWORD=your-password
```

### For New Developers
**Linux/macOS (Make):**
```bash
# Complete setup (installs dependencies and builds containers)
make setup

# Start development environment
make dev
```

That's it! Your backend will be at `http://localhost:8080` and frontend at `http://localhost:3000`.

## 🗄️ Database Setup

The project uses PostgreSQL with Entity Framework Core for data persistence.

### Database Configuration
- **Provider**: PostgreSQL (via Supabase)
- **ORM**: Entity Framework Core 8
- **Migration Strategy**: Code-first with automatic migrations

### Initial Database Setup
```powershell
# Navigate to project root
cd 'c:\Users\faspe\Desktop\workwithmarkus\Osrs-Projects'

# Apply all migrations to create/update database schema
dotnet ef database update

# Verify connection
dotnet run --project BackEnd
```

### Database Models
The application includes the following models:
- **TaskList**: Manages user task lists
- **User**: Basic user information

See `BackEnd/Models/README.md` for detailed model documentation and examples.

### Migration Management
- All migrations are stored in `BackEnd/Migrations/`
- See `BackEnd/Migrations/README.md` for migration best practices
- Use `dotnet ef migrations add [Name]` to create new migrations
- Use `dotnet ef database update` to apply migrations

## � Development Workflow

### Daily Development
1. `make dev` - Start your development environment
2. Make your changes to models, controllers, or frontend
3. `dotnet ef migrations add [MigrationName]` - Create database migrations for model changes
4. `dotnet ef database update` - Apply migrations to update database
5. `make test` - Run tests
6. `make format` - Format your code
7. Commit and push

### Adding New Database Features
1. Create or modify models in `BackEnd/Models/`
2. Update `ApplicationDbContext.cs` if needed
3. Create migration: `dotnet ef migrations add AddNewFeature`
4. Apply migration: `dotnet ef database update`
5. Test your changes with the API

### Adding New API Endpoints
1. Create controller in `BackEnd/Controllers/`
2. Add routes and business logic
3. Test endpoints with Postman/Swagger
4. Update frontend to consume new endpoints

### Frontend Development
1. Components are in `FrontEnd/src/components/`
2. Routes are configured in `FrontEnd/src/routes/`
3. Use `pnpm run dev` for local frontend development
4. Use `pnpm run test` to run frontend tests

## 🌐 API Endpoints

### Task Lists
- `GET /api/tasklists` - Get all task lists
- `GET /api/tasklists/{id}` - Get specific task list
- `POST /api/tasklists` - Create new task list
- `PUT /api/tasklists/{id}` - Update task list
- `DELETE /api/tasklists/{id}` - Delete task list

### Weather Forecast (Example)
- `GET /api/weatherforecast` - Get weather forecast data

### Admin
- `GET /api/admin/stats` - Get application statistics

## �📋 Available Commands

> **Note**: Use `.\build.ps1 [command]` on Windows or `make [command]` on Linux/macOS

### Development
```bash
# Windows                    # Linux/macOS
.\build.ps1 dev             # make dev              # Start both services in development mode
.\build.ps1 dev-backend     # make dev-backend      # Start only backend
.\build.ps1 dev-frontend    # make dev-frontend     # Start only frontend  
.\build.ps1 dev-local       # make dev-local        # Run both services locally (no Docker)
```

### Building
```bash
.\build.ps1 build           # make build            # Build both Docker images
.\build.ps1 build-backend   # make build-backend    # Build only backend image
.\build.ps1 build-frontend  # make build-frontend   # Build only frontend image
.\build.ps1 build-local     # make build-local      # Build locally without Docker
```

### Testing
```bash
.\build.ps1 test            # make test             # Run all tests
.\build.ps1 test-backend    # make test-backend     # Run only backend tests
.\build.ps1 test-frontend   # make test-frontend    # Run only frontend tests
```

### Docker Management
```bash
.\build.ps1 up              # make up               # Start services in background
.\build.ps1 down            # make down             # Stop all services
.\build.ps1 restart         # make restart          # Restart all services
.\build.ps1 logs            # make logs             # View logs from all services
.\build.ps1 status          # make status           # Show service status
```

### Maintenance
```bash
.\build.ps1 clean           # make clean            # Clean up everything (Docker + build artifacts)
.\build.ps1 clean-docker    # make clean-docker     # Clean up Docker containers and images only
.\build.ps1 format          # make format           # Format code in both projects
.\build.ps1 lint            # make lint             # Lint frontend code
```

### Production
```bash
.\build.ps1 prod            # make prod             # Build and start in production mode
# make deploy (Linux/macOS only) # Deploy to production (customize as needed)
```

### Help
```bash
.\build.ps1 help            # make help             # Show all available commands
.\build.ps1 info            # make info             # Display project information
```

##  Docker Structure

### Backend (`BackEnd/Dockerfile`)
- Multi-stage build for optimal image size
- Uses .NET 8 runtime and SDK images
- Exposes ports 8080 and 8081

### Frontend (`FrontEnd/Dockerfile`)
- Node.js build stage with npm
- Nginx production stage for serving
- Exposes port 80 (mapped to 3000 in docker-compose)

### Benefits of This Structure
- **Unified Interface**: Single `make` command for any task
- **Environment Consistency**: Same commands work everywhere
- **Optimized Builds**: Separate Dockerfiles allow per-service optimization
- **Developer Friendly**: New team members need minimal setup knowledge
- **CI/CD Ready**: Easy integration with automated pipelines

## 🔧 Local Development (No Docker)

If you prefer to run services locally:

```bash
# Install dependencies
make install

# Terminal 1: Backend
cd BackEnd
dotnet run

# Terminal 2: Frontend  
cd FrontEnd
pnpm run dev
```

## 📦 Production Deployment

The Makefile provides production-ready commands:

```bash
# Build production images
make build

# Start in production mode
make prod

# Or deploy (customize the deploy target in Makefile)
make deploy
```

## 🤝 Contributing

1. Run `make setup` for initial setup
2. Configure your `.env` file with database credentials
3. Run `dotnet ef database update` to set up the database
4. Use `make help` to see available commands
5. Follow the development workflow above
6. Ensure `make test` passes before submitting PRs
7. Include database migrations if you've changed models

## 📚 Tech Stack

- **Backend**: .NET 8 Web API with Entity Framework Core
- **Database**: PostgreSQL (via Supabase)
- **Frontend**: React 19 + TypeScript + Vite + TailwindCSS
- **Routing**: TanStack Router (React Router)
- **Icons**: Lucide React
- **Styling**: TailwindCSS with custom components
- **Testing**: Vitest + React Testing Library
- **Containerization**: Docker + Docker Compose
- **Build Tool**: Make
- **Package Managers**: pnpm (frontend), NuGet (.NET)
- **Development Tools**: ESLint, Prettier, TypeScript

## 🎯 Current Features

### ✅ Implemented
- **Database Integration**: Full PostgreSQL setup with EF Core
- **Entity Framework Core**: Code-first migrations and models
- **API Controllers**: RESTful endpoints for data operations
- **Modern Frontend**: React 19 with TypeScript and Vite
- **Component Library**: Reusable UI components with TailwindCSS
- **Docker Orchestration**: Complete containerization setup
- **Development Workflow**: Unified build system with Make
- **Environment Management**: Configurable database connections
- **Migration System**: Automated database schema management

### � In Development
- User authentication and authorization
- OSRS API integration
- Advanced task management features
- Real-time updates and notifications

### 🔜 Roadmap
- [ ] Implement user authentication system
- [ ] Add OSRS game data integration
- [ ] Set up CI/CD pipeline
- [ ] Add comprehensive testing suite
- [ ] Performance monitoring and logging
- [ ] Admin dashboard for content management
- [ ] Mobile-responsive design improvements
- [ ] API documentation with Swagger/OpenAPI

## 🔧 Troubleshooting

### Database Connection Issues
```powershell
# Check if database is accessible
dotnet ef database update

# If migrations fail, check your .env file
# Ensure DB_SERVER, DB_USER, DB_PASSWORD are correct
```

### Docker Issues
```bash
# Clean up Docker containers and images
make clean-docker

# Rebuild from scratch
make build
make dev
```

### Migration Issues
```powershell
# Remove last migration if it has issues
dotnet ef migrations remove

# Reset database (WARNING: destroys data)
dotnet ef database drop --force
dotnet ef database update
```

### Frontend Issues
```bash
# Clear node modules and reinstall
cd FrontEnd
rm -rf node_modules package-lock.json
pnpm install

# Clear Vite cache
rm -rf node_modules/.vite
```

### Backend Issues
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore

# Clean and rebuild
dotnet clean
dotnet build
```

## 📞 Support

- Check the troubleshooting section above
- Review the model and migration documentation in their respective READMEs
- Ensure your environment variables are properly configured
- Test database connectivity before running the application

## 📈 Performance

- Database queries are optimized with Entity Framework Core
- Docker containers are optimized for development and production
- Frontend is built with Vite for fast development builds
- Static assets are served through Nginx in production

---

**Happy coding!** 🎮