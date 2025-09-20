# Multi-stage build for React frontend using Vite

# Build stage
FROM node:22 AS build
WORKDIR /app

# Copy package files first for better caching
COPY package*.json pnpm-lock.yaml ./

# Install pnpm and dependencies
RUN corepack enable && corepack prepare pnpm@latest --activate
RUN pnpm install --frozen-lockfile --silent

# Copy source code
COPY . .

# Build the application
RUN pnpm run build

# Production stage with Nginx
FROM nginx:alpine AS final

# Copy built files to Nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Optional: Copy custom Nginx config if needed
# COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]