# Snake Gaming Campus

Version 1.0.0: 20250907.

## Description

Your task is to make the snake eat apples. In order to do so, place **Direction blocks** in its path to change the direction he's heading in. Choose the direction of the block to place with the **arrow keys** and place the block by **clicking on a cell** with the mouse. If you need time to place your blocks, **press space to pause / restart the game**. You have a Timer and a limited number of direction blocks. Be quick and be careful.

The game has a tutorial level in which you can practice controlling the snake. The tutorial level automatically ends when the player eats a fixed number of apples. The game goes then in endless mode.

## File Descriptions

This project has six main folders:
- src/Entities contains all of our entities (building blocks of a level). These entities inherit from an Entity object.
- src/GridElements lets you create grids on which the entities can move and evolve.
- src/Input contains objects to handle user inputs. The game uses the keyboard and mouse to control the snakes. A version of the controls using a gamepad is in progress.
- src/Scenes contains all the levels and menus of the game as well as the transitions between scenes.
- src/Services contains all the global services. Objects that are created once and have a high level role.
- src/Utils contains generic objects that are not specific to any elements of the game but can be used by any of them.

## Launch this repository

This project uses the Raylib library.

You can launch the project by typing the command
```bash
dotnet run .
```

Have a great day.