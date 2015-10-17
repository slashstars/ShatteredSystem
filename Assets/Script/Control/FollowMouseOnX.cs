using UnityEngine;
using System.Collections;

public class FollowMouseOnX : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        transform.position = new Vector3(curScreenPoint.x, transform.position.y, transform.position.z);
    }
}
