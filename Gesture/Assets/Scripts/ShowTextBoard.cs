using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class ShowTextBoard : MonoBehaviour
{
    public GameObject textBoard;                           //文字操作控制面板

    private GestureRecognizer gestureRecognizer;           //手势识别脚本
    private bool isShow = false;                           //是否显示了面板


    void Start ()
    {
        //  创建GestureRecognizer实例
        gestureRecognizer = new GestureRecognizer();
        //  注册指定的手势类型,本例指定双击手势类型
        gestureRecognizer.SetRecognizableGestures(GestureSettings.DoubleTap | GestureSettings.Tap);
        //  订阅手势事件
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        //  开始手势识别
        gestureRecognizer.StartCapturingGestures();
    }


    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 2)
        {
            OnDoubleTap();          //双击事件
        }
    }


    public void OnDoubleTap()
    {
        print("222222");
        if(!isShow)
        {
            textBoard.SetActive(true);              //显示面板
        }
        else
        {
            textBoard.SetActive(false);             //隐藏面板
        }
        isShow = !isShow;
    }
}
