using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
    public float time = 60;
    public static float levelTimer;
    private int currentLevel;

    // Use this for initialization
    void Start()
    {
        levelTimer = time;
        currentLevel = Application.loadedLevel;
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime;

        if (levelTimer <= 0)
            LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        var nextLevel = currentLevel + 1;
        if (nextLevel < Application.levelCount)
        {
            levelTimer = time;
            Application.LoadLevel(nextLevel);
        }
        else
        {
            Application.LoadLevel(0);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (level != 1)
            StartCoroutine(ShowNextLevelLabel(level));
    }

    IEnumerator ShowNextLevelLabel(int level)
    {
        var nextLevelLabel = GameObject.FindGameObjectWithTag(Names.NextLevel).GetComponent<Text>();
        nextLevelLabel.enabled = true;
        nextLevelLabel.text = "Level " + level;
        yield return new WaitForSeconds(3);
        nextLevelLabel.enabled = false;
    }
}
