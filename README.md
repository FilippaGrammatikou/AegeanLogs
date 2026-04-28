# AegeanLogs

AegeanLogs is a backend-first ASP.NET Core Web API that models maritime port-call operations for a simulated port-service environment.

The project focuses on one vessel visit to one port and tracks the operational work required to move that visit from planning to closure: service jobs, required documents, supplier updates, blockers, readiness checks, role-based decisions, and audit history.

The purpose of this repository is to demonstrate production-shaped backend development with ASP.NET Core, Entity Framework Core, SQL Server, DTO-based API contracts, JWT authentication, role-based authorization, validation, structured error handling, tests, Docker, and GitHub Actions CI.

## Core idea

A port call should not move forward just because a user clicks a button.

It should move forward only when the required work is complete, the required documents are checked, and no critical blockers remain.

## Main features planned

- Port-call lifecycle management
- Service job workflow
- Required document tracking
- Readiness score and blocker detection
- Role-based access control
- Audit history for important changes
- Pagination, filtering, and clean DTO responses
- Validation and ProblemDetails-style errors
- Unit and integration tests
- Dockerized local setup
- GitHub Actions CI

## Status

Initial architecture and development setup phase.
