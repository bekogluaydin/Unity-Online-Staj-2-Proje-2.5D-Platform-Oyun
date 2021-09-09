using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsUnlock : MonoBehaviour
{
    public Button[] levelButtons;
    public static int indexNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button levelButton in levelButtons)
            levelButton.interactable = false;

        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

        for (int i = 0; i < reachedLevel; i++)
        {
            levelButtons[i].interactable = true;
            if (levelButtons[i].interactable)
                indexNumber = i + 1;
        }
    }
   public void ContinueGame()
    {
        LevelManager.ContinueGame();
    }
}
