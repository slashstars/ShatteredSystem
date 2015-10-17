using UnityEngine;
using System.Collections;

public class RecoverPosition : MonoBehaviour
{

    public float recoverRate = 1;
    private float startingPositionY;
    private Vector3 startingPosition;
    private bool staying = false;
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        startingPositionY = transform.position.y;
        startingPosition = transform.position;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.y > startingPositionY)
        //    transform.position = startingPosition;

        if (body.velocity.y < 0.1f && transform.position.y < startingPositionY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + recoverRate * Time.deltaTime, 0);
        }
    }

    void FixedUpdate()
    {

        staying = false;
    }



    void OnTriggerStay2D(Collider2D other)
    {
        staying = true;
    }
}
