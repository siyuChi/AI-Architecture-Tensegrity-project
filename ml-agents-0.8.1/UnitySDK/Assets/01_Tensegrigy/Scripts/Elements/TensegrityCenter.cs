using UnityEngine;
using UnityEditor;

namespace ZH.Tensegrity
{
    public class TensegrityCenter : MonoBehaviour
    {
        private Rigidbody crb;
        public Vector3 defaultPosition;
        public void UpdateCenter(TensegrityObject myObject)
        {
            
            var points = myObject.elements.EndPoints;

            Vector3 sum = Vector3.zero;

            foreach(var p in points)
            {
                sum += p.position;
            }
            transform.position = sum / points.Count;
			transform.localScale = myObject.param.CenterSize * Vector3.one;
        }
        public void ResetCenter()
        {
            transform.position = defaultPosition;
        }
        public void SetDefualt()
        {
             defaultPosition = transform.position ;
        }

        public Vector3 GetVelocity()
        {
            crb = gameObject.GetComponent<Rigidbody>();
            return  crb.velocity;
        }
    }
}

