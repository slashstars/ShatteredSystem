using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public float boosterForce = -1;
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        if (boosterForce <= 0)
            throw new UnityException("Rocket was initialized without a boosterForce. Are you missing a Rocket.Launch call?");

        body = GetComponent<Rigidbody2D>();
        GravityGlobal.AddGravityObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        body.AddRelativeForce(Vector3.right * boosterForce);
    }

    /// <summary>
    /// Launch the rocket towards a particular target.
    /// </summary>
    /// <param name="target">Target in world coordinates.</param>
    /// <param name="force">Magnitude of force that will be applied continiously.</param>
    public void Launch(Vector3 target, float force)
    {
        LookAt(target);
        boosterForce = force;
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
            coll.gameObject.GetComponent<Shatter>().ShatterFromCenter(body.velocity.magnitude);
            GravityGlobal.RemoveGravityObject(gameObject);
            Destroy(gameObject);
        }

    }
}
