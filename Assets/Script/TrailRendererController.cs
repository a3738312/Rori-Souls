using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererController : MonoBehaviour {
    public GameObject tr;

    public void TrailStart()
    {
        tr.active = true;
    }
    public void TrailStop()
    {
        tr.active = false;
    }
}
