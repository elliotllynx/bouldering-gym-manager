# 🧗 Bouldering Gym Route Setting Manager

A desktop MAUI application for bouldering gym route-setting management, built for gym staff and route setters to track, manage, and visualise boulder problems across gym walls. Originally build for school C# project.

## Features

- **CRUD for gyms, walls, and boulder problems**
- **Filtering** - filter problems by wall, grade, style, author, or days until retirement
- **Retirement tracking** - quickly surface problems that are expiring soon with a one-click filter
- **Grade statistics** - automatically calculates and displays the average grade across all currently visible problems
- **Style distribution chart** - pie chart showing the breakdown of Slab / Vertical / Overhang problems
- **Grade validation** - enforces Fontainebleau format (e.g. `6A`, `7B+`, `8C`)

## Tech Stack

| Layer | Technology |
|---|---|
| UI | .NET MAUI (Windows) |
| Architecture | MVVM via CommunityToolkit.Mvvm |
| Data access | Entity Framework Core |
| Database | SQLite |
| Charts | Microcharts + SkiaSharp |

## Project Structure

```
bouldering-gym-manager/
├── BoulderSetManager/           # MAUI application
│   ├── Models/
│   │   ├── Entities/            # DTOs (Gym, Wall, BoulderingProblem)
│   │   └── Services/            # Business logic & DB access
│   ├── ViewModels/              # MVVM view models
│   ├── Views/                   # XAML pages
│   └── MauiProgram.cs
└── DAL/                         # Data Access Layer
    ├── Entities/                # EF Core entity models
    └── GymDbContext.cs
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Windows 10/11
- Visual Studio 2022 with the **.NET Multi-platform App UI development** workload

### Running the App

1. Clone the repository:
```bash
   git clone https://github.com/elliotllynx/bouldering-gym-manager.git
```
2. Open `BoulderSetManager.sln` in Visual Studio
3. Set `BoulderSetManager` as the startup project and select **Windows Machine** as the target
4. Run with **F5** — the SQLite database is created automatically on first launch

## Usage

1. On launch, create or select a gym
2. Add walls to represent physical sections of your gym
3. Add boulder problems to each wall — specify grade, style, author, and built/retire dates
4. Use the filter bar to narrow down problems by any combination of attributes
5. Use style chart and average grade automatical updates to help with consistent and intentional route setting
