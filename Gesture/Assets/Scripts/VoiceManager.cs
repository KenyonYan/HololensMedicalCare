using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour {

    private ButtonFunction changeModel;            //改变模型的脚本



    // Use this for initialization
    void Start ()
    {
        changeModel = FindObjectOfType<ButtonFunction>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void NextModel()
    {
        changeModel.ChangeModel();              //切换模型
    }
}
