using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHightLight : MonoBehaviour {

    public GameObject HololensMoveText;     //移动按钮
    public GameObject HololensRotateText;   //旋转按钮
    public GameObject HololensNextModel;    //切换按钮
    public Material SelectColour;           //选中时切换颜色

    private GameObject focusedObject;      //注视的物体
    private Material moveTextColour;       //移动按钮原来的材质球
    private Material rotateTextColour;     //旋转按钮原来的材质球
    private Material nextTextColour;       //切换按钮原来的材质球
    

    public GameObject nowFocusedObject()
    {
        return focusedObject;
    }

    public GameObject GetMoveButton()           //获取移动按钮
    {
        return HololensMoveText;
    }

    public GameObject GetRotateButton()         //获取旋转按钮
    {
        return HololensRotateText;
    }

    public GameObject GetNextModelButton()      //获取切换模型按钮
    {
        return HololensNextModel;
    }

    void Start ()
    {
        focusedObject = null;

        moveTextColour = HololensMoveText.GetComponent<MeshRenderer>().material;
        rotateTextColour = HololensRotateText.GetComponent<MeshRenderer>().material;
        nextTextColour = HololensNextModel.GetComponent<MeshRenderer>().material;
    }
	
	void Update ()
    {
        focusedObject = null;
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            focusedObject = hitInfo.collider.gameObject;
            //移动按钮变色
            if (focusedObject.transform.name == "HololensMoveText")
            {
                HololensMoveText.GetComponent<MeshRenderer>().material = SelectColour;
            }
            //旋转按钮变色
            if (focusedObject.transform.name == "HololensRotateText")
            {
                HololensRotateText.GetComponent<MeshRenderer>().material = SelectColour;
            }
            //切换按钮变色
            if (focusedObject.transform.name == "HololensNextModel")
            {
                HololensNextModel.GetComponent<MeshRenderer>().material = SelectColour;
            }
        }
        else
        {
            focusedObject = null;
            //未注视到相应按钮时取消高光显示
            HololensMoveText.GetComponent<MeshRenderer>().material = moveTextColour;
            HololensRotateText.GetComponent<MeshRenderer>().material = rotateTextColour;
            HololensNextModel.GetComponent<MeshRenderer>().material = nextTextColour;
        }
    }
}
