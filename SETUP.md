# Bucket Project Setup

_My clone of Pocket_

## Project Setup steps

1.  Using a terminal and create a new directory: Bucket

2.  Navigate into the new Bucket directory.

3.  Create project using dotnet cli
```shell
dotnet new webapi -o Kulba.Service.Bucket --no-https
```
4.  Create xunit test project using dotnet cli
```shell
dotnet new xunit -o Kulba.Service.Bucket.Tests
```
## Create Entity, Repository, and Controller
1. Remove WeatherForecast.cs
1. Remove WeatherForecastController.cs
1. Create new Entities/BookmarkEntity.cs as a Record Type
    - BookmarkEntity.cs




## Development Environment Setup

## Changes made to vscode launch configuration files.
1. launch.json file
    - Commented out serverReadyAction so that a new browser window does not open each time the debugger is started.
2. tasks.json file
    - Added new group section to enable Cntr+Shift+B to build project.


## Setup GitFlow
_Special note about gitflow:
The gitflow configuration is not persisted to the source code repository. When checking the project out for the first time, it is necessary to initialize gitflow._
```shell
gitflow init
```


Git flow init
