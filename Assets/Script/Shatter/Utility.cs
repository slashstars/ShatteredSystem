using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public static class Utility
    {
        /// <summary>
        /// Calculates the B angle in an ABC triangle by using the length of its sides.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>The angle in degrees.</returns>
        public static float GetAngleBInTriangleViaThreeSides(float a, float b, float c)
        {
            var aSquared = Mathf.Pow(a, 2);
            var bSquared = Mathf.Pow(b, 2);
            var cSquared = Mathf.Pow(c, 2);

            var divisor = 2 * a * c;
            var dividible = cSquared + aSquared - bSquared;

            return Mathf.Acos(dividible / divisor) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Calculates the coordinates for evenly spaced points along a circle.
        /// </summary>
        /// <param name="radius">The radius of the circe.</param>
        /// <param name="numberOfPoints">How many points we're going to add to the circle.</param>
        /// <returns>A list of the point coordinates (Vector3 with z==0)</returns>
        public static List<Vector3> DefineEvenlySpacedPointsAlongACircle(float radius, int numberOfPoints)
        {
            List<Vector3> coordinates = new List<Vector3>();

            for (var i = 1; i <= numberOfPoints; i++)
            {
                var theta = ((Mathf.PI * 2) / numberOfPoints);
                var angle = (theta * i);

                var ballX = (radius * Mathf.Cos(angle));
                var ballY = (radius * Mathf.Sin(angle));

                coordinates.Add(new Vector3(ballX, ballY, 0));
            }

            return coordinates;
        }

        public static bool CoinToss()
        {
            return UnityEngine.Random.Range(0, 1) == 0;
        }
    }
}
