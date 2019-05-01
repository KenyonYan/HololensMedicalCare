using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFloat : MonoBehaviour {

    private float radian = 0;               //弧度
    public float perRadian = 0.02f;        //每次变化的弧度
    public float radius = 0.3f;            //半径

    private Vector3 oldPos;                 //开始时的坐标

    private TextLookAtCam lookCamera;


	void Start ()
    {
        oldPos = transform.position;
        lookCamera = FindObjectOfType<TextLookAtCam>();
	}
	
	
	void Update ()
    {
        //if(lookCamera.HaveArrive())             //只有跟随到达后才会上下浮动
        //{
            radian += perRadian;                //弧度每次加0.03
            float dy = Mathf.Cos(radian) * radius;
            transform.position = oldPos + new Vector3(0, dy, 0);
        //}
	}
}
