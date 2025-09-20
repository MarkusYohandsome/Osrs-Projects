# Old School RuneScape Project

A full-stack application with .NET 8 Web API backend and React TypeScript frontend, orchestrated with Docker and managed through a unified Makefile.

## 🏗️ Architecture

```
osrs-projects/
├── Makefile                 # Unified command interface
├── docker-compose.yml       # Development orchestration
├── docker-compose.prod.yml  # Production overrides
├── BackEnd/                 # .NET 8 Web API
│   ├── Dockerfile          # Backend container definition
│   ├── Program.cs
│   └── Controllers/
└── FrontEnd/               # React + TypeScript + Vite
    ├── Dockerfile          # Frontend container definition
    ├── package.json
    └── src/
```

## 🚀 Quick Start

### Prerequisites
- Docker and Docker Compose
- **Windows**: PowerShell (included) or Make (install via `choco install make`)
- **Linux/macOS**: Make (usually pre-installed)
- Optional: .NET 8 SDK and Node.js 18+ for local development

### For New Developers

**Windows (PowerShell):**
```powershell
# Complete setup (installs dependencies and builds containers)
.\build.ps1 setup

# Start development environment
.\build.ps1 dev
```

**Linux/macOS (Make):**
```bash
# Complete setup (installs dependencies and builds containers)
make setup

# Start development environment
make dev
```

That's it! Your backend will be at `http://localhost:8080` and frontend at `http://localhost:3000`.

## 📋 Available Commands

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

## 🛠️ Development Workflow

### Daily Development
1. `make dev` - Start your development environment
2. Make your changes
3. `make test` - Run tests
4. `make format` - Format your code
5. Commit and push

### Adding New Features
1. Create feature branch
2. `make dev-local` - For faster iteration during development
3. `make test` - Ensure tests pass
4. `make build` - Ensure Docker builds work
5. Create pull request

### Debugging
- `make logs` - View all service logs
- `make logs-backend` - View only backend logs
- `make logs-frontend` - View only frontend logs

## 🐳 Docker Structure

### Backend (`BackEnd/Dockerfile`)
- Multi-stage build for optimal image size
- Uses .NET 8 runtime and SDK images
- Exposes ports 8080 and 8081

### Frontend (`FrontEnd/Dockerfile`)
- Node.js build stage with pnpm
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
pnpm dev
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
2. Use `make help` to see available commands
3. Follow the development workflow above
4. Ensure `make test` passes before submitting PRs

## 📚 Tech Stack

- **Backend**: .NET 8 Web API
- **Frontend**: React + TypeScript + Vite + TailwindCSS
- **Containerization**: Docker + Docker Compose
- **Build Tool**: Make
- **Package Manager**: pnpm (frontend), NuGet (.NET)

## 🎯 Project Goals

This Old School RuneScape project aims to provide tools and utilities for OSRS players. The architecture is designed to be:

- **Scalable**: Easy to add new features and services
- **Maintainable**: Clear separation of concerns
- **Developer-friendly**: Simple setup and consistent workflow
- **Production-ready**: Optimized builds and deployment processes

## 🔜 Roadmap

- [ ] Add database integration
- [ ] Implement user authentication
- [ ] Add OSRS API integration
- [ ] Set up CI/CD pipeline
- [ ] Add comprehensive testing
- [ ] Performance monitoring and logging