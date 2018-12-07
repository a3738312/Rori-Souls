using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private PlayerProperty pp;
    private Animation anima;
    public GameObject HitPoint;
    private float tcd = 0;

    public void AttackAwake (PlayerProperty p1,Animation anima1) {
        pp = p1;
        anima = anima1;
	}
	
	public void AttackUpdate ()
    {
        if (pp.GetState() == 2)
            return;
        bool a = Input.GetKeyDown(KeyCode.H);
        if (a && pp.GetState() != 3 && pp.GetState() != 4 && pp.Endurance > 0)
        {
            pp.NoactionState();
            anima.Stop();
            if (pp.Endurance >= 15)
            {
                pp.Endurance -= 15;
                tcd = 0;
            }
            else
            {
                pp.Endurance = 0;
                tcd = 0;
            }
            anima.CrossFade("attack 01");
        }
        bool b = Input.GetKeyDown(KeyCode.Space);
        if (b && pp.GetState() != 3 && pp.GetState() != 4 && pp.Endurance > 0)
        {
            pp.InvincibleState();
            anima.Stop();
            if (pp.Endurance >= 10)
            {
                pp.Endurance -= 10;
                tcd = 0;
            }
            else
            {
                pp.Endurance = 0;
                tcd = 0;
            }
            anima.CrossFade("roll");
        }
        if (tcd > 50)
        {
            pp.Endurance += 0.4f;
            if(pp.Endurance>pp.MaxEndurance)
            {
                pp.Endurance = pp.MaxEndurance;
            }
        }
        tcd++;
    }
    public void AttackHit()
    {
        ExplosionDamage(HitPoint.transform.position, 0.6f);
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius,1);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].gameObject.tag=="Enemy")
                hitColliders[i].gameObject.SendMessage("GetDamageE", pp.ATK);
            i++;
        }
    }
}
