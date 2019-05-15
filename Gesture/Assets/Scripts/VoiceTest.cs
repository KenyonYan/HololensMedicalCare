using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTest : MonoBehaviour {

    public GameObject gameObject;
    bool isRotate = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isRotate)
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    public void TestSound()
    {
        isRotate = !isRotate;
    }
}
