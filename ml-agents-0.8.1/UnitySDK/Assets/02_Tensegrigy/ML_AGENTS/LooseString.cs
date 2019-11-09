using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class LooseString : MonoBehaviour
{

    [SerializeField] TensegrityObject tObj;
    [SerializeField] float maxLength = 10f;


    // Start is called before the first frame update
    void Start()
    {
        if (tObj != null)
        {
            LooseStrings();
        }
    }


    void LooseStrings()
    {
        for (int i = 0; i < tObj.elements.Strings.Count; i++)
        {
            var str = tObj.elements.Strings[i];
            str.SetLengthByParameter(maxLength);
           // str.lostphysics();




            //str.SetLength(maxLengthParam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
