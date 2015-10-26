using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ShatterObjectsGlobal : MonoBehaviour
{
    [HideInInspector]
    public static Objects[] gravityObjects;

    void Awake()
    {
        if (gravityObjects == null)
            gravityObjects = new Objects[0];

        foreach (var gravityObject in GameObject.FindGameObjectsWithTag(Tags.Graviton))
            Add(gravityObject);
    }

    public static void Add(GameObject sphere)
    {
        var objectsAsList = new List<Objects>();
        objectsAsList.AddRange(gravityObjects);

        var body = sphere.GetComponent<Rigidbody2D>();
        var gravityObject = new Objects(sphere.transform, body.mass, body);
        objectsAsList.Add(gravityObject);
        gravityObjects = objectsAsList.ToArray();
    }

    public static void Remove(GameObject sphere)
    {
        var objectsAsList = new List<Objects>();
        objectsAsList.AddRange(gravityObjects);

        var index = GetIndex(sphere);
        if (index >= 0)
            objectsAsList.RemoveAt(index);

        gravityObjects = objectsAsList.ToArray();
    }

    private static int GetIndex(GameObject sphere)
    {
        var body = sphere.GetComponent<Rigidbody2D>();
        return Array.FindIndex(gravityObjects, o => o.body == body);
    }
}
