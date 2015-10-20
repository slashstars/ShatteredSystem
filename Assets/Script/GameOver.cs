using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == Names.Player)
        {
            StartCoroutine(GameOverActions());
        }
    }

    IEnumerator GameOverActions()
    {
        print("Game over man");
        var gameOverLabel = GameObject.FindGameObjectWithTag(Names.GameOver).GetComponent<Text>();
        gameOverLabel.enabled = true;
        Destroy(GameObject.Find(Names.Player));
        yield return new WaitForSeconds(8);
        gameOverLabel.enabled = false;
        Application.LoadLevel(Names.StartMenu);
    }
}
