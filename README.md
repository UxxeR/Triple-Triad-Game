# 🎴 Triple Triad 🎴
Triple Triad minigame from Final Fantasy VIII made in Unity 2022.1. 
By default all rules are enabled.

## User interface 👀
### Menu
<img src="https://user-images.githubusercontent.com/25460362/180471161-76036169-9057-4cfd-b1de-e0b2f85f5819.png" width="800" height="450" alt="Menu image"/>

### Settings
<img src="https://user-images.githubusercontent.com/25460362/180787551-98316e5b-6a60-450a-93b6-eef56ceb7eda.png" width="800" height="450" alt="Settings image"/>
<img src="https://user-images.githubusercontent.com/25460362/180787604-df93341d-60c0-4f71-a071-774337978d14.png" width="800" height="450" alt="Settings image"/>
<img src="https://user-images.githubusercontent.com/25460362/180787653-830dd2df-8ca7-4661-9d6a-98b167e4c757.png" width="800" height="450" alt="Settings image"/>
<img src="https://user-images.githubusercontent.com/25460362/180787708-c4203279-01ad-453b-859b-41564b8888f9.png" width="800" height="450" alt="Settings image"/>

### Collection
<img src="https://user-images.githubusercontent.com/25460362/180471386-e53749a3-b9b4-4a9a-99db-69f1c5e18026.png" width="800" height="450" alt="Collection image"/>

### Deck selection
<img src="https://user-images.githubusercontent.com/25460362/180471444-bbb584c1-0ae6-453a-a1e0-d453ae6fbea4.png" width="800" height="450" alt="Deck selection image"/>

### Deck editor
<img src="https://user-images.githubusercontent.com/25460362/180471574-48165ca6-7663-4044-9058-8e05c1a66991.png" width="800" height="450" alt="Deck editor image"/>

### Gameplay
<img src="https://user-images.githubusercontent.com/25460362/180471904-b5a14376-5bed-462b-a85f-072beca7dc99.png" width="800" height="450" alt="Gameplay image"/>

## How to play? 🎮
You are the blue team and your objective is to capture as many cards from the red team as possible.
If your points are greater than 5 you win, else you will lose.
Each side of your card has a power level, if it is greater than the opposite side power of the enemy's card you will capture his card.

The game has different rules that can be enable or disable.

### Elemental rule
In the elemental rule, one or more of the slots are randomly marked with an element. Some cards have elements in the upper-right corner. 
If the card has an element and match with the slot's element, the power of the card will increase in 1. Else will decrease in 1.

### Random rule
Five cards are randomly chosen from the player's deck instead of the player being able to choose five cards themselves.

### Same rule
When a card is placed touching two or more other cards (one or both of them have to be the opposite color), and the touching sides of each card is the same, then the other two cards are flipped. Combo rule applies.

### Plus rule
When one card is placed touching two others and the power touching the cards plus the opposing power equal the same sum, then both cards are captured. Combo rule applies.

### Same wall rule
An extension of the Same rule. The edges of the board are counted as 10 power for the purposes of the Same rule. Combo rule applies.

### Sudden death rule
If the game ends in a draw, a sudden death occurs in which a new game is started but the cards are distributed on the side of the color they were on at the end of the game.

### Combos
Of the cards captured by the Same, Same Wall or Plus rule, if they are adjacent to another card whose power is lower, it is captured as well.

## Art 🎨
Every piece of art in this project was self-made using Aseprite.

# Game localization
Uses the Unity localization package v1.0.5.
The game is available to play in both English and Spanish.

## Features ✔️
- [x] Capture cards.
- [x] Easy card creation.
- [x] Points system.
- [x] Turn system.
- [x] Elemental rule.
- [x] Random rule.
- [x] Same rule.
- [x] Plus rule.
- [x] Same wall rule.
- [x] Sudden death rule.
- [x] Combos.
- [x] Enemy basic AI.
- [x] Enemy advanced AI.
- [x] Turn timer.
- [x] Settings (including rule settings).
- [x] Deck building.
- [x] Unlocking cards.
- [x] Game localization.
