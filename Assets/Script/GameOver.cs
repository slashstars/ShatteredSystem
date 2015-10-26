using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    private LevelManager manager;

    void Start()
    {
        manager = GameObject.Find(Names.Game).GetComponentInChildren<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == Names.Player)
        {
            manager.GameOver();
        }
    }

}
