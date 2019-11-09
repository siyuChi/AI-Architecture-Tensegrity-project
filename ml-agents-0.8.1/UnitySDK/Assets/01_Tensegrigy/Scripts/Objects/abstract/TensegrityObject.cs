using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZH.Tensegrity.Support;
using ZH.Tensegrity.Behavior;

namespace ZH.Tensegrity
{
    //[RequireComponent(typeof(TensegrityBehavior))]
    [RequireComponent(typeof(TensegrityPlayer))]
    [RequireComponent(typeof(TensegrityBehavior))]
    public abstract class TensegrityObject : MonoBehaviour, ITensegrityStructureGenerator
    {
        [Header("Prefabs")]
        public PrefabContainer prefabs;
        [Header("Elements")]
        public ElementsContainer elements;
        [Header("Parameters")]
        public TensegrityParameters param;
        [Header("State")]
        public bool isBuilt = false;
        public bool physicsApplied = false;
        public bool isFreezed = false;


        public void SetupStructure()
        {
            elements.Initialize();
            SetupPoints();
            SetupBars();
            SetupStrings();
            isBuilt = true;
        }


        public abstract void SetupPoints();


        public abstract void SetupBars();


        public abstract void SetupStrings();

        public void SetStringLength(int index,float length)
        {
            elements.Strings[index].SetLength(length);
        }


        public void ApplyPhysics()
        {
            foreach (var b in elements.Bars)
            {
                b.ToPhysics(param);
            }
            foreach (var s in elements.Strings)
            {
                if (!s.name.Contains("CoString"))
                    s.ToPhysics(param);
            }
            foreach (var b in elements.Bars)
            {
                b.ToggleKinematic(false);
            }

            physicsApplied = true;
        }

        public void UpdateStructure()
        {
            foreach(var p in elements.EndPoints)
            {
                p.UpdatePosition();
            }
            if(elements.center!=null)
            elements.center.UpdateCenter(this);

            foreach(var s in elements.Strings)
            {
                
                s.UpdateElement();
            }
        }

        public void Clear()
        {
            elements.Clear();
            isBuilt = false;
            physicsApplied = false;
            isFreezed = false;
        }
        
        public void ResetStructure()
        {
            foreach (var s in elements.Strings)
            {
                s.ResetString();
            }
            foreach (var b in elements.Bars)
            {
                b.ResetBar();
            }
         if(elements.center!= null)
            elements.center.ResetCenter();
        }
        public void ToggleFreeze(bool freeze)
        {
            isFreezed = freeze;
            foreach (var b in elements.Bars)
            {
                b.ToggleKinematic(isFreezed);
            }
        }


        public void SetAsDefualt()
        {
            foreach (var b in elements.Bars)
            {
                b.SetDefualt();
            }
            foreach (var s in elements.Strings)
            {
                s.SetDefualt();
            }
            if(elements.center!=null)
            elements.center.SetDefualt();
        }
    }

}

