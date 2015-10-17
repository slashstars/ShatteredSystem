using UnityEngine;
using System.Collections;

public class GenerateSpheres : MonoBehaviour
{
    public float cooldown_const;
    private float cooldown = 3;

    public float scaleMinPercent = 5;
    public float scaleMaxPercent = 40;

    public float speedMax = 2;
    public float speedMin = 1;

    public GameObject sphere;
    private float screenWidth;
    private float screenRangeMin;
    private float screenRangeMax;

    // Use this for initialization
    void Start()
    {
        var screenHeight = 2f * Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        screenRangeMax = screenWidth / 2;
        screenRangeMin = -screenWidth / 2;

        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            CreateSphere();
            cooldown = cooldown_const;
        }

    }

    void CreateSphere()
    {
        var newSphere = Instantiate(sphere);
        var randomX = Random.Range(screenRangeMin, screenRangeMax);

        newSphere.transform.position = new Vector3(randomX, transform.position.y, 0);

        var randomScale = Random.Range(screenWidth * scaleMinPercent / 100, screenWidth * scaleMaxPercent / 100);
        newSphere.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        var randomSpeed = Random.Range(speedMin, speedMax);
        newSphere.GetComponent<Rigidbody2D>().gravityScale = randomSpeed;

        GravityGlobal.AddGravityObject(newSphere);
    }
}
