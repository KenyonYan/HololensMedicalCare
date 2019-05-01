using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Baidu.Aip.Face;
using System.Text;
using System;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;


public class FaceDetect : MonoBehaviour
{
    public string API_KEY = "rkPG0aSDzXt5Sn97OCA0zx77";
    public string SECRET_KEY = "ZQmfiza4SGQQaQYr0HvrgjAna9gOT05D ";
    //public Text text;
    //截取摄像头实时画面
    public Camera cameras;


    private GestureRecognizer gestureRecognizer;           //手势识别脚本
    private GetInfoFromFace faceInfo;
    Face client;


    void Awake()
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
               delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                           System.Security.Cryptography.X509Certificates.X509Chain chain,
                           System.Net.Security.SslPolicyErrors sslPolicyErrors)
               {
                   return true; // **** Always accept
               };
    }

    void Start()
    {
        faceInfo = new GetInfoFromFace();
        client = new Face(API_KEY, SECRET_KEY);
        //  创建GestureRecognizer实例
        gestureRecognizer = new GestureRecognizer();
        //  注册指定的手势类型,本例指定双击手势类型
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        //  订阅手势事件
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        //  开始手势识别
        gestureRecognizer.StartCapturingGestures();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CaptureScreen();
        }
    }


    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 1)
        {
            //text.text = null;
            CaptureScreen();          //单击事件
        }
    }

    //截图
    public void CaptureScreen()
    {
        Texture2D screenShot;
        RenderTexture rt = new RenderTexture(1920, 1080, 1);
        cameras.targetTexture = rt;
        cameras.Render();
        RenderTexture.active = rt;
        screenShot = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0);
        screenShot.Apply();
        string fileName = Application.streamingAssetsPath + "/CameraFaceDetect/" + "CameraFace.jpg";
        //保存到本地
        byte[] jpgData = screenShot.EncodeToJPG();
        System.IO.File.WriteAllBytes(fileName, jpgData);
        //人脸对比
        CameraFaceSearch(fileName);
    }

    //实时摄像头人脸画面对比
    public void CameraFaceSearch(string fileName)
    {
        FileInfo file = new FileInfo(fileName);
        var stream = file.OpenRead();
        byte[] buffer = new byte[file.Length];
        //读取图片字节流
        stream.Read(buffer, 0, Convert.ToInt32(file.Length));
        var image = Convert.ToBase64String(buffer);

        var imageType = "BASE64";
        //之前注册的组
        var groupIdList = "Patient";

        var result = client.Search(image, imageType, groupIdList);
        //float score = faceInfo.ParseJson(result);
        //text.text = result.ToString();
        string res = result.ToString();
        Debug.Log(res);
    }


    //点击按钮开始截屏检测对比
    public void BtnClick()
    {
        //text.text = null;
        CaptureScreen();
    }

}