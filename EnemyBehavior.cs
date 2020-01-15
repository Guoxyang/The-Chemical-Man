using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBehavior : MonoBehaviour {
	private float attackCD;
	private float lastAttack;
	public GameObject element;
	// Use this for initialization
	void Start () {
		attackCD = 1.5f;
		lastAttack = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Attack ();
	}

	void Attack(){
		if (Time.time > lastAttack + attackCD) {
			Collider[] objects = Physics.OverlapSphere (transform.position, 2f);
			foreach (Collider col in objects){
				if (col.tag == "Player") {
					transform.DOMove (col.transform.position, 0.3f).OnComplete(()=>transform.DOLocalMove(Vector3.zero,0.5f));
				}
			}
			lastAttack = Time.time;
		}

	}

	void OnParticleCollision(GameObject obj){
		if (obj.name == "Flames") {
			gameObject.GetComponent<DynamicChangeShader> ()._ChangeMaterial = true;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
			Invoke("destoryGameObject", 1.0f);
		}

	}
	void destoryGameObject(){
		Destroy (gameObject);
		GameObject elem = Instantiate (element, gameObject.transform.position, gameObject.transform.rotation);
		elem.GetComponent<element> ().setState (global::element.State.Available);
	}
}
