using UnityEngine;
using System.Collections;

public class InitialPush : MonoBehaviour
{

    public float strength = 10;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * strength);
    }
}
