using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowModel : MonoBehaviour {

    private GameObject axixY;         
    private Transform showModel;        //当前显示的模型
    private Vector3 offset;

    private bool isFirst = false;


    // Use this for initialization
    void Start ()
    {
        showModel = GameObject.Find("AxisY").GetComponentInChildren<Transform>();
        offset = this.transform.position - showModel.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, showModel.transform.position+offset, Time.deltaTime*1.0f);
    }
}
