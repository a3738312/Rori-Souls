using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private float g = 10.0f;
    private float speed = 5.0f;
    private float turnSmoothing = 15f;
    public float rollSpeed = 30.0f;
    public float speedDampTime = 0.01f;
    public AudioClip shoutingClip;

    private PlayerProperty pp;
    private Animation anim;
    private Rigidbody rig;
    private CharacterController character;
    private Vector3 moveV;
    private Vector3 moveR;
    private Vector3 moves;
    private bool roll = false;

    private Transform camerarotat;

    public void RollStart()
    {
        moveR = transform.forward * rollSpeed;
        roll = true;
    }
    public void RollEnd()
    {
        roll = false;
    }
   public  void MoveAwake(Animation anima, Rigidbody rigb, CharacterController characterc, PlayerProperty p1)
    {
        anim = anima;
        rig = rigb;
        character = characterc;
        pp = p1;
    }
   public void MoveUpdate()
    {
        if (pp.GetState() == 2)
            return;
        camerarotat = Camera.main.transform.parent.transform;
        if (pp.GetState()==1)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            MovementManagement(h, v);
        }
        if (pp.GetState() == 3 && roll == true)
        {
            Vector3 roll = transform.forward * rollSpeed;
            roll.y = 0;
            character.Move(roll * Time.deltaTime);
        }
        if(pp.GetState()!=5)
            character.Move(Vector3.down * g * Time.deltaTime);
    }
    void MovementManagement(float horizontal, float vertical)
    {
        if (character.isGrounded)
        {
            moveV = camerarotat.transform.forward * vertical * speed + camerarotat.transform.right.normalized * horizontal * speed;
            moveV = Vector3.ClampMagnitude(moveV, 5);
            moveV.y = 0;
            character.Move(moveV * Time.deltaTime);
            if (horizontal != 0f || vertical != 0f)
            {
                Rotating(horizontal, vertical);
                anim.CrossFade("run");
            }
            else
            {
                anim.CrossFade("idle");
            }
        }
        else
        {
            anim.CrossFade("down");
        }
            
    }
    void Rotating(float horizontal, float vertical)
    {
        Vector3 targetDirection = camerarotat.transform.forward * vertical + camerarotat.transform.right * horizontal;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rig.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        rig.MoveRotation(newRotation);
    }
}
