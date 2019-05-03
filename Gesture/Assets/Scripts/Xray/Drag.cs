using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Drag : MonoBehaviour {

    public Texture[] Lugu;
    public Slider slider;
    public RawImage xraySection;

    private float sliderValue;
    private Vector3 startPosition;



	// Use this for initialization
	void Start ()
    {
        this.GetComponent<RawImage>().texture = Lugu[0];
        startPosition = xraySection.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        sliderValue = slider.value;
        int index = (int)(sliderValue * (Lugu.Length - 1));
        xraySection.transform.position = new Vector3(startPosition.x, startPosition.y+(float)index*0.01f , startPosition.z);
        //print(index);
        this.GetComponent<RawImage>().texture = Lugu[index];
    }
}
