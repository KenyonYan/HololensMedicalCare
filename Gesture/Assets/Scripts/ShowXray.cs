using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class ShowXray : MonoBehaviour, IHoldHandler
{
    
    public GameObject xrayPic;              //x光图片
    public GameObject section;              //x光切片
    

    private ShowTextBoard textBoard;        //显示操作面板的脚本
    private bool isShow = true;             //是否显示x光图片部分

    void Start ()
    {
        textBoard = FindObjectOfType<ShowTextBoard>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            showxrayPic();
        }
    }


    public void OnHoldStarted(HoldEventData eventData)
    {
        print("11111111");
        textBoard.OnDoubleTap();          //长按显示与隐藏面板
        showxrayPic();
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        //TODO
        
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        //TODO
    }


    private void showxrayPic()              //显示x光图片部分
    {
        if(isShow)
        {
            xrayPic.SetActive(false);
            section.SetActive(false);
        }
        else
        {
            xrayPic.SetActive(true);
            section.SetActive(true);
        }
        isShow = !isShow;
    }
}
