using UnityEngine;
using System.Collections;

public class GravityObject
{
    public Transform transform;
    public float mass;
    public Rigidbody2D body;

    public GravityObject(Transform transform, float mass, Rigidbody2D body)
    {
        this.transform = transform;
        this.mass = mass;
        this.body = body;
    }
}
