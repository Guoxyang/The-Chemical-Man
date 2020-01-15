using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class shotCamera : MonoBehaviour {
	public GameObject center;
	private GameObject _cam;
	public GameObject cameraView;
	private bool isAiming;
	// Use this for initialization
	void Start () {
		isAiming = false;
		_cam = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo animInfo = gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
		isAiming = animInfo.IsName ("Aiming")||animInfo.IsName ("Fire");
		if (isAiming) {
			Vector3 aimmingPos = cameraView.transform.right - cameraView.transform.right.normalized * 1.4f;
			cameraView.transform.DOMove (transform.position - transform.right.normalized * 0.4f, 0.1f);
		} else {
			if (center != null) {
				center.SetActive (false);
			}
		}
		
		setCamera ();
	}

	void setCamera(){
		if (isAiming) {
			if (center != null) {
				center.SetActive (true);
			}
			gameObject.GetComponent<MouseView> ().enabled = true;
			_cam.GetComponent<vThirdPersonCamera> ().lockCamera = true;
			gameObject.GetComponent<Invector.CharacterController.vThirdPersonController> ().enabled = false;
			_cam.GetComponent<Camera> ().fieldOfView = 30;
		} else {
			cameraView.transform.DOMove(transform.position,0.1f);
			_cam.GetComponent<vThirdPersonCamera> ().lockCamera = false;
			_cam.GetComponent<Camera> ().fieldOfView = 60;
			gameObject.GetComponent<Invector.CharacterController.vThirdPersonController> ().enabled = true;
			gameObject.transform.eulerAngles = new Vector3(0,gameObject.transform.eulerAngles.y,0);
			gameObject.GetComponent<MouseView> ().enabled = false;
		}
	}
}
