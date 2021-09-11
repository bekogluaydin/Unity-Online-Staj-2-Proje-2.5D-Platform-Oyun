### Online-Staj-2-Proje-2.5D-Platform-Oyun
Online Staj 2 Proje(2.5D Platform Oyun)
Most of the assets are from the Asset Store and free assets. So the game may not be very good in terms of UI. My second major game with Unity. The first was the card guessing game. This project was developed for internship 2 and will continue to be developed.
## Main Menu
There are 3 buttons in the Main Menu. 
1. Start button, that is, the last level of the user continues from there.
2. Level selection button; With this button, previously played levels can be selected.
3. Exit button; Allows you to exit the game.

![MainMenu](https://user-images.githubusercontent.com/51875713/132951787-034aec8d-81a2-4f91-bb85-41aa29d1fc60.png)

# Main Menu Level Chosoe
You can choose and play previously played levels from this section. I used PlayerPrefs for this. Buttons become active or inactive according to PlayerPrefs.

![MainMenu_LevelSelection](https://user-images.githubusercontent.com/51875713/132951894-72279123-822c-4c99-aace-7535a6fb24dc.png)

## Sample Level
Sample designs for a level(Level 3). As I said, assets are free and i got them from asset store.

1. As for the character control buttons, I added them at the last moment so that they can be played on mobile.
2. Normally, I wrote the code according to the computer keyboard to test the game comfortably.
3. I added the default buttons so finally that I can test it on android after certain things are done.
4. Necessary sprites will be added to these buttons in the near future. I needed to throw the project on Github, so it happened like this.

![SampleLevel](https://user-images.githubusercontent.com/51875713/132951935-abe7ee7a-b93c-48d4-b2df-34efc06c939d.png)
![SampleLevel1](https://user-images.githubusercontent.com/51875713/132951939-58f7e5f9-40c5-48a6-b235-f1df8a207d7c.png)
![SampleLevel2](https://user-images.githubusercontent.com/51875713/132951941-88b91eef-82ec-41d8-aa94-34b6abdee229.png)
![SampleLevel3](https://user-images.githubusercontent.com/51875713/132951945-e66562ec-7ea0-4563-bea9-ac1fa41f5b8b.png)
![SampleLevel4](https://user-images.githubusercontent.com/51875713/132951946-ee8ed10a-49d4-4bc2-a296-92abf2703cd4.png)

# Sample Level Climb Ladder
The climb button is normally deactivated. It becomes active when it comes to the relevant ladder. When the climb button is active, I deactivate the attack and jump buttons to avoid problems such as bugs.

When the work is done with the ladder, the climb button is deactivated again and the jump and attack buttons are active again.
![SampleLevel_ClimbLadder1](https://user-images.githubusercontent.com/51875713/132952050-4639f5cf-4707-4ee2-ad99-2d95b9579c5d.png)
![SampleLevel_ClimbLadder2](https://user-images.githubusercontent.com/51875713/132952051-8dd81654-54ca-4d05-a4bb-9572ad070133.png)

# Sample Level Game Over Screen
When the character we manage dies (by the enemy or falls down), the endgame panel becomes active.

1. Main Menu; returns to the main menu.
2. RePlay; restarts the active episode (scene).
3. Exit; quit the game

![SampleLevel_gameOverPanel](https://user-images.githubusercontent.com/51875713/132951936-7f637f6a-86ad-43ed-8bdf-23f2d7c98b3f.png)

# Sample Level Next Level Pass
When the chapter is completed successfully, this panel opens while moving to the next chapter.

![SampleLevel_nextLevelPass](https://user-images.githubusercontent.com/51875713/132951938-33f2c8a5-e5a4-4fd5-a8ec-34ddbfef69a4.png)
