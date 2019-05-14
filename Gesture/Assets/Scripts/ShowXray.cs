using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;


public class ShowXray : MonoBehaviour {

    private GestureRecognizer gestureRecognizer;           //手势识别脚本


    void Start ()
    {
        //  创建GestureRecognizer实例
        gestureRecognizer = new GestureRecognizer();
        //  注册指定的手势类型,本例指定双击手势类型
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Hold);
        //  订阅手势事件
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        //  开始手势识别
        gestureRecognizer.StartCapturingGestures();
    }
	
	void Update () {
		
	}

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 4)
        {
            OnHoldTap();          //长按事件
        }
    }

    private void OnHoldTap()
    {
        print("11111111111");
    }

}
