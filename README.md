# Test_Arena
//this project is my test task

Task description:

Player:
Starting stats:
Health - 100/100
Strength - 50/100

The player can be controlled through 2 controls for movement and turning. By clicking the attack button, the player launches a projectile from the center of the screen, which kills the target on impact, with a small chance that the projectile may fly further or bounce off the nearest enemy after killing it. (When the player's health is low, the chance of a bounce increases up to 100%).

For killing an enemy, the player earns strength points (energy for the "Ultimate" ability):
50 points for a blue enemy
15 points for a red enemy

If the player kills an enemy with a secondary ricochet projectile, they receive a small amount of strength or half of their health replenished.

When the player has full strength, they can activate the "Ultimate" ability by clicking on the button, which kills all enemies.

When the player approaches the edge, they will be teleported to a random location on the platform away from all enemies.

Enemies:
Starting stats for enemies:
Blue Enemy Health - 100/100
Red Enemy Health - 50/50

Blue Enemy - large and slow (boss), attacks the player with a long-range shot. The projectile follows the player until it hits them, taking away the player's strength (25 points). If the player teleports from the edge, the projectiles continue to fly to the point until the player moves, then disappear without causing harm. Interval between shots - 1 every 3 seconds.

Red Enemy - upon appearing, flies up a certain distance (higher than others), then stops for a while and then flies towards the player, taking away their health (15 points) and dying on impact.

Every 5 seconds, enemies spawn on the surface of the platform, with each spawn reducing the spawn time to a minimum of 2 seconds and increasing the number of enemies spawned. The ratio is 1 blue enemy to 4 red enemies.

Menu/UI:
If the player is killed, a menu opens with a "Restart" button and the number of enemies killed.
Additionally, a pause button and controls, indicators of life, progress bar for the "Ultimate" ability, hit indicators, etc. may be added to the screen.

Also, I added saving the record of defeated enemies to the project.
