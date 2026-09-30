# Reto Técnico - React Native + .NET

Mini aplicación móvil de gestión de tareas.

## Tecnologías

- .NET 8
- ASP.NET Core Web API
- React Native CLI
- TypeScript
- SQL Server
- Git

## Arquitectura

El backend utiliza una arquitectura basada en capas:

- Domain
- Application
- Infrastructure
- API

## Funcionalidades

- Listar tareas
- Filtrar por estado
- Filtrar por prioridad
- Consultar detalle

## Base de datos

Ejecutar:

database/script.sql

Este script crea:

- Base de datos
- Tabla Tasks
- Datos iniciales
- Procedimientos almacenados

## Backend

Entrar a:

backend/TaskManagement.Api

Ejecutar:

dotnet restore
dotnet build
dotnet run

## Frontend

Entrar a:

frontend/TaskManagementApp

Ejecutar:

npm install
npm start

En otra terminal:

npm run android

## Endpoints

GET /api/tasks

GET /api/tasks?status=Pendiente

GET /api/tasks?priority=Alta

GET /api/tasks/{id}

## Decisiones técnicas

Se utiliza Clean Architecture para separar responsabilidades y
facilitar mantenimiento y pruebas.

Los procedimientos almacenados encapsulan las consultas
principales de la aplicación.

React Native consume la API mediante HTTP/JSON.
