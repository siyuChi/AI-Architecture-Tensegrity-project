using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZH.Tensegrity;

public class Filter : MonoBehaviour
{
    [SerializeField] private TensegrityObject obj;

    private List<TensileElement> String;
    [SerializeField] private List<TensileElement> StringA;
    [SerializeField] List<TensileElement> StringB;
    [SerializeField] List<TensileElement> StringC;
    [SerializeField] List<TensileElement> StringD;
    [SerializeField] List<TensileElement> StringE;
    [SerializeField] List<TensileElement> COString;
    [SerializeField] Material myMaterial;
    [SerializeField] Material myMaterial1;
    [SerializeField] Material myMaterial2;
    [SerializeField] Material myMaterial3;
    [SerializeField] Material defMaterial;

    [SerializeField] private bool a;
    [SerializeField] private bool b;
    [SerializeField] private bool c;
    [SerializeField] private bool d;
    [SerializeField] private bool e;
    [SerializeField] private bool co;

    // Start is called before the first frame update
    void Awake()
    {
        obj = this.gameObject.GetComponent<TensegrityObject>();
        String = obj.elements.Strings;
    }

    void Start()
    {
        StringFilter();
    }

    void Update()
    {
        ChangeColor();
    }
    void StringFilter()
    {
        foreach (var s in String)
        {
            if (s.CompareTag("StringA"))
            {
                StringA.Add(s);

            }
            if (s.CompareTag("StringB"))
            {
                StringB.Add(s);

            }
            if (s.CompareTag("StringC"))
            {
                StringC.Add(s);
            }
            if (s.CompareTag("StringD"))
            {
                StringD.Add(s);
            }

            if (s.CompareTag("StringE"))
            {
                StringE.Add(s);
            }

            if (s.CompareTag("COString"))
            {
                COString.Add(s);
            }
        }
    }
    void ChangeColor()
    {
        //if (a == true)
        //{
        //    foreach (var a in StringA)
        //    {
        //        a.GetComponentInChildren<MeshRenderer>().material = myMaterial;
        //    }
        //}
        //else
        //    foreach (var a in StringA)
        //    {
        //        a.GetComponentInChildren<MeshRenderer>().material = defMaterial;
        //    }
        //if (b == true)
        //{
        //    foreach (var b in StringB)
        //    {
        //        b.GetComponentInChildren<MeshRenderer>().material = myMaterial1;
        //    }
        //}
        //else foreach (var b in StringB)
        //    {
        //        b.GetComponentInChildren<MeshRenderer>().material = defMaterial;
        //    }
        //if (c == true)
        //{
        //    foreach (var c in StringC)
        //    {
        //        c.GetComponentInChildren<MeshRenderer>().material = myMaterial2;
        //    }
        //}

        var targetmaterial = myMaterial;
        if (a == true)
        {
            foreach (var a in StringA)
            {
                a.GetComponentInChildren<MeshRenderer>().material = targetmaterial;
            }
        }
        else
            foreach (var a in StringA)
            {
                a.GetComponentInChildren<MeshRenderer>().material = defMaterial;
            }
        if (b == true)
        {
            foreach (var b in StringB)
            {
                b.GetComponentInChildren<MeshRenderer>().material = targetmaterial;
            }
        }
        else foreach (var b in StringB)
            {
                b.GetComponentInChildren<MeshRenderer>().material = defMaterial;
            }
        if (c == true)
        {
            foreach (var c in StringC)
            {
                c.GetComponentInChildren<MeshRenderer>().material = targetmaterial;
            }
        }
        else foreach (var c in StringC)
            {
                c.GetComponentInChildren<MeshRenderer>().material = defMaterial;
            }
        if (d == true)
        {
            foreach (var d in StringD)
            {
                d.GetComponentInChildren<MeshRenderer>().material = targetmaterial;
            }
        }
        else foreach (var d in StringD)
        {
            d.GetComponentInChildren<MeshRenderer>().material = defMaterial;
        }

        if (e == true)
        {
            foreach (var e in StringE)
            {
                e.GetComponentInChildren<MeshRenderer>().material = targetmaterial;
            }
        }
        else foreach (var e in StringE)
        {
            e.GetComponentInChildren<MeshRenderer>().material = defMaterial;
        }

        if (co == true)
        {
            foreach (var co in COString)
            {
                co.GetComponentInChildren<MeshRenderer>().material = targetmaterial;
            }
        }
        else foreach (var co in COString)
        {
            co.GetComponentInChildren<MeshRenderer>().material = defMaterial;
        }
    }

}
