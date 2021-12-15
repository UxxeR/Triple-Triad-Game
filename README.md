# Triple Triad
Triple Triad minigame from Final Fantasy VIII made in Unity 2021.1.
By default all rules are enable.

## How to play?
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

## Art
Every piece of art in this project was self-made using Aseprite.

## Features
- [x] Capture cards.
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
- [ ] Enemy advanced AI.
- [ ] Deck building.
- [ ] Unlocking cards.
- [ ] Settings (including rule settings).
