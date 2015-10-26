using UnityEngine;
using System.Collections;

public class Objects
{
    public Transform transform;
    public float mass;
    public Rigidbody2D body;

    public Objects(Transform transform, float mass, Rigidbody2D body)
    {
        this.transform = transform;
        this.mass = mass;
        this.body = body;
    }
}
