using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{
    public GameObject[] Models;

    private int _indexOfModels;              //当前模型的下标
    private GameObject _displayModel;        //当前显示的模型
    private GameObject _axisY;               //代表Y轴的物体

    void Start()
    {
        //将头作为第一个模型
        _displayModel = Instantiate(Models[0], Models[0].transform.position, Models[0].transform.rotation);
        _axisY = GameObject.Find("AxisY");
        _displayModel.transform.parent = _axisY.transform;
        _indexOfModels = 0;
    }

    public GameObject GetNowModel()
    {
        return _displayModel;
    }

    public void ChangeNewModel()        //更换新模型
    {
        _indexOfModels = (_indexOfModels + 1) % Models.Length;              //下标循环
        Transform trans = _displayModel.transform;              //获取模型位置组件，
        Destroy(_displayModel);            //销毁当前显示的模型
        //在之前模型的位置再次生成新的模型，避免造成新模型回到初始位置
        _displayModel = Instantiate(Models[_indexOfModels], trans.position, Models[_indexOfModels].transform.rotation);             
        _displayModel.transform.parent = _axisY.transform;
    }



}
