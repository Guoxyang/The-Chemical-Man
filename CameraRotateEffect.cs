using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateEffect : MonoBehaviour {


    public float _speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.RotateAround(Vector3.zero, Vector3.forward, _speed * Time.deltaTime);
    }
}
