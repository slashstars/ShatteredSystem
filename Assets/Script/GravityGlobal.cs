using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GravityGlobal : MonoBehaviour
{
    public float G = 6.674f;

    [HideInInspector]
    public static GravityObject[] gravityObjects;

    void Awake()
    {
        if (gravityObjects == null)
            gravityObjects = new GravityObject[0];

        foreach (var gravityObject in GameObject.FindGameObjectsWithTag(Tags.Graviton))
            AddGravityObject(gravityObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < gravityObjects.Length; i++)
        {
            ApplyGravity(gravityObjects[i].transform.position, gravityObjects[i].mass);
        }
    }

    public static void AddGravityObject(GameObject sphere)
    {
        var objectsAsList = new List<GravityObject>();
        objectsAsList.AddRange(gravityObjects);

        var body = sphere.GetComponent<Rigidbody2D>();
        var gravityObject = new GravityObject(sphere.transform, body.mass, body);
        objectsAsList.Add(gravityObject);
        gravityObjects = objectsAsList.ToArray();
    }

    public static void RemoveGravityObject(GameObject sphere)
    {
        var objectsAsList = new List<GravityObject>();
        objectsAsList.AddRange(gravityObjects);
        objectsAsList.RemoveAt(GetGravityObjectIndex(sphere));
        gravityObjects = objectsAsList.ToArray();
    }

    private static int GetGravityObjectIndex(GameObject sphere)
    {
        var body = sphere.GetComponent<Rigidbody2D>();
        return Array.FindIndex(gravityObjects, o => o.body == body);
    }

    private void ApplyGravity(Vector3 currentPosition, float mass)
    {
        for (int i = 0; i < gravityObjects.Length; i++)
        {
            var distance = Vector3.Distance(currentPosition, gravityObjects[i].transform.position);
            if (distance == 0)
                continue;

            var gravityPullDirection = currentPosition - gravityObjects[i].transform.position;

            //print("About to add force with mass " + mass + " to:" + gravityObjects[i].transform.name);
            gravityObjects[i].body.AddForce(gravityPullDirection.normalized * G * mass / Mathf.Pow(distance, 2));

        }
    }
}
