using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGesture : MonoBehaviour {

    private Text gestureText;
    private GestureAction gesAction;
	
	void Start ()
    {
        gestureText = this.GetComponentInChildren<Text>();
        gesAction = FindObjectOfType<GestureAction>();
	}
	
	
	void Update ()
    {
		
	}

    public void ChangeGestureText()
    {
        if (gestureText.text == "移动")
        {
            gestureText.text = "旋转";
            //gesAction.IsNavigationEnabled = false;
            gesAction.GestureControl = GestureType.Move;
        }
        else
        {
            gestureText.text = "移动";
            //gesAction.IsNavigationEnabled = true;
            gesAction.GestureControl = GestureType.Rotate;
        }
    }
}
