using System;

using UnityEngine;

namespace RC3.Unity.SDFDemo
{
    public class Twist : ScalarField
    {
        [SerializeField] private ScalarField _source;
        [SerializeField] private float _strength = 20.0f;
        private Matrix4x4 _toLocal;

        public override void BeforeEvaluate()
        {
            _toLocal = transform.worldToLocalMatrix;
            _source.BeforeEvaluate();
        }

        public override float Evaluate(Vector3 point)
        {
            point = _toLocal.MultiplyPoint3x4(point);
            var t = point.y * _strength; // angle of rotation
            float c = Mathf.Cos(t);
            float s = Mathf.Sin(t);
            var x = point.x;
            var z = point.z;
            // matrix vector multiply (m = [c, -s, s, c])
            x = c * x - s * x;
            z = s * z + c * z;
            return _source.Evaluate(new Vector3(x, z, point.y));
        }
    }
}
