using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {
	public GameObject winCanvas;
	public GameObject loseCanvas;
	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void loadNextLevel(){
		int currentLevel = SceneManager.GetActiveScene ().buildIndex;
		int totalLevel = SceneManager.sceneCountInBuildSettings;
		if (currentLevel < SceneManager.sceneCountInBuildSettings-1) {
			SceneManager.LoadScene (currentLevel + 1);
		} else {
			backToMenu ();
		}
	}

	public void backToMenu(){
		SceneManager.LoadScene (0);
	}

	public void reStart(){
		int currentLevel = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentLevel);
	}

	public void endGame(){
		Application.Quit ();
	}

	public void callWinCanvas(){
		winCanvas.SetActive (true);
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void callLoseCanvas(){
		loseCanvas.SetActive (true);
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
