using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class reaction : MonoBehaviour {

	public equation _equation;
	public ArrayList elems;
	public GameObject particle;
	private ArrayList reactionElement;
	// Use this for initialization
	void Start () {
		elems = new ArrayList ();
		reactionElement = new ArrayList ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void combine(){
		react ();
	}

	public bool react(){
		reactionElement.Clear ();
		bool reactable = false;
		equation myEquation = new equation (_equation.reactantNum, _equation.reactant, _equation.resultantNum, _equation.resultant);
		for (int i = 0; i < elems.Count; i++) {
			
			GameObject myElem = elems[i] as GameObject;

			//遍历方程式的反应物看当前物质是哪种反应物，并判断该反应物的数量是否足够，如果不够，则入栈
			for (int j = 0; j < myEquation.reactant.Length; j++) {
				if (myElem.GetComponent<element> ()._name == myEquation.reactant [j].GetComponent<element> ()._name && myEquation.reactantNum [j]!= 0) {
					reactionElement.Add (myElem);
					myEquation.reactantNum [j]--;
				}
			}
			int restNum = 0;
			for (int j = 0; j < myEquation.reactantNum.Length; j++) {
				restNum += myEquation.reactantNum [j];
			}
			if (restNum == 0) {

				GameObject center = new GameObject ();

				center.transform.position = ((reactionElement[0] as GameObject).transform.position + (reactionElement[1] as GameObject).transform.position)/2;

				for (int k = 0; k < reactionElement.Count-1; k++) {
					GameObject Object = reactionElement[k] as GameObject;
					Object.transform.parent = null;
					Object.transform.DOMove (center.transform.position, 1.0f);
				}
				GameObject deleteObject = reactionElement[reactionElement.Count-1] as GameObject;
				deleteObject.transform.parent = null;
				ArrayList reactElem = reactionElement.Clone () as ArrayList;
				reactionElement.Clear ();
				deleteObject.transform.DOMove (center.transform.position, 1.0f).OnComplete (()=>getNewElement(reactElem,myEquation,center));
				reactable = true;
				break;
			}
		}

		return reactable;
	}

	public void getNewElement(ArrayList reactionelem,equation myEquation,GameObject center){
		for (int i=0;i<reactionelem.Count;i++) {
			GameObject deleteObject = reactionelem[i] as GameObject;
			Destroy (deleteObject);

		}
		GameObject p = Instantiate (particle, center.transform.position,center.transform.rotation);

		reactionelem.Clear ();
		for (int j = 0; j < myEquation.resultantNum.Length; j++) {
			for (int k = 0; k < myEquation.resultantNum [j]; k++) {
				GameObject newElement = Instantiate (myEquation.resultant [j],center.transform.position,center.transform.rotation);
			}
		}
	}
}
	

public struct equation
{
	public int[] reactantNum;
	public GameObject[] reactant;
	public int[] resultantNum;
	public GameObject[] resultant;

	public equation(int[] reactantNum,GameObject[] reactant,int[] resultantNum,GameObject[] resultant){
		this.reactantNum = reactantNum.Clone() as int[];
		this.reactant = reactant as GameObject[];
		this.resultantNum = resultantNum.Clone() as int[];
		this.resultant = resultant as GameObject[];
	}

	public bool valid(){
		if (reactantNum.Length == reactant.Length && resultantNum.Length == resultant.Length) {
			return true;
		} else {
			return false;
		}
	}
}