using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class BonfireController : MonoBehaviour
{
    public GameObject target;
    public GameObject EventPoint;
    public GameObject sp;
    public GameObject sb;

    private Image tips;
    private Animation anima;
    private PlayerProperty pp;
    private bool isUse = false;
    private bool isTrigger = false;

    public void BonfireAwake(GameObject player)
    {
        pp = player.GetComponent<PlayerProperty>();
        anima = player.GetComponent<Animation>();
    }
	public void CreateAppearTips() {
        pp.gameObject.GetComponent<PlayerController>().ChangeTip(1);
        tips.enabled = true;
    }
    public void ChangeAppearTips()
    {
        pp.gameObject.GetComponent<PlayerController>().ChangeTip(2);
    }
    public void DesAppearTips()
    {
        tips.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            BonfireAwake(other.gameObject);
            tips = other.GetComponent<PlayerProperty>().tip;
            CreateAppearTips();
            other.GetComponent<PlayerProperty>().isBonfire = true;
            isTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DesAppearTips();
            other.GetComponent<PlayerProperty>().isBonfire = false;
            isTrigger = false;
        }
    }
    void OnDown()
    {
        isUse = true;
        pp.Heath = pp.MaxHeath;
        ChangeAppearTips();
        anima.CrossFade("recessing");
    }
    void OnUp()
    {
        pp.NormalState();
        isUse = false;
        anima["recess"].speed = 1;
    }
    void Update () {
        if (target == null)
            return;
        bool c = Input.GetKeyDown(KeyCode.F);

        if (isUse == false && isTrigger == true)
        {
            if (c && pp.GetState() != 3 && pp.GetState() != 4 && pp.isBonfire == true)
            {
                pp.InvincibleState();
                anima.CrossFade("recess");
                pp.gameObject.transform.DOMove(pp.gameObject.transform.position,0.7f).OnComplete(OnDown);
            }
        }
        else
        {
            if (c && pp.isBonfire == true)
            {
                anima["recess"].speed = -1;
                anima.CrossFade("recess");
                pp.gameObject.transform.DOMove(pp.gameObject.transform.position, 0.8f).OnComplete(OnUp);
            }
        }
    }
}
