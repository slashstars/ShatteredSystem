using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityGlobal : MonoBehaviour
{
    public float G = 6.674f;

    [HideInInspector]
    public static GameObject[] gravityObjects;

    void Awake()
    {
        if (gravityObjects == null)
            gravityObjects = new GameObject[0];

        foreach (var gravityObject in GameObject.FindGameObjectsWithTag(Tags.Graviton))
            AddGravityObject(gravityObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < gravityObjects.Length; i++)
        {
            ApplyGravity(gravityObjects[i].transform.position);
        }
    }

    public static void AddGravityObject(GameObject gravityObject)
    {
        var objectsAsList = new List<GameObject>();
        objectsAsList.AddRange(gravityObjects);
        objectsAsList.Add(gravityObject);
        gravityObjects = objectsAsList.ToArray();
    }

    public static void RemoveGravityObject(GameObject gravityObject)
    {
        var objectsAsList = new List<GameObject>();
        objectsAsList.AddRange(gravityObjects);
        objectsAsList.Remove(gravityObject);
        gravityObjects = objectsAsList.ToArray();
    }

    private void ApplyGravity(Vector3 currentGravitonPosition)
    {
        for (int i = 0; i < gravityObjects.Length; i++)
        {
            var gravityPullDirection = currentGravitonPosition - gravityObjects[i].transform.position;
            gravityObjects[i].GetComponent<Rigidbody2D>().AddForce(gravityPullDirection * G);
        }
    }
}
