using UnityEngine;
using System.Collections;

public class ShatterObstacles : MonoBehaviour
{
    public float shatterMin = 0.25f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == Tags.Graviton && GetObjectRadius(obj) >= shatterMin)
            obj.GetComponent<Shatter>().ShatterFromCenter(1);
    }

    private float GetObjectRadius(GameObject obj)
    {
        return obj.GetComponent<CircleCollider2D>().radius * obj.transform.localScale.x;
    }
}
