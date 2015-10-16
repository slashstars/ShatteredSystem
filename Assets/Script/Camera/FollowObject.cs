using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{

    public GameObject follow;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, transform.position.z);
    }
}
