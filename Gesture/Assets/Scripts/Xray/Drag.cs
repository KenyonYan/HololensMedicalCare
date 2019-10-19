using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Drag : MonoBehaviour {

    public Texture[] Lugu;               //颅骨的x光图片
    public Texture[] Fuzhudongmai;       //腹主动脉x光图片
    public Texture[] Jingdongmai;        //颈动脉x光图片
    public Texture[] Jingzhui;           //颈椎x光图片

    public Slider slider;                //滑动条
    public GameObject xraySection;       //移动的x切片

    private float sliderValue;           //滑动条的数值
    private Vector3 startPosition;       //x切片开始位置
    private Transform nowModel;          //当前显示的模型
    private LimiteSlider limitSlider;    //限制滑动的脚本



    void Start ()
    {
        startPosition = xraySection.transform.localPosition;
        limitSlider = FindObjectOfType<LimiteSlider>();
    }
	
	
	void Update ()
    {
        //sliderValue = slider.value;
        sliderValue = limitSlider.GetSliderValue();
        slider.value = sliderValue;                     //同步slider的值的变化
        //print("滑动条的值：" + sliderValue);
        int index = (int)(sliderValue * 29);
        //print(index);
        xraySection.transform.localPosition = new Vector3(startPosition.x, startPosition.y+(float)index*0.01f , startPosition.z);
        //print(index);
        WhichXPicToShow(index);
    }

    private void WhichXPicToShow(int index)
    {
        //获取当前的显示的模型
        GameObject axisY = GameObject.Find("AxisY");
        foreach (Transform child in axisY.transform)
        {
            nowModel = child;
            if(nowModel.name != "section")
            {
                break;
            }
        }
        
        if (nowModel.name == "head(Clone)")
        {
            ShowLuguXPic(index);
        }
        else if (nowModel.name == "腹部动脉(Clone)")
        {
            ShowFuzhuXPic(index);
        }
        else if (nowModel.name == "动脉(Clone)")
        {
            ShowJingdongmaiXPic(index);
        }
        else if(nowModel.name == "颈椎(Clone)")
        {
            ShowJingzhuiXPic(index);
        }
    }


    public void ShowLuguXPic(int index)
    {
        this.GetComponent<RawImage>().texture = Lugu[index];
    }

    public void ShowFuzhuXPic(int index)
    {
        this.GetComponent<RawImage>().texture = Fuzhudongmai[index];
    }

    public void ShowJingdongmaiXPic(int index)
    {
        this.GetComponent<RawImage>().texture = Jingdongmai[index];
    }

    public void ShowJingzhuiXPic(int index)
    {
        this.GetComponent<RawImage>().texture = Jingzhui[index];
    }


}
