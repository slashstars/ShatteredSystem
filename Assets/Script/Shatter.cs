using UnityEngine;
using System.Collections;
using Assets.Script;

public class Shatter : MonoBehaviour
{
    public float shatterRatio;
    public GameObject subPiece;
    private Transform piecesContainer;
    private Transform piecesGlobal;
    private readonly string PIECES_GLOBAL = "PiecesGlobal";

    // Use this for initialization
    void Start()
    {
        piecesGlobal = GameObject.Find(PIECES_GLOBAL).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        ShatterFromCenter(1);
    }

    /// <summary>
    /// Radius of the sphere object.
    /// </summary>
    private float Radius
    {
        get
        {
            return GetComponent<SphereCollider>().radius * transform.localScale.x;
        }
    }

    /// <summary>
    /// Calculated radius of subspheres afer current sphere is "shattered".
    /// </summary>
    private float NewPieceRadius
    {
        get
        {
            return Radius * shatterRatio;
        }
    }

    /// <summary>
    /// Replaces current sphere with subspheres.
    /// Subspheres are forced outward from current sphere's center.
    /// </summary>
    /// <param name="shatterStrength">Magnitude of force scattering the subsphere.</param>
    private void ShatterFromCenter(int shatterStrength)
    {
        CreateSubPieces();

        var currentVelocity = GetComponent<Rigidbody>().velocity;
        foreach (var piece in piecesContainer.GetComponentsInChildren<Rigidbody>())
        {
            piece.AddForce((piece.transform.position - transform.position) * shatterStrength + currentVelocity, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Creates the subspheres intended to replace current sphere.
    /// The number of subspheres is determined by the public shatterRatio variable.
    /// </summary>
    private void CreateSubPieces()
    {
        piecesContainer = new GameObject().transform;
        piecesContainer.parent = piecesGlobal;

        var numberOfCirlceLayers = Mathf.RoundToInt(Radius / NewPieceRadius / 2);
        addBallsInLayersRecursive(numberOfCirlceLayers, Radius - NewPieceRadius);
    }

    /// <summary>
    /// Adds subspheres evenly spaced in a circle around the current center.
    /// Recursive function.
    /// </summary>
    /// <param name="layerIndex">The current circle layer we're adding spheres to.</param>
    /// <param name="layerRadius">The radius of the current circle layer.</param>
    private void addBallsInLayersRecursive(int layerIndex, float layerRadius)
    {
        var numberOfBallsInLayer = HowManyBallsInLayer(layerRadius, NewPieceRadius);

        addBallsInCurrentLayer(layerRadius, numberOfBallsInLayer);

        if (layerIndex > 1)
            addBallsInLayersRecursive(--layerIndex, layerRadius - NewPieceRadius * 2);
    }

    /// <summary>
    /// Adds spheres evently spaced in a circle shape around the current position of object.
    /// </summary>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="numberOfBalls">Number of spheres to add.</param>
    private void addBallsInCurrentLayer(float radius, int numberOfBalls)
    {
        var offset = new Vector3(radius, 0, 0);
        var pointsInLayers = Utility.DefineEvenlySpacedPointsAlongACircle(radius, numberOfBalls);

        foreach (var point in pointsInLayers)
        {
            var newPiece = InstantiateScaledBall();
            newPiece.transform.position = transform.position + point;
            newPiece.transform.parent = piecesContainer;
        }
    }

    /// <summary>
    /// Creates a new instances of a sphere and scales it to the current shatter ratio.
    /// </summary>
    /// <returns>The newly created sphere.</returns>
    private GameObject InstantiateScaledBall()
    {
        var newPiece = Instantiate(subPiece);
        newPiece.transform.localScale = transform.localScale * shatterRatio;
        return newPiece;
    }

    /// <summary>
    /// Calculates the number of subspheres that would fit in a circle layer given the subsphere size.
    /// </summary>
    /// <param name="radiusOfLayer">The radius of the circle.</param>
    /// <param name="newBallRadius">The radius of the subspheres.</param>
    /// <returns></returns>
    private int HowManyBallsInLayer(float radiusOfLayer, float newBallRadius)
    {
        var distanceBetweenTwoBalls = newBallRadius * 2;
        var angleBetweenBalls = Utility.GetAngleBInTriangleViaThreeSides(radiusOfLayer, distanceBetweenTwoBalls, radiusOfLayer);
        return Mathf.RoundToInt(360 / angleBetweenBalls);
    }
}
