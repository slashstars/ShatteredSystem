using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Launch the rocket towards a particular target.
    /// </summary>
    /// <param name="target">Target in world coordinates.</param>
    /// <param name="force">Magnitude of force that will be applied continiously.</param>
    public void Launch(Vector3 target, float force)
    {
        LookAt(target);
    }

    private void LookAt(Vector3 lookAt)
    {
        var angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;
        transform.Rotate(-Vector3.right, angle);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Graviton")
        {
            coll.gameObject.GetComponent<Shatter>().ShatterFromCenter(1);
            Destroy(gameObject);
        }

    }
}
