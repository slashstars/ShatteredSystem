using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private int startLevel = 1;

    void Start()
    {
        startLevel = LevelManager.GetSavedLevel();

        if (startLevel == 1)
            GameObject.Find(Names.ContinueButton).GetComponent<Button>().interactable = false;
        else
            GameObject.Find(Names.ContinueLabel).GetComponent<Text>().text = "CONTINUE (L" + startLevel + ")";

    }

    public void NewGameButton()
    {
        LevelManager.ResetSavedLevel();
        Application.LoadLevel(1);
    }

    public void ContinueButton()
    {
        Application.LoadLevel(startLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
