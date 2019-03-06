using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour {

    public string Name { get; set; }
    public abstract string ParentName();

    public virtual void OnEntering()
    {

    }

    public virtual void OnPausing()
    {

    }

    public virtual void OnResuming()
    {

    }

    public virtual void OnExitng()
    {

    }
}
