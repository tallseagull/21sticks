# 21 Sticks Learning

This is an example of how to teach the computer to play the game "21 Sticks".
Here is a link to an online game to play this game: https://atozmath.com/Games/21MatchStick.aspx

The game is very simple. We start with 21 sticks on the table. You and the computer take turns, each has to take between 1-3 sticks on each turn.
The player who picks up the last stick **looses** the game.

## Your Task
Your task is to write a computer program that learns the game. **Do not** program it with the simple mathematical solution, rather teach it how to learn. Read further to learn more how to do it.

To teach the computer how to play the game, there are 3 main steps:
1. Write code that lets you play against the computer. At this stage create a function that selects the next move for the computer, and use a random number between 1-3 for the move.
You should remember the moves both players made in the game, and who won in the end. Print the winner's moves and the loser's moves in the end.
A move for us is how many sticks are on the table before the move, and how many did the player take. For example, if we have 17 sticks and we take 2, our move is (17,2).
2. Now we want to keep count of each move - how many times it won, how many times it lost. For this you should add two arrays - one with wins, one with losses. Start with 0 in all cells in the arrays.
After each game, add 1 to the "wins" array for the moves the winner made, and add 1 to the "losses" array for the moves the loser made.
For example, if the winner's moves were (20,3), (15,2), ... we will add one to wins[20,3], wins[15,2], ...
3. Now replace your function that selects the computer move. The new function should look at the number of wins and number of losses for all potential moves.
For example, if we have 15 sticks on the table, then we look at wins[15,1], wins[15,2] and wins[15,3] and compare with losses[,] in the same places.
We want to calculate the win probability for each potential move (number of sticks we take) and pick the best move. See below on how to calculate the probabilities if you need help.
Now make the game run in a loop and restart every time if ends. Play with the computer 10-15 games, and see for yourself how it learns and gets better!

## Bonus:
As a bonus, you can also write code to make the computer play against itself (so two computer players competing). After playing itself, the computer can play against you with the arrays it learned. Make a loop where both players are computer players, and use the same logic to keep track of wins and losses.
Add the winning moves to the "wins" array, and the losing moves to the "losses" array. You can have the computer play itself 50, 100, 500 and 1000 times. See how well it plays with you after playing itself each number of times.

### Hints
A few hints to help:

#### Probability Calculations
To calculate the probability of winning, we want to divide the number of wins by the total number of games (win + loss).
So for example to calculate the probability of winning if we have 18 sticks and want to take 2, we calculate wins[18,2] / (wins[18,2] + losses[18,2])
However there is a problem here as well - we can divide by 0! The simple bypass is to add 1 to both the numerator and the denominator so the calculation is:
(wins[18,2] + 1) / (wins[18,2] + losses[18,2] + 1)
This works even if we have 0 wins and losses.

#### The optimal strategy
This game has a winning strategy. I'll tell you what it is, you need to think why :-).
The winner is the player who goes second, what he has to do is always complete the number the first player took so the sum is 4. For instance if the computer starts and takes 1, you take 3. If
he took 2, you take 2 and so on.


