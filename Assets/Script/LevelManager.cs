using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private static string levelIndexKey = "CurrentLevel";
    public float totalTime;

    [HideInInspector]
    public float currentTime;
    private int currentLevel;
    private bool terminalState = false;
    private bool isGameOver = false;
    private DisplayMessage messages;

    // Use this for initialization
    void Start()
    {
        currentTime = totalTime;
        currentLevel = Application.loadedLevel;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0 && !isGameOver)
            LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        var nextLevel = currentLevel + 1;
        if (nextLevel < Application.levelCount)
        {
            currentTime = totalTime;
            Application.LoadLevel(nextLevel);
        }
        else
        {
            GameWon();
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (level > 1)
            SaveLevel(level);

        if (level != 0)
        {
            messages = GameObject.Find(Names.ScriptRunner).GetComponent<DisplayMessage>();
            messages.DisplayNextLevelMessage(3, level);
        }
    }

    private void SaveLevel(int index)
    {
        PlayerPrefs.SetInt(levelIndexKey, index);
    }

    public static int GetSavedLevel()
    {
        return PlayerPrefs.GetInt(levelIndexKey);
    }

    public static void ResetSavedLevel()
    {
        PlayerPrefs.SetInt(levelIndexKey, 1);
    }

    public void GameOver()
    {
        StartCoroutine(GameOver(3));
    }

    IEnumerator GameOver(int time)
    {
        isGameOver = true;
        messages.DisplayGameOverMessage(time);
        Destroy(GameObject.Find(Names.Player));
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Names.StartMenu);
    }

    public void GameWon()
    {
        StartCoroutine(GameWon(3));
    }

    IEnumerator GameWon(int time)
    {
        messages.DisplayWinnerMessage(time);
        Destroy(GameObject.Find(Names.Player));
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Names.StartMenu);
    }
}
