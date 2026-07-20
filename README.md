# Bouldering Gym Route Setting Manager

A desktop MAUI application for bouldering gym route-setting management, built for gym staff and route setters to track, manage, and visualise boulder problems across gym walls. Originally build for school C# project. 

## Overview

With bouldering on rise and climber customer apllications flodding the market I found an unused potential in bouldering apps focused on gym rouse-setters and staff.
My app is designed with their needs and perspective as the top priority for feature development. Creating an intuitive UI with many build in features that should ensure the best experience. 

## Features

- **CRUD and input validations for gyms, walls, and boulder problems**
- **Filtering** - filter problems by wall, grade, style, author, or days until retirement
- **Retirement tracking** - quick and visual signs of soon retiring boudlers
- **Grade statistics** - automatically calculates and displays the average grade across all currently visible problems
- **Style distribution chart** - automatically calculated pie chart showing the type distribution of currently visible boulders

<img width="1424" height="837" alt="image" src="https://github.com/user-attachments/assets/4cd99042-6f12-4108-beb6-2593244b28ef" />


**+ plan on adding soon:**
- archive (gym, wall, boulder) - working on right now
- light/dark mode toggles
- integrated json file inport/export of individual gyms and their walls and boulders
- visual interactive map manager
- Azure-hosted REST API backend (ASP.NET Core + Azure SQL) for multi-device sync

## Tech Stack

| Layer | Technology |
|---|---|
| UI | .NET MAUI (Windows) |
| Architecture | MVVM via CommunityToolkit.Mvvm |
| Data access | Entity Framework Core |
| Database | SQLite |
| Charts | Microcharts + SkiaSharp |

## Project Structure - MVVM architacture in directories

```
bouldering-gym-manager/
├── BoulderSetManager/           # MAUI application
│   ├── Models/
│   │   ├── Entities/            # DTOs (Gym, Wall, BoulderProblem)
│   │   └── Services/            # database access servises
│   ├── ViewModels/              # view models
│   ├── Views/                   # views (xaml files)
│   └── MauiProgram.cs
└── DAL/                         # Data Access Layer
    ├── Entities/                # EF Core entity models (Gym, Wall, BoulderProblem)
    └── GymDbContext.cs          # databse state holder
```

## Getting Started

### Prerequisites

- .NET 10
- Windows 10/11
- Visual Studio

### Running the App

1. Clone the repository:
```bash
    git clone https://github.com/elliotllynx/bouldering-gym-manager.git
```
2. Open `BoulderSetManager.sln` in Visual Studio
3. Set `BoulderSetManager` as the startup project and select **Windows Machine** as the target
4. Run with **F5** (will run with preseeded data)
5. for own database management comment out annotated section in OnModelCreating in DAL/GymDbContext and "db.Database.EnsureDeleted()" in BoulderSetManager/SelectGymViewModel.cs

## Any feedback is greatly welcomed!

Thank you for reading my first proper project and readme file. I enjoyed so much of this process and if you have any suggestions, improvements or feedback of any kind it mind, please reach out to me wherever you are the most comfortable with. 
