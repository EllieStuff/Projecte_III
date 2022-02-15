using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public class Vectors : MonoBehaviour
    {
        public static Vector3 GetRelativePosition2D(Vector3 v1, Vector3 v2)
        {

            return new Vector3(v2.x - v1.x, v2.y - v1.y);
        }

        //public static bool AreParallel(Vector3 v1, Vector3 v2)
        //{
        //    float edge = 0.001f;
        //    float v1Div = Mathf.Abs(v1.y / v1.x);
        //    float v2Div = Mathf.Abs(v2.y / v2.x);

        //    return v1Div < v2Div + edge && v1Div > v2Div - edge;
        //}

        public static Vector3 GetRandomDir()
        {

            return new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)).normalized;
        }

        public static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180);

            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), Mathf.Tan(angleRad));
        }

        //public static float GetAngleFromVector(Vector3 dir)
        //{
        //    dir = dir.normalized;
        //    float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //    if (n < 0) n += 360;

        //    return n;
        //}

        //public static Vector3 GetVectorFromAngleAndVector(float angle, Vector3 dir)
        //{
        //    float angleRad = angle * (Mathf.PI / 180) + GetAngleFromVector(dir);

        //    return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), Mathf.Tan(angleRad));
        //}

        public static float GetAngleB_H(Vector3 origin, Vector3 b, Vector3 h)
        {

            return Vector3.Distance(b, origin) / Vector3.Distance(h, origin);
        }

        public static Vector3 GetUpDir(Vector3 origin)
        {
            Vector3 tmpPoint = new Vector3(origin.x, origin.y + 1);

            return (tmpPoint - origin).normalized;
        }

        public static Vector3 GetDownDir(Vector3 origin)
        {
            Vector3 tmpPoint = new Vector3(origin.x, origin.y - 1);

            return (tmpPoint - origin).normalized;
        }

        public static Vector3 GetLeftDir(Vector3 origin)
        {
            Vector3 tmpPoint = new Vector3(origin.x - 1, origin.y);

            return (tmpPoint - origin).normalized;
        }

        public static Vector3 GetRightDir(Vector3 origin)
        {
            Vector3 tmpPoint = new Vector3(origin.x + 1, origin.y);

            return (tmpPoint - origin).normalized;
        }
    }

}