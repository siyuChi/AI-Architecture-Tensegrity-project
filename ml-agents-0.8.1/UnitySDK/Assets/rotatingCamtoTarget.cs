using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingCamtoTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private float param;

    private float speed =1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        param += Time.deltaTime * speed;
        var x = Mathf.Cos(param);
        var z = Mathf.Sin(param);
        var y = 150f;
        transform.position = new Vector3(x * 280f + target.transform.position.x, y+target.transform.position.y, z * 280f+target.transform.position.z);
        transform.LookAt(target.transform.position);
    }
}
