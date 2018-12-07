using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public AudioClip miss;
    public AudioClip death;
    public AudioClip damage;
    public AudioClip damage2;
    public AudioClip footstep;
    public AudioClip roll;
    public AudioClip fall;

    private PlayerMove pm;
    private PlayerAttack pa;
    private PlayerProperty pp;

    private Animation anim;
    private AudioSource AS;
    private Rigidbody rig;
    private CharacterController character;
    public void GetDamage(int x)
    {
        pp.Heath -= x;
        pp.NoactionState();
        anim.Stop();
        anim.CrossFade("hit");
    }
    void Awake()
    {
        anim = GetComponent<Animation>();
        rig = GetComponent<Rigidbody>();
        character = GetComponent<CharacterController>();
        pm = GetComponent<PlayerMove>();
        pa = GetComponent<PlayerAttack>();
        pp = GetComponent<PlayerProperty>();
        AS = GetComponent<AudioSource>();

        pa.AttackAwake(pp, anim);
        pm.MoveAwake(anim, rig, character, pp);
    }
	void FixedUpdate ()
    {
        pm.MoveUpdate();
    }
    void LateUpdate()
    {
        IfDeath();
        pa.AttackUpdate();
    }
    public void ChangeTip(int x)
    {
        pp.tip.sprite = pp.sps[x];
    }
    private void IfDeath()
    {
        if(pp.Heath<=0)
        {
            pp.DeathState();
            anim.CrossFade("death");
        }
    }
    public void delPlayer()
    {

        this.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void runAudio()
    {
        AS.clip = footstep;
        AS.Play();
    }
    public void damageAudio()
    {
        AS.clip = damage;
        AS.Play();
    }
    public void rollAudio()
    {
        AS.clip = roll;
        AS.Play();
    }
    public void fallAudio()
    {
        AS.clip = fall;
        AS.Play();
    }
    public void missAudio()
    {
        AS.clip = miss;
        AS.Play();
    }
    public void deathAudio()
    {
        AS.clip = death;
        AS.Play();
    }
}
