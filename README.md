# Snake Gaming Campus

Version 1.0.0: 20250907.

English Version (French below)
---

## Description

Your task is to make the snake eat apples. In order to do so, place **Direction blocks** in its path to change the direction he's heading in. Choose the direction of the block to place with the **arrow keys** and place the block by **clicking on a cell** with the mouse. If you need time to place your blocks, **press space to pause / restart the game**. You have a Timer and a limited number of direction blocks. Be quick and be careful.

The game has a tutorial level in which you can practice controlling the snake. The tutorial level automatically ends when the player eats a fixed number of apples. The game goes then in endless mode.

## File Descriptions

This project has six main folders:
- src/Entities contains all of our entities (building blocks of a level). These entities inherit from an Entity object. Main entities are the following:
    - Snake -> Contains the position of snake pieces on the grid. Takes care of the update of snake and snake collisions.
    - Apple -> Collectibles that snake has to retrieve.
    - DirectionBlock -> Blocks that the user can put on the grid in order to change the direction snake is going in.
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


Version française
---

## Description

Dans ce jeu, vous incarnez un ou plusieurs Snake, votre but est de manger le plus de pommes possibles. Afin de pouvoir controler tous les serpents, vous allez devoir placer sur leur chemin des **Blocs de direction**. Un snake qui marche sur un bloc de direction va adopter la direction pointée par le bloc.

Le joueur choisit la direction du bloc à placer avec les **flèches directionnelle** et place un bloc en utilisant le **clic gauche de la souris**. Si vous voulez ralentir le jeu afin de réfléchir, appuyez sur la **barre d'espace pour mettre le jeu en pause**. Les snakes ne bougent plus en pause, mais vous pouvez mettre des blocs de direction.

Le jeu ne possède **pas de condition de victoire (c'est un jeu infini)**. Le joueur perd la partie si les snake rentrent en collision avec eux-mêmes ou l'un avec l'autre. Il y a aussi un **timer** de 10s. Si les snake n'ont pas mangé toutes les pommes à l'écran à l'issue du timer, le joueur perd la partie. Le timer se réinitialise à chaque fois que les snake mangent toutes les pommes à l'écran.

Afin de familiariser avec les commandes, il est recommandé de faire le niveau tutoriel avant de se lancer dans le jeu.

## Composition du projet

Le projet possède six dossiers principaux :
- src/Entities : Contient toutes les entités du jeu. Elles héritent tous d'une classe abstraite entité. Les entitiés principales sont les suivantes:
    - Snake -> Contient une queue de cellules. Ces cellules permettent d'indiquer les cellules dans la grille occupées par snake.
    - Apple -> Collectible que Snake doit récupérer.
    - DirectionBlock -> Bloc que le joueur peut poser sur le terrain. Il possède une cellule indiquant la direction dans laquelle snake doit tourner quand il marche dessus et la position sur la grille du bloc.
- src/GridElements : Grille et cellules. Les cellules sont les cases de la grille et possèdent leurs propres opérateurs afin de pouvoir gérer les déplacements d'une cellule à une autre.
- src/Input : Ensemble de classes pour gérer les inputs utilisateurs. Pour l'instant, les contrôles utilisent la souris et le clavier. On aimerait bien pouvoir inclure un mode pour que le joueur puisse utiliser une manette.
- src/Scenes : Contient les niveaux et menus. Tous les objets sont des enfants de Scene. Afin de simplifier la construction, nous avons crée un objet abstrait level et un objet abstrait menu qui forme un "niveau générique" et un "menu générique". Les autres menus et niveaux héritent de ces classes abstraites.
- src/Services : Contient les services. Un service est un objet global dont on ne crée qu'une seule instance. Il est possible de trouver le service que l'on cherche grâce à notre Service Locator.
- src/Utils : Contient des objets génériques sont utiles en toutes circonstances (exemple : timer ou encore classe pour désigner des formes géométriques).

## Lancer le projet

Ce projet utilise la librairie Raylib.

Vous pouvez lancer le projet en vous rendant dans la direction du projet et en tapant la commande suivante :
```bash
dotnet run .
```

Passez une excellente journée.
