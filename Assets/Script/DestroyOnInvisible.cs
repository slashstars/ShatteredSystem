using UnityEngine;
using System.Collections;

public class DestroyOnInvisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        ShatterObjectsGlobal.Remove(gameObject);
        Destroy(gameObject);
    }
}
