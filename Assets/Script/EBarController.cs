using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBarController : MonoBehaviour
{
    public Image outsidebar;
    public Image withinbar;
    public GameObject target;

    private EnemyProperty ep;
    
	void Awake () {
        ep = target.GetComponent<EnemyProperty>();
	}
	
	void Update () {
        outsidebar.fillAmount = ep.Heath * 1.0f / ep.MaxHeath;
        if (withinbar.fillAmount > outsidebar.fillAmount)
        {
            withinbar.fillAmount -= 0.01f;
        }
        else if (withinbar.fillAmount < outsidebar.fillAmount)
        {
            withinbar.fillAmount = outsidebar.fillAmount;
        }
    }
}
