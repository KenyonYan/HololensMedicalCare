using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Drag : MonoBehaviour {

    public Texture[] Lugu;               //颅骨的x光图片
    public Slider slider;                //滑动条
    public GameObject xraySection;       //移动的x切片

    private float sliderValue;           //滑动条的数值
    private Vector3 startPosition;       //x切片开始位置



	
	void Start ()
    {
        this.GetComponent<RawImage>().texture = Lugu[0];
        startPosition = xraySection.transform.position;
	}
	
	
	void Update ()
    {
        sliderValue = slider.value;
        int index = (int)(sliderValue * (Lugu.Length - 1));
        xraySection.transform.position = new Vector3(startPosition.x, startPosition.y+(float)index*0.01f , startPosition.z);
        //print(index);
        this.GetComponent<RawImage>().texture = Lugu[index];
    }
}
