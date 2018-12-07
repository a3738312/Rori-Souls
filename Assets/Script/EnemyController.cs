using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    //定义怪物的几种状态
    public const int STATE_STAND = 0;
    public const int STATE_WALK = 1;
    public const int STATE_RUN = 2;
    public const int STATE_DAMAGE = 3;
    public const int STATE_ATTACK = 4;
    public const int STATE_DEATH = 5;

    public GameObject player;
    public GameObject ebc;
    public Text st;
    public GameObject HitPoint;
    public AudioClip footstep;
    public AudioClip death;
    public AudioClip fall;
    public AudioClip attack;
    public AudioClip damage;
    public AudioClip idle;
    public AudioClip miss;

    private EnemyProperty ep;
    private bool dsb = false;
    private int ds = 0;
    private int di = 0;
    private int ac = 0;
    private Vector3 rv;
    private AudioSource AS;

    //怪物当前状态  
    private int NowState;
    //触发怪物攻击的临界距离  
    public float AI_ATTACT_DISTANCE = 10;
    public float AI_ATTACK_DISTANCE = 2f;
    public float AI_ATTACK_TIME = 5;

    void Awake()
    {
        ep = GetComponent<EnemyProperty>();
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        IfDeath();
        if (Vector3.Distance(transform.position, player.transform.position) < AI_ATTACT_DISTANCE)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < AI_ATTACK_DISTANCE && 
                NowState != STATE_DEATH && NowState != STATE_DAMAGE)
            {
                if (ac> AI_ATTACK_TIME && !this.GetComponent<Animator>().GetBool("attack"))
                {
                    //敌人进入攻击状态  
                    NowState = STATE_ATTACK;
                    //重置动画状态
                    ReState();
                    //敌人开始攻击
                    this.GetComponent<Animator>().SetBool("attack", true);
                }
                transform.LookAt(player.transform);
                ac++;
            }
            else
            {
                if (NowState != STATE_DAMAGE && NowState != STATE_DEATH &&
                    NowState != STATE_ATTACK && !this.GetComponent<Animator>().GetBool("attack"))
                {
                    //敌人开始奔跑  
                    this.GetComponent<Animator>().SetBool("run", true);
                    //敌人进入奔跑状态  
                    NowState = STATE_RUN;
                    //使敌人面向角色  
                    transform.LookAt(player.transform);
                    //向玩家靠近  
                    GetComponent<CharacterController>().Move(transform.forward * Time.deltaTime * 3);
                }
            }
        }
        else
        {
            ReState();
        }
        GetComponent<CharacterController>().Move(Vector3.up * (-3) * Time.deltaTime * 3);
        idleAudio();
    }
    void LateUpdate () {
        if(dsb==true)
        {
            ds++;
            if (ds >= 60)
            {
                st.enabled = false;
                di = 0;
            }
        }
    }
    public void GetDamageE(int x)
    {
        if (NowState == STATE_DEATH)
            return;
        rv = transform.position - player.transform.position;
        rv.y = 0;
        rv = rv.normalized;
        transform.DOMove(transform.position+rv*0.3f, 0.5f);
        ep.Heath -= x;
        ShowDamage(x);
        IfDamage();
        if (ep.Heath > 0)
        {
            if (ebc.activeSelf == false)
                ebc.SetActive(true);
        }
        else
            ebc.SetActive(false);
    }
    private void ShowDamage(int x)
    {
        di += x;
        st.text = ""+di;
        dsb = true;
        st.enabled = true;
        ds = 0;
    }

    private void IfDamage()
    {
        if(NowState!=STATE_DAMAGE)
        {
            this.GetComponent<Animator>().SetBool("damage", true);
            NowState = STATE_DAMAGE;
        }
        else
            this.GetComponent<Animator>().SetBool("reset",true);
    }
    private void Rereset()
    {
        this.GetComponent<Animator>().SetBool("reset", false);
    }
    private void ReState()
    {
        NowState = STATE_STAND;
        this.GetComponent<Animator>().SetBool("damage", false);
        this.GetComponent<Animator>().SetBool("run", false);
        this.GetComponent<Animator>().SetBool("attack", false);
        this.GetComponent<Animator>().SetBool("walk", false);
        this.GetComponent<Animator>().SetBool("reset", false);
        ac = 0;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
    private void IfDeath()
    {
        if (this.GetComponent<EnemyProperty>().Heath <= 0 && NowState != STATE_DEATH)
        {
            ReState();
            this.GetComponent<Animator>().SetBool("death", true);
            NowState = STATE_DEATH;
        }
    }
    public void Attack()
    {
        ExplosionDamage(HitPoint.transform.position, 2f);
    }
    private void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, 1);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Player")
                hitColliders[i].gameObject.SendMessage("GetDamage", ep.ATK);
            i++;
        }
    }
    public void runAudio()
    {
        AS.clip = footstep;
        AS.Play();
    }
    public void attackAudio()
    {
        AS.clip = attack;
        AS.Play();
    }
    public void damageAudio()
    {
        AS.clip = damage;
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
    private void idleAudio()
    {
        if(NowState==0&&!AS.isPlaying)
        {
            AS.clip = idle;
            AS.loop = true;
            AS.Play();
        }
        else
        {
            AS.loop = false;
        }
    }
    private void BossDeath()
    {
        SceneManager.LoadScene(2);
    }
}
