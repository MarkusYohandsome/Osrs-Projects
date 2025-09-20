# OSRS Project Makefile
# Provides unified commands for development, building, and deployment

.PHONY: help build build-backend build-frontend dev dev-backend dev-frontend test clean up down logs install

# Default target
help: ## Show this help message
	@echo "Available commands:"
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-20s\033[0m %s\n", $$1, $$2}'

# Installation and Setup
install: ## Install all dependencies for frontend and backend
	@echo "Installing frontend dependencies..."
	cd FrontEnd && pnpm install
	@echo "Restoring backend dependencies..."
	dotnet restore "Old School Runescape Project.csproj"
	@echo "Dependencies installed successfully!"

# Development Commands
dev: ## Start both frontend and backend in development mode
	docker-compose up --build

dev-backend: ## Start only the backend in development mode
	docker-compose up --build backend

dev-frontend: ## Start only the frontend in development mode
	docker-compose up --build frontend

dev-local: ## Run frontend and backend locally (no Docker)
	@echo "Starting backend..."
	start powershell -Command "cd BackEnd; dotnet run"
	@echo "Starting frontend..."
	cd FrontEnd && pnpm run dev

# Build Commands
build: ## Build both frontend and backend Docker images
	docker-compose build

build-backend: ## Build only the backend Docker image
	docker-compose build backend

build-frontend: ## Build only the frontend Docker image
	docker-compose build frontend

build-local: ## Build projects locally without Docker
	@echo "Building backend..."
	dotnet build "Old School Runescape Project.csproj" -c Release
	@echo "Building frontend..."
	cd FrontEnd && pnpm run build

# Testing Commands
test: ## Run all tests
	@echo "Running backend tests..."
	dotnet test "Old School Runescape Project.csproj"
	@echo "Running frontend tests..."
	cd FrontEnd && pnpm run test

test-backend: ## Run only backend tests
	dotnet test "Old School Runescape Project.csproj"

test-frontend: ## Run only frontend tests
	cd FrontEnd && pnpm run test

# Docker Management Commands
up: ## Start all services (equivalent to dev)
	docker-compose up -d

down: ## Stop all services
	docker-compose down

restart: ## Restart all services
	docker-compose restart

logs: ## View logs from all services
	docker-compose logs -f

logs-backend: ## View logs from backend service
	docker-compose logs -f backend

logs-frontend: ## View logs from frontend service
	docker-compose logs -f frontend

# Cleanup Commands
clean: ## Clean up Docker containers, images, and build artifacts
	docker-compose down -v
	docker system prune -f
	@echo "Cleaning backend build artifacts..."
	dotnet clean "Old School Runescape Project.csproj"
	@if exist "bin" rmdir /s /q bin
	@if exist "obj" rmdir /s /q obj
	@echo "Cleaning frontend build artifacts..."
	@if exist "FrontEnd\dist" rmdir /s /q FrontEnd\dist
	@if exist "FrontEnd\node_modules" rmdir /s /q FrontEnd\node_modules

clean-docker: ## Clean up Docker containers and images only
	docker-compose down -v
	docker system prune -f

# Production Commands
prod: ## Build and start in production mode
	docker-compose -f docker-compose.yml -f docker-compose.prod.yml up --build

deploy: ## Deploy to production (customize as needed)
	@echo "Deploying to production..."
	$(MAKE) build
	# Add your deployment commands here

# Database Commands (if you add a database later)
db-setup: ## Set up database (placeholder for future use)
	@echo "Database setup would go here"

db-migrate: ## Run database migrations (placeholder for future use)
	@echo "Database migration would go here"

# Utility Commands
format: ## Format code in both projects
	@echo "Formatting backend code..."
	dotnet format "Old School Runescape Project.csproj"
	@echo "Formatting frontend code..."
	cd FrontEnd && pnpm run format || echo "No format script found in package.json"

lint: ## Lint code in both projects
	@echo "Linting frontend code..."
	cd FrontEnd && pnpm run lint || echo "No lint script found in package.json"

status: ## Show status of all services
	docker-compose ps

# Quick setup for new developers
setup: install build ## Complete setup for new developers
	@echo "Setup complete! Run 'make dev' to start development"

# Show project info
info: ## Display project information
	@echo "OSRS Project Information:"
	@echo "Backend: .NET 8 Web API"
	@echo "Frontend: React + TypeScript + Vite"
	@echo "Database: TBD"
	@echo "Containerization: Docker + Docker Compose"
	@echo ""
	@echo "Quick start: make setup && make dev"