using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAtCam : MonoBehaviour {

    private Transform nowModel;         //当前显示的模型
    private Vector3 offset;             //操作面板与模型之间位置的偏移值
    //private bool isArrive = false;      //是否到达指定位置

    //public bool HaveArrive()
    //{
    //    return isArrive;
    //}


    void Start()
    {
        //获取当前的显示的模型
        nowModel = GameObject.Find("AxisY").GetComponentInChildren<Transform>();
        //计算偏移值
        offset = this.transform.position - nowModel.transform.position;
    }

    void Update ()
    {
        //通过插值移动到目标位置，最后一个参数是速度
        this.transform.position = Vector3.Lerp(this.transform.position, nowModel.transform.position + offset, Time.deltaTime * 5.0f);
        //if(transform.position == nowModel.transform.position + offset)
        //{
        //    isArrive = true;
        //}
        //else
        //{
        //    isArrive = false;
        //}
        //面向摄像机
        this.transform.LookAt(Camera.main.transform);
    }
    
}
