# RaceDay MVC - Final Part 3

## Overview
MVC web app consuming RaceDay API (Part 2). Implements session auth, role-based UI (Organizer/Participant), Blob image upload via API, Docker.

## Run API (Part 2)
cd RaceDay.API
dotnet ef database update
dotnet run
# API runs at http://localhost:5131

## Run MVC locally
cd RaceDay.MVC
dotnet run
# MVC runs at http://localhost:5130 (check launchSettings)

## Docker Run (Submission requirement)
docker build -t raceday-mvc .
docker run -d -p 8080:80 --name raceday raceday-mvc
# Access at http://localhost:8080

## Azure Blob Storage
Add to RaceDay.API/appsettings.json:
"AzureBlob": { "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=...;AccountKey=...;EndpointSuffix=core.windows.net" }

API endpoints:
POST /api/Upload/event-banner (IFormFile file)
POST /api/Upload/profile-picture

Images stored in container `raceday-images` with public blob access.

## Features Implemented
- [x] MVC consumes API only, no direct DB
- [x] Session management for token/role
- [x] Organizer Dashboard, Event CRUD with banner upload
- [x] Participant Home, Details, Enrol, MyEnrolments, MyResults, Profile
- [x] Blob Storage via API
- [x] Docker file best practices
- [x] Bootstrap sporting theme, colour-coded enrolment status

## Video Script
See Report for YouTube demo steps: show login, organizer create event with banner, participant enrol, dashboard, Docker.
