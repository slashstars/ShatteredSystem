using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "SpaceGun")
        {
            Application.LoadLevel("StartMenu");
            print("Game over man");
        }

    }
}
