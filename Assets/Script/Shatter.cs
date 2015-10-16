using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script;

public class Shatter : MonoBehaviour
{
    public float shatterRatio;
    public float shatterForce;
    public GameObject subPiece;
    private Transform piecesContainer;
    private Transform piecesGlobal;
    private readonly string PIECES_GLOBAL = "PiecesGlobal";
    private Rigidbody2D body;
    private int totalNumberOfCreatedSubPieces = 0;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        piecesGlobal = GameObject.Find(PIECES_GLOBAL).transform;
    }

    void OnMouseDown()
    {
        ShatterFromCenter(shatterForce);
    }

    private float radius = -1;
    /// <summary>
    /// Radius of the sphere object.
    /// </summary>
    private float Radius
    {
        get
        {
            if (radius < 0)
                radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
            return radius;
        }
    }

    private float newPieceRadius = -1;
    /// <summary>
    /// Calculated radius of subspheres afer current sphere is "shattered".
    /// </summary>
    private float NewPieceRadius
    {
        get
        {
            if (newPieceRadius < 0)
                newPieceRadius = Radius * shatterRatio;
            return newPieceRadius;
        }
    }

    private float newPieceMass = -1;
    /// <summary>
    /// Calculate the mass of subspheres after current sphere is "shattered".
    /// </summary>
    private float NewPieceMass
    {
        get
        {
            if (newPieceMass < 0)
                newPieceMass = body.mass / NewPiecesCount;

            return newPieceMass;
        }
    }

    private int newPiecesCount = -1;
    private int NewPiecesCount
    {
        get
        {
            if (newPiecesCount < 0)
                newPiecesCount = CalculateNumberOfSubPieces();
            return newPiecesCount;
        }
    }

    /// <summary>
    /// Replaces current sphere with subspheres.
    /// Subspheres are forced outward from current sphere's center.
    /// </summary>
    /// <param name="shatterStrength">Magnitude of force scattering the subsphere.</param>
    private void ShatterFromCenter(float shatterStrength)
    {
        CreateSubPieces();

        var currentVelocity = body.velocity;
        foreach (var piece in piecesContainer.GetComponentsInChildren<Rigidbody2D>())
        {
            var currentSphereVelocity = new Vector3(currentVelocity.x, currentVelocity.y, 0);
            var calculatedSubpiecesVelocity = currentSphereVelocity / newPiecesCount;
            piece.AddForce(((piece.transform.position - transform.position) * shatterStrength + calculatedSubpiecesVelocity), ForceMode2D.Impulse);
        }

        GravityGlobal.RemoveGravityObject(gameObject);
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

        var startingLayerIndex = HowManyCircleLayersAroundCenter();
        addBallsInLayersRecursive(startingLayerIndex, Radius - NewPieceRadius);
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
        newPiece.GetComponent<Rigidbody2D>().mass = NewPieceMass;
        newPiece.transform.parent = piecesContainer;
        GravityGlobal.AddGravityObject(newPiece);
        totalNumberOfCreatedSubPieces++;
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

    /// <summary>
    /// Calculates how many layers of subpieces will fit within the shattered sphere.
    /// </summary>
    /// <returns>The number of layers.</returns>
    private int HowManyCircleLayersAroundCenter()
    {
        return Mathf.RoundToInt(Radius / NewPieceRadius / 2);
    }

    /// <summary>
    /// Calculate how many subpieces of the current shatter ratio "fit" in the current sphere.
    /// </summary>
    /// <returns></returns>
    private int CalculateNumberOfSubPieces()
    {
        int numberOfBalls = 0;
        var currentLayerRadius = Radius - NewPieceRadius;

        for (int i = 0; i < HowManyCircleLayersAroundCenter(); i++)
        {
            numberOfBalls += HowManyBallsInLayer(currentLayerRadius, NewPieceRadius);
            currentLayerRadius -= NewPieceRadius * 2;
        }

        return numberOfBalls;
    }
}
