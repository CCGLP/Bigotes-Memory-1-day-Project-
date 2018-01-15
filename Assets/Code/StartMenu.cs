using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnClickStart(){
		SceneManager.LoadScene (1); 
	}
}
