using UnityEngine;
using UnityEditor;


namespace ZH.Tensegrity
{
    public static class TensegrityUtil
    {
        public static float GoldenRatio { get { return (1f - Mathf.Sqrt(5f)) * 0.5f; } }


        public static Vector3[] GetGoldenRectPoints(float longSize, Matrix4x4 matrix)
        {
            float shortSize = longSize * GoldenRatio;
            Vector3 p0 = matrix.MultiplyPoint(new Vector3(-longSize, 0f, -shortSize));
            Vector3 p1 = matrix.MultiplyPoint(new Vector3(longSize, 0f, -shortSize));
            Vector3 p2 = matrix.MultiplyPoint(new Vector3(-longSize, 0f, shortSize));
            Vector3 p3 = matrix.MultiplyPoint(new Vector3(longSize, 0f, shortSize));

            return new Vector3[] { p0, p1, p2, p3 };
        }

        public static Vector3[] GetRectPoints(float longSize, float shortSize,Matrix4x4 matrix)
        {
            Vector3 p0 = matrix.MultiplyPoint(new Vector3(-longSize, 0f, -shortSize));
            Vector3 p1 = matrix.MultiplyPoint(new Vector3(longSize, 0f, -shortSize));
            Vector3 p2 = matrix.MultiplyPoint(new Vector3(-longSize, 0f, shortSize));
            Vector3 p3 = matrix.MultiplyPoint(new Vector3(longSize, 0f, shortSize));

            return new Vector3[] { p0, p1, p2, p3 };
        }

        public static Vector3[] GetTetraPoints(float radius)
        {
            float d0 = radius * 0.5f;
            float d1 = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(d0, 2));
            float h = Mathf.Sqrt(Mathf.Pow(2f * d1, 2) - Mathf.Pow(radius, 2));

            var p0 = new Vector3(0, 0, radius);
            var p1 = new Vector3(-d1, 0, -d0);
            var p2 = new Vector3(d1, 0, -d0);
            var p3 = new Vector3(0, h, 0);

            var center = (p0 + p1 + p2 + p3) / 4f;

            return new Vector3[] { p0-center, p1-center, p2-center, p3-center };

        }
        public static Vector3 CleanUp(Vector3 point)
        {
            
            if (Mathf.Abs(point.x)<0.0001f)
            {
                point.x = 0;
            }
            if (Mathf.Abs(point.y) < 0.0001f)
            {
                point.y = 0;
            }
            if (Mathf.Abs(point.z) < 0.0001f)
            {
                point.z = 0;
            }

            return point;
        }
    }
}
