using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallCamera : MonoBehaviour
{

    [HideInInspector]
    public WebCamTexture webTex;
    [HideInInspector]
    public string deviceName;
    //显示摄像头画面
    public RawImage rawImage;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(IECallCamera());
    }

    IEnumerator IECallCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            //设置摄像机摄像的区域    
            webTex = new WebCamTexture(deviceName, 1920, 1080, 60);
            webTex.Play();//开始摄像    
            rawImage.texture = webTex;

        }
    }

}