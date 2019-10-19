using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ButtonFunction : MonoBehaviour
{
    [Header("医学模型")]
    public GameObject[] Models;                         

    private GestureAction gesAction;                       //控制旋转和移动的手势脚本

    private int _indexOfModels;                            //当前模型的下标
    private GameObject _displayModel;                      //当前显示的模型
    private GameObject _axisY;                             //代表Y轴的物体


    void Start ()
    {
        gesAction = FindObjectOfType<GestureAction>();

        //将头作为第一个模型
        _displayModel = Instantiate(Models[0], Models[0].transform.position, Models[0].transform.rotation);
        _axisY = GameObject.Find("AxisY");
        _displayModel.transform.parent = _axisY.transform;
        _indexOfModels = 0;
    }


    void Update ()
    {

    }


    /// <summary>
    /// 移动模式
    /// </summary>
    public void MoveButton()
    {
        gesAction.IsNavigationEnabled = false;
        gesAction.GestureControl = GestureType.Move;
    }
    /// <summary>
    /// 旋转模式
    /// </summary>
    public void RotateButton()
    {
        gesAction.IsNavigationEnabled = true;
        gesAction.GestureControl = GestureType.Rotate;
    }

    /// <summary>
    /// 缩放模式
    /// </summary>
    public void ZoomButton()
    {
        gesAction.IsNavigationEnabled = true;
        gesAction.GestureControl = GestureType.Zoom;
    }


    /// <summary>
    /// UI形式的下一个模型
    /// </summary>
    public void NextModel()
    {
        _indexOfModels = (_indexOfModels + 1) % Models.Length;              //下标循环
        Transform trans = _displayModel.transform;

        Destroy(_displayModel);            //销毁当前显示的模型
        //在之前模型的位置再次生成新的模型，避免造成新模型回到初始位置
        _displayModel = Instantiate(Models[_indexOfModels], trans.position, Models[_indexOfModels].transform.rotation);
        _displayModel.transform.parent = _axisY.transform;
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
