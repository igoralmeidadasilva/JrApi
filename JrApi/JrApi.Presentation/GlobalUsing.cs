global using FluentValidation;
global using JrApi.Presentation.Middlewares;
global using Microsoft.EntityFrameworkCore;
global using JrApi.Infrastructure.Data;
global using JrApi.Infrastructure.Repository.Interfaces;
global using JrApi.Infrastructure.Repository;
global using JrApi.Infrastructure.Repository.Caches;
global using System.Reflection;
global using MediatR;
global using JrApi.Domain.Entities;
global using JrApi.Infrastructure.Handlers.Commands;
global using JrApi.Application.Commands.Users;
global using JrApi.Application.Queries.Users;
global using JrApi.Infrastructure.Handlers.Queries;
global using JrApi.Application.Behaviors;
global using JrApi.Application.Validations;