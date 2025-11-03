# GAMİZO

GAMİZO is a retro-style arcade game developed using the Unity game engine. Players control the on-screen character through a joystick and interactive mechanics, avoiding enemies and completing various in-game tasks. This project serves as an experimental example of game development, showcasing different gameplay mechanics and modern development techniques.

Features

Retro TV Screen Mechanic: Players observe the game world through a TV screen and control the character using a joystick.

Diverse Gameplay Mechanics: Enemy avoidance, ball and obstacle interactions, and a health system.

Post-Processing Effects: Visual effects implemented using Unity’s Post Processing Stack.

Ads and Reward System: Integrated with Google Mobile Ads; players can earn rewards after watching ads.

Alttester Package: Edit and Play Mode tests have been conducted to ensure proper functionality.

Chat System: Integrated with Gemini LLM to provide dynamic in-game conversation experiences.

Modular Structure: Various gameplay mechanics and UI elements can be added or modified modularly.

Packages and Tools Used

Unity 2021+ – Game engine and scene management.

Post Processing Stack v2 – Visual effects and color grading.

Google Mobile Ads SDK – Ads and reward system.

Alttester – Edit and Play Mode testing.

Gemini LLM – AI-driven in-game chat system.

Cinemachine & DOTween – Camera controls and animation effects.

Installation

Clone the repository (the latest developments are on the Test branch):

git clone -b Test https://github.com/polatcangurbuz/Gamizo.git
cd Gamizo


Open the project in Unity Hub.

Install the required packages via the Unity Package Manager:

Post Processing

Google Mobile Ads

DOTween

Alttester

Open the scene: Assets/Scenes/GameScene.unity.

Test the game in Play Mode or create a build to run it.

Gameplay Instructions

Joystick Controls: Move the character using the joystick.

On-Screen Objects: Avoid enemies and obstacles.

Ad Viewing: Watch a limited number of ads per day to earn rewards.

Health System: Each hit reduces health; the game ends when health reaches zero.


Development and Testing

Edit Mode Tests: Conducted with Alttester for scene and script validation.

Play Mode Tests: Real-time gameplay behaviors have been tested.

Post-Processing Effects: Bloom, Color Grading, Vignette, and other effects applied.

Chat System: AI-driven chat experience powered by Gemini LLM.

Contributing

To contribute to the project:

Fork the repository and work on your own branch.

Add new features or fix bugs.

Submit a pull request; changes will be reviewed and merged into the main project.

Note: The latest developments are available on the Test branch.
