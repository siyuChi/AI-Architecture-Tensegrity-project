using System;

using UnityEngine;

namespace RC3.Unity.SDFDemo
{
    public class Ellipsoid : ScalarField
    {
        [SerializeField] private Vector3 _radius;
        private Matrix4x4 _toLocal;

        public override void BeforeEvaluate()
        {
            _toLocal = transform.worldToLocalMatrix;
        }

        public override float Evaluate(Vector3 point)
        {
            point = _toLocal.MultiplyPoint3x4(point);
            //float k0 = length(p / r);
            //float k1 = length(p / (r * r));
            //return k0 * (k0 - 1.0) / k1;
            float d1 = Length(point.x / _radius.x, point.y / _radius.y, point.z / _radius.z);
            float d2 = Length(point.x / (_radius.x * _radius.x), point.y / (_radius.y * _radius.y),
                point.z / (_radius.z * _radius.z));
            return d1 * (d1 - 1.0f) / d2;
        }


        private static float Length(float x, float y,float z)
        {
            return Mathf.Sqrt(x * x + y * y+z*z);
        }
    }
}
