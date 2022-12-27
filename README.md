# Tortilla Wars

"Tortilla Wars" es el nombre de mi prototipo para la tercera Práctica de Evaluación Continua (PEC3) de la asignatura Programación de Videojuegos 2D del Máster Universitario en Diseño y Programación de Videojuegos de la UOC.

El objetivo de la práctica era desarrollar un juego de artillería en 2D, similar a Scorched Earth o Worms, utilizando los conocimientos adquiridos en el estudio del tercer módulo de la asignatura y realizando investigación por cuenta propia.

## Vídeo explicativo




## Vídeo versión Android




## Versión jugable




## Repositorio en GitLab

[UOC - M7.456 - PEC3 en GitLab](https://gitlab.com/ragart-uoc/m7.456/uoc-m7.456-pec3)

## Cómo jugar

El objetivo del juego es vencer al resto de jugadores u oponentes. Para ello, los jugadores cuentan con un proyectil que se dispara de manera parabólica y que permite dañar al resto de jugadores u oponentes y destruir el terreno de juego.

El juego permite hasta 4 jugadores locales (pasando el control en cada turno) o controlados por la CPU.

En el caso de WebGL, el control se hace mediante teclado y ratón:

- La flecha izquierda y la tecla A mueven al personaje hacia la izquierda.
- La flecha derecha y la tecla D mueven al personaje hacia la derecha.
- El botón izquierdo del ratón dispara el proyectil hacia el punto en el que se haya hecho el clic, aunque siempre asegurando un ángulo mínimo de 45º. Si se mantiene pulsado antes de soltarlo, la fuerza del proyectil aumenta.

En el caso de Android, el control se hace mediante los botones en pantalla:

- El botón con la flecha izquierda mueve al personaje hacia la izquierda.
- El botón con la flecha derecha mueve al personaje hacia la derecha.
- El botón de disparo dispara el proyectil hacia la dirección en la que esté mirando el personaje en un ángulo de 45º y con una intensidad fija.

## Desarrollo

A efectos de cumplir lo solicitado en las instrucciones, el prototipo incluye lo siguiente:

- Cuatro personajes diferentes, cada uno con su propio proyectil, con más de dos animaciones y sistemas de partículas.
- Los personajes controlados por la CPU calculan la ubicación del personaje más cercano y ajustan la dirección y la fuerza del disparo en base a ello.
- Hay múltiples escenas, una clase persistente y una máquina de estados que permiten mostrar en pantalla datos del juego, incluyendo la vida de los jugadores y el estado de la partida.
- Se utilizan etiquetas y capas para la detección de colisiones y para la ubicación de objetos y componentes dentro del código.
- El juego se ha desarrollado tanto para WebGL como para Android, simplificando en este último caso algunas de las acciones.

De manera adicional, se han usado componentes como Cinemachine o el nuevo InputSystem para obtener un mayor control sobre el juego.

## Problemas conocidos
- La versión de Android presenta problemas con el movimiento y los disparos de los jugadores humanos.

## Créditos

### Fuentes
- "Moon 2.0" - Jack Harvatt - https://www.harvatt.house/moon-free
- "Akira Expanded" - Typologic - https://www.dafont.com/es/akira-expanded.font

### Imágenes y animaciones
- "Post-apocalyptic background" - PashaSmith - https://pashasmith.itch.io/post-apocalyptic-background
- "An egg in cartoon style" - brgfx - https://www.freepik.com/free-vector/egg-cartoon-style_28768528.htm
- "Pizza" - Mesym - https://pixabay.com/es/illustrations/pizza-pan-bocadillo-queso-desayuno-6682514/
- "Cebolla" - OpenClipart-Vectors - https://pixabay.com/es/vectors/cebolla-recorte-nuevo-lágrimas-161611/

### Sonidos
- "4 projectile launches" - Michel Baradari - https://opengameart.org/content/4-projectile-launches

### Música
- "The fall of Arcana" - Matthew Pablo - https://opengameart.org/content/the-fall-of-arcana-new-era-version
- "Boss Battle Theme" - CleytonKauffman - https://opengameart.org/content/boss-battle-theme
- "Myst" - Alexandr Zhelanov - https://opengameart.org/content/post-apocalypse-more-music-inside
- "Assasin's Assault" - HorrorPen - https://opengameart.org/content/assasins-assault

## Referencias

### C#
- "How can i shuffle a list" - Unity Answers - https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
- "Shuffling list without destroying it" - Sendatsu_Yoshimitsu [Reddit] - https://www.reddit.com/r/Unity3D/comments/36pzid/shuffling_list_without_destroying_it/

### Unity - General
- "GameObject.FindGameObjectsWithTag" - Unity Documentation - https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
- "Get all the childs in Transform even if the childs are inactive???" - Unity Forums - https://forum.unity.com/threads/get-all-the-childs-in-transform-even-if-the-childs-are-inactive.99452/

### Lanzamiento de objetos / cálculo de coordenadas
- "Rendering a Launch Arc in Unity" - Board To Bits Games [YouTube] - https://www.youtube.com/watch?v=iLlWirdxass
- "Pseudocoding adventures 1: AIM & THROW any game object (Unity, Rigidbody2d)" - ZeroKelvinTutorials [YouTube] - https://www.youtube.com/watch?v=zMtUs9BhXDQ
- "2D Throwable Bombs in Unity / 2022" - Distorted Pixel Studios [YouTube] - https://www.youtube.com/watch?v=l8K3KmOJUsI
- "Artillery shooting in Unity using Angle, Power & Trajectory" - GameDevLuuk [YouTube] - https://www.youtube.com/watch?v=otqueqkdm3g

### Android / Mobile
- "How to publish to Android" - Unity Learn - https://learn.unity.com/tutorial/how-to-publish-to-android-2
- "Android mobile scripting" - Unity Documentation - https://docs.unity3d.com/Manual/android-API.html
- "Mobile device input" - Unity Documentation - https://docs.unity3d.com/Manual/MobileInput.html
- "Landscape mode only" - Unity Answers - https://answers.unity.com/questions/774186/landscape-mode-only.html

### Tilemaps
- "Destroy Tile on Collision (Tilemap)" - Unity Forums - https://forum.unity.com/threads/destroy-tile-on-collision-tilemap.483751/

### Cinemachine
- "Class CinemachineVirtualCamera" - Unity Documentation - https://docs.unity3d.com/Packages/com.unity.cinemachine@2.1/api/Cinemachine.CinemachineVirtualCamera.html
- "Set limits for the position of the Cinemachine camera that follows the player" - Unity Forums - https://forum.unity.com/threads/set-limits-for-the-position-of-the-cinemachine-camera-that-follows-the-player.1070030/
- "2D World building w/ Tilemap & Cinemachine for 2D - Confined Follow Camera [4/8] Live 2017/22/08" - Unity [YouTube] - https://www.youtube.com/watch?v=M7v1TGQnJ7I

### New InputSystem
- "How To Use The New Unity Input System In A Platformer" - gamesplusjames [YouTube] - https://www.youtube.com/watch?v=1QWjm6yVp3g
- "Unity's New Input System for Mobile touch doesn't work" - gknicker [StackOverflow] - https://stackoverflow.com/questions/68839319/unitys-new-input-system-for-mobile-touch-doesnt-work