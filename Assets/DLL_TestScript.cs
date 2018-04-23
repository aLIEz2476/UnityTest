using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestLibrary;

public class DLL_TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(TestLibrary.Class1.DLLTest(10));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
