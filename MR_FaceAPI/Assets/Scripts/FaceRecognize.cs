
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Baidu.Aip.Face;
using System.Text;
using System;
using UnityEngine.UI;


public class FaceRecognize : MonoBehaviour {

    public string API_KEY = "rkPG0aSDzXt5Sn97OCA0zx77";
    public string SECRET_KEY = "ZQmfiza4SGQQaQYr0HvrgjAna9gOT05D";
    Face client;

    //截取摄像头实时画面
    public Camera cameras;

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


    // Use this for initialization
    void Start()
    {

        client = new Face(API_KEY, SECRET_KEY);
        //StartCoroutine(IEGetStringBase64());
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(CaptureScreen(), 1f);
    }




    //获取图片base64字符串，由于QPS限制，此处采用协程降低注册频率
    IEnumerator IEGetStringBase64()
    {
        //获取到每一张图片的路径
        string[] picsPathArr = Directory.GetFiles(Application.streamingAssetsPath + "/FaceDetect/");
        //循环获取每张图片的base64字符串
        for (int i = 0; i < picsPathArr.Length; i++)
        {
            //unity会自动生成.meta文件，过滤掉
            if (picsPathArr[i].Contains("meta")) continue;
            //读取
            FileInfo file = new FileInfo(picsPathArr[i]);
            var stream = file.OpenRead();
            byte[] buffer = new byte[file.Length];
            //读取图片字节流
            stream.Read(buffer, 0, Convert.ToInt32(file.Length));
            //base64字符串
            string imageBase64 = Convert.ToBase64String(buffer);
            //采用base64字符串方式上传
            string imageType = "BASE64";
            //用户组
            string groupId = "group1";
            //用户id，一般同一个人的图片放在同一个id下
            string userId = "liyanhong";

            //开始注册
            SignUpFace(imageBase64, imageType, groupId, userId);

            yield return new WaitForSeconds(0.6f);
        }

        FaceSearch();
    }




    //人脸注册
    public void SignUpFace(string image, string imageType, string groupId, string userId)
    {
        var options = new Dictionary<string, object>{
        {"user_info", "LiYanHong"},
        {"quality_control", "NORMAL"},
        {"liveness_control", "LOW"}
    };
        // 带参数调用人脸注册
        var result = client.UserAdd(image, imageType, groupId, userId, options);

    }

    public Text text;

    //人脸对比
    public void FaceSearch()
    {
        FileInfo file = new FileInfo(Application.streamingAssetsPath + "/FaceDetect/8.jpg");
        var stream = file.OpenRead();
        byte[] buffer = new byte[file.Length];
        //读取图片字节流
        stream.Read(buffer, 0, Convert.ToInt32(file.Length));
        var image = Convert.ToBase64String(buffer);

        var imageType = "BASE64";
        //之前注册的组
        var groupIdList = "group1";

        var result = client.Search(image, imageType, groupIdList);
        text.text = result.ToString();
        Debug.Log(result);
    }

    //截图
    public string CaptureScreen()
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
        return "0";
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
        var groupIdList = "MySelf";

        var result = client.Search(image, imageType, groupIdList);
        text.text = result.ToString();
        Debug.Log(result);
    }


    //点击按钮开始截屏检测对比
    public void BtnClick()
    {
        text.text = null;
        CaptureScreen();
    }


}
