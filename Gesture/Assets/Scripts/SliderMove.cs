using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class SliderMove : MonoBehaviour
{

    private LimiteSlider gesAction;            //控制旋转和移动的手势脚本
    private Vector3 startPos;                   //开始的Slider位置

    void Start ()
    {
        gesAction = FindObjectOfType<LimiteSlider>();
        gesAction.IsNavigationEnabled = false;
        startPos = this.transform.position;
    }
	
	void Update ()
    {
        //this.transform.position = new Vector3(startPos.x, this.transform.position.y, startPos.z);
	}
}
