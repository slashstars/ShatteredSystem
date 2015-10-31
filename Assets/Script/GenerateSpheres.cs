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
        var newSphere = (GameObject)Instantiate(sphere, new Vector3(0, 0, -1000), new Quaternion());

        var randomScale = Random.Range(screenWidth * scaleMinPercent / 100, screenWidth * scaleMaxPercent / 100);
        newSphere.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        var randomSpeed = Random.Range(speedMin, speedMax);
        newSphere.GetComponent<Rigidbody2D>().gravityScale = randomSpeed;
        //newSphere.GetComponent<Rigidbody2D>().AddForce(Vector3.down * randomSpeed, ForceMode2D.Impulse);

        var radius = newSphere.GetComponent<Shatter>().Radius;

        var nonOverlappingPosition = GetNonOverlappingRandomPosition(screenRangeMin, screenRangeMax, radius);

        if (nonOverlappingPosition != null)
        {
            newSphere.transform.position = (Vector3)nonOverlappingPosition;
            ShatterObjectsGlobal.Add(newSphere);
        }
        else
            Destroy(newSphere);
    }

    private Vector3? GetNonOverlappingRandomPosition(float screenRangeMin, float screenRangeMax, float radius)
    {
        int attempts = 10;
        Vector3? generationPoint = null;

        while (attempts > 1)
        {
            var randomX = Random.Range(screenRangeMin, screenRangeMax);
            var testPoint = new Vector3(randomX, transform.position.y, 0);
            Collider2D colls = Physics2D.OverlapCircle(testPoint, radius);

            if (colls == null)
            {
                generationPoint = testPoint;
            }

            attempts--;
        }

        return generationPoint;
    }
}
