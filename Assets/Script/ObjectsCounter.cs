using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectsCounter : MonoBehaviour
{
    private Text counterText;

    // Use this for initialization
    void Start()
    {
        counterText = GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        counterText.text = "Objects:" + GravityGlobal.gravityObjects.Length;
    }
}
