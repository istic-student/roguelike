# RogueLike

## Introduction

> Rennes 1 University' bachelor project - roguelike made with Unity

> Roguelike is a subgenre of role-playing video game characterized by a dungeon crawl through procedurally generated levels,  turn-based gameplay, tile-based graphics, and permanent death of the player character. Most roguelikes are based on a high fantasy narrative, reflecting their influence from tabletop role playing games such as Dungeons & Dragons.

[Wikepadia](https://en.wikipedia.org/wiki/Roguelike)  

[Short video of the game](https://www.youtube.com/watch?v=KGxtIgiZCT0&feature=youtu.be)

## Requirements

This project requires the following softwares/modules/libraries:
* Unity (2018.1.1f1 or higher)

## Resources

* https://trello.com/isticstudent
* https://github.com/istic-student/roguelike
* https://projetl3istic20172018.slack.com/messages/CAER9D37W/
* https://drive.google.com/drive/folders/10aP7FhrHG_gJ-YKKqGxbI6g_oWgnBPhk

## Authors

* **Lionel Jouin** - [LionelJouin](https://github.com/LionelJouin)  
* **Emily Delorme** - [emilydelorme](https://github.com/emilydelorme)  
* **Nicolas Girault** - [Rhohen](https://github.com/Rhohen)  
* **Quentin Loustau** - [qloustau](https://github.com/qloustau)  

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## Controls

### Keyboard

- Up : Z
- Down : S
- Left : Q
- Right : D
- Action: A
- Take object: E
- attack: Space

### Controller

- Move: Left Joystick
- Action: X
- Take object: B
- attack: Y


## Features

- [x] Player controller (Keyboard and Gamepad)
- [x] Character
	- Control (Move...)
	- Interaction (Attack, Catch, Use...)
	- Raycaster
	- Health
	- Inventory
- [x] Interactive
	- Activable
		- Door
		- Chest
		- Shop
	- Catchable
		- Passive
		- Consumable (Key, Coin...)
		- Equipment (Weapon, Armor...)
	- Other
		- Breakable
- [x] Offline multiplayer
- [x] Weapon
	- Melee weapon
	- Ranged weapon
		- Projectile
	- Enemy weapon
- [ ] UI
	- Player UI
- [ ] IA
	- EnemyController

		- Random movment

		- Movment toward player

		- Detection range

		- Attack range

		- Attack system

	- Boss Necromancer

		- 3 phase attack system

		- Summon of skeletton minion
- [ ] Online multiplayer
- [x] Procedural generation
	- Generation like the binding of isaac
	- Room Types
		- Normal Room
		- Treasure Room
		- Boss Room
		- Secret Room
	- Obstacles
	- Enemmies spawn when entering room for the first time
- [x] Shaders and Light
	- Player produce lights
		- Directionnal ligh
		- Round light around the player
	- Room obstacles generate shadow
	

## Screenshot

![Map generation](https://i.imgur.com/N7jzmkU.png)
![Map generation](https://i.imgur.com/azAYkcp.png)
![Map generation](https://i.imgur.com/rYynkUV.png)  
Legend:
- Blue: spawn
- Yellow : treasure room
- Pink: secret room
- Red: boss room

[![Demo lightt](https://i.imgur.com/KNU563K.png)](https://imgur.com/a/oHPyaKJ)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
