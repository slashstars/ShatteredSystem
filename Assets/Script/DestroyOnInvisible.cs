using UnityEngine;
using System.Collections;

public class DestroyOnInvisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        GravityGlobal.RemoveGravityObject(gameObject);
        Destroy(gameObject);
    }
}
