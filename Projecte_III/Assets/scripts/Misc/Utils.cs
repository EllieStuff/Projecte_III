using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    [System.Serializable]
    public struct MinMaxFloat
    {
        public float min, max;
        public MinMaxFloat(float _min, float _max) { min = _min; max = _max; }

        public float GetRndValue() { return Random.Range(min, max); }
        public static float GetRndValue(MinMaxFloat _minMax) { return Random.Range(_minMax.min, _minMax.max); }
    }
    [System.Serializable]
    public struct MinMaxVec3
    {
        public Vector3 min, max;
        public MinMaxVec3(Vector3 _min, Vector3 _max) { min = _min; max = _max; }

        public Vector3 GetRndValue() { return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z)); }
        public static Vector3 GetRndValue(MinMaxVec3 _minMax) { return new Vector3(Random.Range(_minMax.min.x, _minMax.max.x), Random.Range(_minMax.min.y, _minMax.max.y), Random.Range(_minMax.min.z, _minMax.max.z)); }
    }

    public class Misc
    {
        public static int RndPositivity()
        {
            float value = Random.Range(-1.0f, 1.0f);
            if (value > 0) return 1;
            else return -1;
        }

        public static int FindFrictionIdByName(List<FrictionController.Friction> _list, string _nameToFind)
        {
            for(int i = 0; i < _list.Count; i++)
            {
                if (_list[i].name == _nameToFind)
                    return i;
            }

            return -1;
        }

    }

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