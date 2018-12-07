using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperty : MonoBehaviour
{
    public int MaxHeath;
    public int Heath;
    public float MaxEndurance;
    public float Endurance;
    public int ATK;
    public bool isBonfire = false;
    public Image tip;
    public Sprite[] sps;

    public const int STATE_LIFE = 1;
    public const int STATE_DEATH = 2;
    public const int STATE_INVINCIBLE = 3;
    public const int STATE_NOACTION = 4;
    public const int STATE_USELADDER = 5;

    public int CharacterStatus = STATE_LIFE;//1.life 2.death 3.Invincible 4.No action 5.use ladder
   
    public int GetState()
    {
        return CharacterStatus;
    }
    public void NormalState()
    {
        CharacterStatus = STATE_LIFE;
    }
    public void DeathState()
    {
        CharacterStatus = STATE_DEATH;
    }
    public void InvincibleState()
    {
        CharacterStatus = STATE_INVINCIBLE;
    }
    public void NoactionState()
    {
        CharacterStatus = STATE_NOACTION;
    }
    public void UseLadderState()
    {
        CharacterStatus = STATE_USELADDER;
    }

}
