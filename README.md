# Space Invaders Game Assessment

## Introduction

Hello,

This repository contains my submission for the Space Invaders game assessment as part of my application for the senior gameplay programmer role at TrailBlazerGames. I appreciate the opportunity and look forward to presenting my work.

## Game Description

I have created a simple Space Invaders game based on the provided instructions. Here is an overview of the game components:

### Player Spaceship

- A player-controlled spaceship can be moved horizontally using the left and right arrow keys or 'A' and 'D' keys.
- The spaceship is confined within the game screen boundaries.
- I have not implemented a mechanism to support various spaceship skins. But the idea is have various skin of spaceship as a prefab assign them a unique id via script, or just a value in enum. we can load them dynamically by asset bundle or in this case just simply reference to a list of skin to instantiate it.

### Alien Invaders

- A group of alien invaders moves horizontally from left to right and back.
- When an invader reaches the screen's edge, it drops down and changes direction.
- The speed of the invaders gradually increases over time.
- I have designed the code to accommodate multiple types of invaders (red, green and blue) with different score and hp require to take it down. A row will be full of one type of invader, that can be instantiate randomly when player hit play game.
- All of value above could be modify by gamedesigner through scripable object. Or if in a large scale game with tons of data, it can be configure through spreadsheet, or excel file

### Bullets

- The player can shoot bullets upwards using the spacebar key.
- Bullets are instantiated from the player's spaceship and move upward until they hit an alien invader or go off-screen.
- When a bullet hits an alien invader, the invader is destroyed.
- I have planned the code to support various types of bullets with different effects 
(fast bullet that fly fast but low damage, heavy bullet fly slow but bigger and high damage, a zigzag bullet that flight in a zigzag pattern).
Player can choose what bullet to fire by pressing TAB on keyboard

### Scoring

- I have implemented a scoring system to track the player's score.
- The player earns points for each alien invader destroyed.
- Different types of alien invaders can generate different scores.

### Game Over

- The game ends under two conditions:
  1. All invaders are destroyed, and the player wins.
  2. An invader reaches the bottom of the screen, resulting in a loss.

### User Interface

- The game features a simple user interface displaying the player's score.
- A game-over message is shown when the game ends (win or lose).
- We can consider supporting localization by specify a spreadsheet contain multiple row, which primary key is a readable string in english, and other colums is the translation to that language acordingly.
For example : https://docs.google.com/spreadsheets/d/17e_6k1_aooK9DBjIhpcMuq8LhA0N43zpxAY07DtxAdg/edit?usp=sharing
After that, we can create a custom tool to convert this spreadsheet file into json or byte to use in game. Then depends on the text we need and in what language we can look them up it the custom data

## Repository Contents

This repository contains the following:

- Unity project folder
- Video demonstration of the gameplay : https://screenrec.com/share/nwqP2VTZ0l, in second play, i have speed up the game to make it faster to the game over screen
- Instructions on how to run the game : Play the SpaceInvader scene
- Additional notes : Press A, D to move, Space to shooot, TAB to change bullet