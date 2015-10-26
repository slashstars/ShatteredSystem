using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayMessage : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayGameOverMessage(int time)
    {
        Destroy(GameObject.Find(Names.Player));
        StartCoroutine(ShowMessage(time, Names.GameOver));
    }

    public void DisplayWinnerMessage(int time)
    {
        StartCoroutine(ShowMessage(time, Names.WinnerMessage));
    }

    public void DisplayNextLevelMessage(int time, int levelIndex)
    {
        var levelLabel = GameObject.Find(Names.NextLevel).GetComponent<Text>();
        levelLabel.text = "Level " + levelIndex;
        StartCoroutine(ShowMessage(time, Names.NextLevel));
    }

    IEnumerator ShowMessage(int time, string messageName)
    {
        var gameOverLabel = GameObject.Find(messageName).GetComponent<Text>();
        gameOverLabel.enabled = true;
        yield return new WaitForSeconds(time);
        gameOverLabel.enabled = false;
    }
}
