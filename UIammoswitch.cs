using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIammoswitch : MonoBehaviour {
    private const float Angle = 90f;
    float totalangle = 0;
	GameObject gun;
	private bool changeweaponforward, changeweaponbackward;
    // Use this for initialization
    void Start () {
        totalangle = this.transform.localEulerAngles.z;
		gun = GameObject.FindGameObjectWithTag ("Gun");
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (changeweaponforward)
        {
            changeweaponbackward = false;
        }
        if (changeweaponbackward)
        {
            changeweaponforward = false;
        }
        //transform.RotateAround(new Vector3(this.transform.position.x, this.transform.position.y, 0), new Vector3(0, 0, 1), Time.deltaTime * rotateangle*50);

        if (changeweaponforward)
        { 
			changeweaponforward = false;
			transform.DORotate (new Vector3 (0, 0, totalangle + 90.0f), 0.5f).OnComplete (getLast);
        }
        if (changeweaponbackward)
         {
			changeweaponbackward = false;
			transform.DORotate (new Vector3 (0, 0, totalangle - 90.0f), 0.5f).OnComplete (getNext);
         }

    }
	void getLast(){
		totalangle = this.transform.localEulerAngles.z;
		gun.BroadcastMessage ("switchAmmo", true);
	}

	void getNext(){
		totalangle = this.transform.localEulerAngles.z;
		gun.BroadcastMessage ("switchAmmo", false);
	}

	public void changeToNext(){
		changeweaponforward = true;
	}

	public void changeToLast(){
		changeweaponbackward = true;
	}
}
