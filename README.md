# Tile-Matcher-Roll-Like
## Tiles
The tiles will be objects that vary in lifespan – for example an apple for a short lifespan, a diamond for a long one. Matching tiles counts as one turn
## Points
Each type of tile will have its own point pool (e.g. you have a separate count of apples and a separate count of diamonds).
In order of how long an object lasts every few turns you will lose an amount of it every few turns, where the least lifespan objects you lose every turn, the next least 2 turns, then 4, then 8, etc. (I will call this a countdown from now on)
The amount you lose is dependant on another point pool, decay. It starts at one and has its own tile that gets more and more likely to appear until you must match it. Once match the chance of decay tiles decreases to 0 again and decay is increased by 1. (If decay is 3 you’ll lose 3 diamonds if your diamond countdown ticks over)
You start with an amount of each object based on how fast its countdown is, the faster the countdown the more you start with. When one of the objects run out the game is over. You then get a score calculated by the multiplication of all you’re other object counts/points (if you run out of more than one object at once you get a score of 0).

## Shop
There will be a shop where you can buy upgrades (and maybe trade point types) that impact your game. For example, an upgrade that increases the amount you get from matching 4 in a rows.
The shop will have an upgrade cost, large pool of upgrades but limited selection, re-rolls, and scales with how long you have been going (things get more expensive).

## Objective
It’s probably pretty self-explanatory now but the objective is to get as high a score as possible which will be posted to a leaderboard.
