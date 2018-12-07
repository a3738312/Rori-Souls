using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
    public Image outsidebar;
    public Image withinbar;
    public GameObject player;

    public Image outsidebarT;
    public Image withinbarT;

    public Image youdie;

    private PlayerProperty pp;

    void Awake () {
        pp = player.GetComponent<PlayerProperty>();
	}
	

	void Update () {
        outsidebar.fillAmount = pp.Heath * 1.0f / pp.MaxHeath;
        if(withinbar.fillAmount > outsidebar.fillAmount)
        {
            withinbar.fillAmount -= 0.01f;
        }
        else if (withinbar.fillAmount < outsidebar.fillAmount)
        {
            withinbar.fillAmount = outsidebar.fillAmount;
        }

        outsidebarT.fillAmount = pp.Endurance * 1.0f / pp.MaxEndurance;
        if (withinbarT.fillAmount > outsidebarT.fillAmount)
        {
            withinbarT.fillAmount -= 0.01f;
        }
        else if (withinbarT.fillAmount < outsidebarT.fillAmount)
        {
            withinbarT.fillAmount = outsidebarT.fillAmount;
        }

        if (pp.Heath <= 0)
        {
            youdie.enabled = true;
        }
        else
            youdie.enabled = false;
    }
}
