using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZH.Tensegrity;
using System;

public class TensegrityDriver : MonoBehaviour
{
    [SerializeField] TensegrityObject tObj;

    [SerializeField] StringLength[] stringLength;
    [SerializeField] bool Build;

    // Start is called before the first frame update
    void Start()
    {
        if (Build)
        {
            tObj.SetupStructure();
            tObj.ApplyPhysics();
        }
        stringLength = new StringLength[tObj.elements.Strings.Count];

        for (int i = 0; i < stringLength.Length; i++)
        {
            stringLength[i].length = 3f;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < stringLength.Length; i++)
        {
            var str = tObj.elements.Strings[i];
            // float dl = str.defalutLimitLength;

            str.SetLengthByParameter(stringLength[i].length);
        }
    }

    
}
[Serializable]
public struct StringLength
{
    [Range(0, 3f)] public float length;
}
