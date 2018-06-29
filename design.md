# Architecture

## Introduction

Ce document a pour but d'expliquer la manière dont le code est structuré. Avec les règles respectées pour avoir un développement en équipe de manière optimale.

## Règles d'architecture

Chaque fonctionnalité est un package correctement nommé, par exemple tous ce qui concerne le personnage du joueur sera dans `assets/scripts/player`.
![Package](https://i.imgur.com/5Pglt4F.png)

Chaque animation du personnage, des ennemies ou de certain objet est le défilement de plusieurs sprites situé dans le dossier ‘assets/Sprites’, l’ensemble des animations de ces sprites est dans ‘assets/Animations’. Il y a donc plusieurs animations de créer pour le personnage comme par exemple la marche ou l’état repos de même pour les ennemies.
![Animator](https://i.imgur.com/ZFQ8YcG.png)
![Animator](https://i.imgur.com/pNZOfLS.png)
![Animator](https://i.imgur.com/z8RXwMJ.png)

Un animator est créé à partir des animations et regroupe la totalité des animations liés à une unité avec différent transition entre chaque animation.
![Animator](https://i.imgur.com/u1Guyf1.png)


L'architecture de ce projet se base principalement sur le modèle [SOLID](https://fr.wikipedia.org/wiki/SOLID_(informatique)) (single class responsibility ...), ce qui nous permet d'avoir un code facile à comprendre, maintenable et réutilisable dans d'autres futures projets.

## Modèle statique

Afin de définir la structure de base des assets du projet sous Unity, nous avons utilisé ce repository : [Unity-Directory-Structure](https://github.com/LionelJouin/Unity-Directory-Structure)

Le joueur et l'IA utilisent des classes communes et génériques : les classes `Character`.
`CharacterRaycaster` : permet de détecter l'objet interactif le plus proche (null si pas d'objet ou trop loin) du player ou de l'IA. L'objet interactif est envoyé à `CharacterInteraction`
`CharacterInteraction` : Attend les actions du joueur ou de l'IA. Les actions peuvent être : attaquer, utiliser, prendre.
`CharacterController` : Permet de faire bouger le joueur ou l'IA
`Inventory` : Inventaire de l'IA ou du joueur, contient les objets `Catchables`
`Animator` : Lance les animations en fonction des actions
Diagramme de classes de la partie Character, Player et IA:
![Character](https://i.imgur.com/Kxzn4JD.png)


Les objets Unity contenant une classe qui hérite de `Interactive` sera détecté par `CharactereRaycaster` afin d'y effectuer des actions : Utiliser pour les objets `Activables` et prendre pour les objets `Catchables`. Toutes les propriétés `public` de ces classes sont des paramètres modifiables dans l'inspecteur de Unity. Grâce à ces classes, nous avons pu créer des `Prefabs` pour les objets Interactifs.
Diagramme de classes de la partie Interaction :
![Interaction](https://i.imgur.com/WCoSG7U.png)

### Génération procédurales

La génération prcocédurale est divisée en 5 parties:

#### 1ère partie

Génération des salles dans un tableau a deux dimensions de `Room` qui est la version simplifier de `RoomInstance` pour avoir une génération plus rapide et avoir l'essentiel pour la suite.
Le générateur essaye de mettre le moins de salle possible collée afin de faire un dongeon en mode couloir, mais tout en ayant certaines zones avec plus de salle collées.

#### 2ème partie

Le générateur cherche les pièces adjacentes pour chaque pièce et complète la classe room pour afficher les portes ou non sur la map.

#### 3ème partie

Le générateur cherche des emplacements dans zones où il existe qu'une seule salle adjacente. Il place la salle du boss, puis les salles secrètes (5% de chance) et enfin les salles au trésors (10% de chance).

#### 4ème partie

Le générateur créer une map à partir du `MapSprite` avec l'aide de `MapSpriteSelector` qui choisira le bon sprite avec les portes au bon endroit et affichera la couleur en fonction du type de pièce.

#### 5ème partie

Le générateur converti les `Room`en `RoomInstance` et génére physiquement la map à partir des emplacements des différentes pièces dans le tableau en 2 dimensions.

![Environnement](https://i.imgur.com/LAN4iGH.png)

## Outils utilisés

Afin de développer ce jeu, nous avons utilisé le moteur de jeu [Unity](https://unity3d.com/fr) (version 2018.1.1f1) sur Windows 10 avec les environnements de développement [Visual Studio Community 2017](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/). Nous avons versionné notre projet Unity grâce à [Git](https://git-scm.com/) avec [Github](https://github.com/istic-student/roguelike). 

De manière à travailler en groupe de la façon la plus productive possible, nous avons utilisé plusieurs outils pour communiqué et partagé nos avancées. [Messenger](https://www.messenger.com/) nous a permis de nous parler en temps réel. [Trello](https://trello.com/b/B4JwvxdB/roguelike) nous a permis de définir les taches et de faire pars de nos avancées. Afin de rédiger les documents, nous avons choisis d'utiliser [Google Drive](https://drive.google.com/drive/folders/10aP7FhrHG_gJ-YKKqGxbI6g_oWgnBPhk). Dans le but de réaliser des réunions avec notre tuteur de projet, nous avons utilisé [Skype](https://www.skype.com/fr/).
