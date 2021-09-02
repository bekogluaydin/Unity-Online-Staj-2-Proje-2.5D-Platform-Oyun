using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsUnlock : MonoBehaviour
{
    public Button[] levelButtons;
    public int indexNumber = 0;
    void Start()
    {
        foreach (Button levelButton in levelButtons)
            levelButton.interactable = false;

        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

        for (int i = 0; i < reachedLevel; i++)
        {
            levelButtons[i].interactable = true;
            if (levelButtons[i].interactable)
                indexNumber = i+1;
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(indexNumber);
    }
}
