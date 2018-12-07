using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LadderController : MonoBehaviour {
    private GameObject traget;
    private bool start = false;
    private float d = 1;

    public Transform pu;
    public Transform pd;
    public Transform pd2;

    void AwakeUP(Collider collider)
    {
        traget = collider.gameObject;
        d = 1;
        traget.GetComponent<Animation>().CrossFade("useladderstart");
        traget.GetComponent<PlayerProperty>().UseLadderState();
        traget.transform.rotation = transform.rotation;
        traget.transform.DOMove(pu.position, 0.8f).OnComplete(new TweenCallback(OnStart));
    }
    void AwakeDOWN(Collider collider)
    {
        traget = collider.gameObject;
        d = -1;
        traget.GetComponent<Animation>()["useladderend"].speed = -1f;
        traget.GetComponent<Animation>().CrossFade("useladderend");
        traget.GetComponent<PlayerProperty>().UseLadderState();
        traget.transform.rotation = transform.rotation;
        traget.transform.DOMove(pd2.position, 0.8f).OnComplete(new TweenCallback(OnStart));
    }
    void OnStart()
    {
        start = true;
        if(d>0)
            traget.GetComponent<Animation>()["useladderstart"].speed = 1f;
        else
            traget.GetComponent<Animation>()["useladderend"].speed = 1f;
        traget.GetComponent<Animation>().CrossFade("useladder");
    }
    void OnEnd()
    {
        start = false;
        traget.GetComponent<PlayerProperty>().NormalState();
    }
    void Update()
    {
        if(start==true)
        {
            traget.GetComponent<CharacterController>().Move(new Vector3(0, 0.05f*d, 0));
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(d>0)
        {
            traget.GetComponent<Animation>().CrossFade("useladderend");
            traget.transform.DOMove(pd.position, 0.8f).OnComplete(new TweenCallback(OnEnd));
        }
        else
        {
            traget.GetComponent<Animation>()["useladderstart"].speed = -1f;
            traget.GetComponent<Animation>().CrossFade("useladderstart");
            traget.transform.DOMove(pu.position, 0.8f).OnComplete(new TweenCallback(OnEnd));
        }
    }

}
