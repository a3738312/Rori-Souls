using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTriggerDown : MonoBehaviour {

    private Collider player;
    private bool isUse = false;
    private bool isIn = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag=="Player"&&collider.gameObject.GetComponent<PlayerProperty>().GetState() != 5)
        {
            collider.gameObject.GetComponent<PlayerController>().ChangeTip(4);
            collider.gameObject.GetComponent<PlayerProperty>().tip.enabled = true;
            player = collider;
            isIn = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerProperty>().tip.enabled = false;
            isIn = false;
        }
    }
    void Update()
    {
        bool c = Input.GetKeyDown(KeyCode.F);
        if (isIn == true && c)
        {
            transform.parent.gameObject.GetComponent<LadderController>().SendMessage("AwakeDOWN", player);
            player.gameObject.GetComponent<PlayerProperty>().tip.enabled = false;
        }
    }
}
