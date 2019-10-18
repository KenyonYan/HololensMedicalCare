using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ButtonFunction : MonoBehaviour {

    public GameObject[] Models;
    public GameObject HololensMoveText;     //移动按钮
    public GameObject HololensRotateText;   //旋转按钮
    public GameObject HololensNextModel;    //切换按钮
    

    private GestureAction gesAction;                       //控制旋转和移动的手势脚本
    private GestureRecognizer gestureRecognizer;           //手势识别脚本

    private GameObject focusedObject;                      //注视的物体
    private ButtonHightLight buttonHightLight;             //按钮高光脚本
    private int _indexOfModels;                            //当前模型的下标
    private GameObject _displayModel;                      //当前显示的模型
    private GameObject _axisY;                             //代表Y轴的物体


    void Start ()
    {
        gesAction = FindObjectOfType<GestureAction>();

        //  创建GestureRecognizer实例  
        gestureRecognizer = new GestureRecognizer();
        //  注册指定的手势类型,本例指定单击手势类型  
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        //  订阅手势事件  
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        //  开始手势识别  
        gestureRecognizer.StartCapturingGestures();

        //将头作为第一个模型
        _displayModel = Instantiate(Models[0], Models[0].transform.position, Models[0].transform.rotation);
        _axisY = GameObject.Find("AxisY");
        _displayModel.transform.parent = _axisY.transform;
        _indexOfModels = 0;
    }

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 1)
        {
            OnTap();          //单击按钮事件
        }
    }


    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            OnTap();
        }

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            focusedObject = hitInfo.collider.gameObject;
        }
    }

    private void OnTap()
    {

        if (focusedObject.transform.name == "HololensMoveText")
        {
            MoveButton();
        }
        else if(focusedObject.transform.name == "HololensRotateText")
        {
            RotateButton();
        }
        else if(focusedObject.transform.name == "HololensNextModel")
        {
            ChangeModel();
        }
    }

    /// <summary>
    /// 移动模式
    /// </summary>
    public void MoveButton()
    {
        gesAction.IsNavigationEnabled = false;
        ChangeR2MText();
    }
    /// <summary>
    /// 旋转模式
    /// </summary>
    public void RotateButton()
    {
        gesAction.IsNavigationEnabled = true;
        ChangeM2RText();
    }
    /// <summary>
    /// 从移动模式切换为旋转模式
    /// </summary>
    public void ChangeM2RText()
    {
        HololensMoveText.SetActive(true);
        HololensRotateText.SetActive(false);
    }
    /// <summary>
    /// 从旋转模式切换为移动模式
    /// </summary>
    public void ChangeR2MText()
    {
        HololensMoveText.SetActive(false);
        HololensRotateText.SetActive(true);
    }

    /// <summary>
    /// 3D立体文字的更改模型
    /// </summary>
    public void ChangeModel()
    {
        _indexOfModels = (_indexOfModels + 1) % Models.Length;              //下标循环
        Transform trans = _displayModel.transform;
        
        Destroy(_displayModel);            //销毁当前显示的模型
        //在之前模型的位置再次生成新的模型，避免造成新模型回到初始位置
        _displayModel = Instantiate(Models[_indexOfModels], trans.position, Models[_indexOfModels].transform.rotation);
        _displayModel.transform.parent = _axisY.transform;
    }

    /// <summary>
    /// UI形式的下一个模型
    /// </summary>
    public void NextModel()
    {
        ChangeModel();
    }

    /// <summary>
    /// UI形式的上一个模型
    /// </summary>
    public void PreModel()
    {
        _indexOfModels -= 1;
        if (_indexOfModels < 0)
        {
            _indexOfModels = Models.Length-1;
        }
        Transform trans = _displayModel.transform;

        Destroy(_displayModel);            //销毁当前显示的模型
        //在之前模型的位置再次生成新的模型，避免造成新模型回到初始位置
        _displayModel = Instantiate(Models[_indexOfModels], trans.position, Models[_indexOfModels].transform.rotation);
        _displayModel.transform.parent = _axisY.transform;
    }

}
