using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class Bigote : MonoBehaviour {


	private int id; 
	private SpriteRenderer rend; 


	// Use this for initialization
	void Start () {
		
	}


	public void SetId(int id, Sprite sprite){
		rend = this.GetComponent<SpriteRenderer> ();
		rend.sprite = sprite; 
		this.id = id; 
	}

	public int GetId(){
		return id; 
	}



	// Update is called once per frame
	void Update () {
	
	}
}
