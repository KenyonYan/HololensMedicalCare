using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTest : MonoBehaviour {

    private ChangeModel changeModel;


    // Use this for initialization
    void Start ()
    {
        changeModel = FindObjectOfType<ChangeModel>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void NextModel()
    {
        changeModel.ChangeNewModel();
    }
}
