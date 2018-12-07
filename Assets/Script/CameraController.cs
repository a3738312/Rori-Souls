using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //摄像机朝向的目标模型
    public Transform target;
    //摄像机与模型保持的距离
    public float distance = 10.0f;

    public float rotatespeed = 5.0f;
    public float speed = 5;
    private Vector3 a;
    private float angle;
    void Start()
    {
        transform.LookAt(target.transform.position+new Vector3(0,0.5f,0));
    }

    void Update()
    {

        a = new Vector3(0,-1,0);
        angle = Vector3.Angle(transform.forward, a); //求出两向量之间的夹角
        if (!target)
            return;
        if (Input.GetKey(KeyCode.J))
            transform.parent.transform.Rotate(new Vector3(0, -1, 0), rotatespeed);
        if (Input.GetKey(KeyCode.L))
            transform.parent.transform.Rotate(new Vector3(0, 1, 0), rotatespeed);
        if (Input.GetKey(KeyCode.I) && angle < 110)
            transform.RotateAround(target.position, transform.right, -rotatespeed);
        if (Input.GetKey(KeyCode.K) && angle > 30)
            transform.RotateAround(target.position, transform.right, rotatespeed);

    }

    void LateUpdate()
    {
        if (!target)
            return;
        Ray ray = new Ray(target.position,transform.position-target.position);//从摄像机发出到点击坐标的射线
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
            GameObject gameObj = hitInfo.collider.gameObject;
            if (gameObj.tag == "Sence")//当射线碰撞目标为boot类型的物品 ，执行拾取操作
            {
                transform.position = hitInfo.point - (transform.position - target.position).normalized * 0.1f;
            }
        }
        else
        {
            transform.position = target.position + (transform.position - target.position).normalized * 5f;
        }
    }
}
