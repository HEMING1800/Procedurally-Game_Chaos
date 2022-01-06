# Chaos
Chaos is a procedurally generated game. The project aims to create a 3D Role-play procedural generation game on the pc. The player plays a specific character to defeat enemies and access powerful equipment or skills. Also, when they complete certain levels, the game difficulty increases, and the procedural generation content changes.

## Maps
The game map generation depends on the max size which the designer input. Additionally, the map is made by tiles, the quad in Unity. After base tiles generation, the game invokes the Fisher-Yates Shuffle algorithm to randomly generate the obstacle positions stored in the queue to avoid duplication.

Next, the Flood fill algorithm is invoked to implement the consecutive path for the player on the map. It avoids randomly generated obstacles surrounding open spaces. After the tile accessible is checked by the Flood fill algorithm, obstacles are generated and displayed on the map. The obstacles cube will be exchanged for the other assists, depending on the different map styles. For more precise management, all obstacles and tiles belong to the “Generated Map,” a child component. When the map setting is changed, such as reducing the max map size or increasing the obstacle percent, the “Generated Map” will be immediately destroyed for free memory. 

There are certain waves of attack on each map. If all attack waves finish and the player survives, the following map will be loaded. The tile which is generated will flesh different colours to remind the player. The enemies could randomly be generated during each wave on the open space of the map. After generation, enemies will move toward the player position. If the player does not move for a long time, the enemies will be generated on the player tile. It only has an enemy type who attacks the player when they near the player. The enemy types will be increased with different attack ways and gadgets.

## Fighting System(Continue working)
The enemies’ attack can reduce the player’s health level. If the health level equals to zero, the game over. On other hand, the player uses different gun to shoot the enemies. Different gun has different projectile and damage.

## References
The project uses the suggestion about professional game development progress and what designer need to consider during development in the Game Design Workshop, written by Tracy Fullerton. As she mentioned in the book, ”Inviting feedback from players early on and is the key to designing games that delight and engage the audience because the game mechanics are developed from the ground up with the player experience at the center of the process,” the game should advocate for the player.

For the procedural generation, the project was learned the Fisher-yates shuffle algorithm and Flood fill algorithm from the videos produced by Sebastian Lague and Leios Labs on YouTube. Meanwhile, the project is considered to add Perlin noise to some of the maps for the performance comparison and particular content generation. The procedural terrain generation implied in the game Minecraft will be referenced

At the current stage, the other procedural generation is not made. The initial idea is that the game should procedurally generate some characters, such as different enemies and the weapon system. Some books may be referenced, such as Unity Shader Tutorial, written by Lele Feng.

Additionally, the project uses assets from the Unity engine for the primary demonstration, including guns, bullets, obstacles, characters, and enemies. Each obstacle has used the cube instead of the mountain rock, tress. Further formal assets will be made on the Blender or purchased and downloaded from the Unity Assets Store.

The reference list:
1.	Game Design Workshop: A Playcentric Approach to Creating Innovative Games by Tracy Fullerton
2.	Sebastian Lague’s YouTube address:
https://www.youtube.com/c/SebastianLague/videos 
3.	Leios Labs’s YouTube address:
https://www.youtube.com/watch?v=ldqAmkdthHY&list=LL&index=3&t=22s
4.	Unity Shader Tutorial by Lele Feng
5.	Unity Assets Store：https://assetstore.unity.com/
