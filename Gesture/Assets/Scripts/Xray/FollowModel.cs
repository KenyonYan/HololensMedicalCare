using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowModel : MonoBehaviour {

    private Transform nowModel;         //当前显示的模型
    private Vector3 offset;             //操作面板与模型之间位置的偏移值


    void Start()
    {
        //获取当前的显示的模型
        nowModel = GameObject.Find("AxisY").GetComponentInChildren<Transform>();
        //计算偏移值
        offset = this.transform.position - nowModel.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, nowModel.transform.position + offset, Time.deltaTime * 5.0f);
    }
}
