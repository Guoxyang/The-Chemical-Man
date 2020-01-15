using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
	public float innerRadius = 0.0f;
	public float outerRadius = 15.0f; 

	private ArrayList ammoList;
	private Stack currentAmmo;
	private int currIndex;
	private string currentAmmoName;
	private GameObject ammoText;
	public GameObject flameGun;
	public GameObject gunPoint;
	private float shootSpeed;

	public GameObject clip;

	// Use this for initialization
	void Start () {
		ammoList = new ArrayList ();
		ammoText = GameObject.Find ("ammoText");
		flameGun.GetComponent<ParticleSystem> ().Stop ();
/*		Stack flameStack = new Stack();
		flameStack.Push ("flame");
		flameStack.Push (flameGun);
		currentAmmoName = "flame";
		ammoList.Add (flameStack);
		currentAmmo = flameStack;*/
		currIndex = 100;
		shootSpeed = 800f;
		clip = GameObject.Find ("clip");
	}
	
	// Update is called once per frame
	void Update () {
		showAmmo ();
		collectElement ();

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			clip.BroadcastMessage ("changeToNext");
		} else if(Input.GetAxis ("Mouse ScrollWheel") < 0){
			clip.BroadcastMessage ("changeToLast");
		}
		if (Input.GetMouseButton (1)) {

			if (Input.GetMouseButtonDown (0)) {
				shot ();
			}
		} else if (Input.GetMouseButtonUp (1)){
			flameGun.GetComponent<ParticleSystem> ().Stop ();
		}
	}
	private void showAmmo(){
		
		if (currentAmmo != null && ammoText != null) {
			
			ammoText.GetComponent<Text> ().text = currentAmmoName+ ": " + (currentAmmo.Count - 1);
		}

	}

	private void collectElement(){
			Collider[] objects = Physics.OverlapSphere (transform.position, outerRadius);
			foreach (Collider col in objects) {
				if (col.tag == "elementAvailable")
				{
					if (Vector3.Distance(transform.position, col.transform.position) > innerRadius)
					{
					if (col.gameObject.GetComponent<element> ()._name == "flame") {
						col.gameObject.GetComponent<element> ().setState (element.State.Attracting);
						collect (col.gameObject);
						continue;
					}
						col.gameObject.GetComponent<element> ().setState (element.State.Attracting);
						col.transform.DOMove(gameObject.transform.position,0.4f).OnComplete(()=>collect(col.gameObject));
					}
				}
			}
	}

	private void switchAmmo(bool isNext){
		
		if (isNext) {
			currIndex++;
		} else {
			currIndex--;
		}
		currentAmmo = ammoList [currIndex % ammoList.Count] as Stack;

		object[] objs = currentAmmo.ToArray ();
		currentAmmoName = objs [objs.Length-1].ToString();

	}

	private void shot(){
		if (currentAmmo == null) {
			return;
		}


		if (currentAmmo.Count > 1) {
			if ((currentAmmo.Peek () as GameObject).GetComponent<element> ()._name == "flame") {
				GameObject flame = currentAmmo.Peek () as GameObject;
				flame.SetActive (true);
				flame.GetComponent<ParticleSystem>().Play();
				return;
			}
			GameObject ammo = Instantiate (currentAmmo.Peek() as GameObject, gunPoint.transform.position, gunPoint.transform.rotation);
			ammo.SetActive (true);
			ammo.GetComponent<element> ().setState (element.State.Shooting);
			Destroy (currentAmmo.Peek() as GameObject);
			currentAmmo.Pop ();
			ammo.GetComponent<Rigidbody> ().AddForce (shootSpeed * ammo.transform.forward);
		}


	}

	public void collect(GameObject obj){
		bool exist = false;
		foreach (Stack s in ammoList) {
			//GameObject _obj = s.Peek () as GameObject;
			object[] objs = s.ToArray ();
			string str = objs [objs.Length-1].ToString();
			if (str == obj.GetComponent<element> ()._name) {
				exist = true;
				s.Push (obj);
				//Debug.Log (s.Count);
			}
		}
		if (!exist) {
			Stack ammoStack = new Stack();
			ammoStack.Push (obj.GetComponent<element> ()._name);
			ammoStack.Push (obj);
			ammoList.Add (ammoStack);
			//Debug.Log (ammoStack.Count);
			//currentAmmo = ammoStack;
			//currIndex++;
			//currentAmmoName = obj.GetComponent<element> ()._name;
		}
		obj.SetActive (false);
	}

}
