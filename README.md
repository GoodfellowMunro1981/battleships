# Battleships
Battleships C# Console App


## Requirements

Implement a game of battle ships. If you’ve never played the game, you can get a feel for it from this online game.
You should create a console application to allow a single human player to play a one­sided game of battleships against the computer.
The program should create a 10x10 grid, and place a number of ships on the grid at

random with the following sizes:
•	1x Battleship (5 squares)
•	2x Destroyers (4 squares)

The console application should accept input from the user in the format “A5” to signify a square to target, and feedback to the user whether the shot was success, and additionally report on the sinking of any vessels.


## step 1

enter prompt to ChatGPT to generate simple code from Requirements as existing non custom logic required

setup a new new c# Console App, paste code from ChatGPT 

overview of console app to find any basic potential issues

## basic smoke testing

ensure core functionality works as expected with some basic manual testing

1) start application

2) enter a valid co-ordinate A1 - see message in console

3) enter the same valid co-ordinate again A1 - see message in console, does the code know its already been entered?

4) enter an invalid co-ordinate AA - see message in console, does it highlight it's invalid

5) enter an invalid co-ordinate 11 - see message in console, does it highlight it's invalid

6) continue testing till a ship is found and sunk to ensure message is correct


## Git Repo setup

setup repo on Github and commit/push changes for basic game so changes can commit history can be viewed


## Code improvements/refactor 

split code into appropiate seperations

Entities
	- ship
	- grid
	
Service/Logic
	- actions to play game
	
Tests
	- unit tests for Logic
	
Helpers
	- constant values

Ensure code allows easier configuraiton chanages
- grid size
- number of ships
- ship types
- messages to user


