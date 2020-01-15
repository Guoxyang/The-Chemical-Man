using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class flameFade : MonoBehaviour {
	private float scale = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (scale <= 0.1f) {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<levelManager> ().callWinCanvas ();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.GetComponent<element>()&&col.gameObject.GetComponent<element> ()._name == "H2O" && col.gameObject.GetComponent<element> ().state == element.State.Shooting) {
			scale -= 0.3f;
			gameObject.transform.DOScale (new Vector3 (scale, scale, scale), 1.0f);
		}
	}
}
