using UnityEngine;
using System.Collections;

public class MovableBigote : MonoBehaviour {

	private bool mouseDown;
	private Vector3 initialPosition; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (mouseDown) {
			Vector3 aux = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			aux = new Vector3 (aux.x, aux.y, 0); 
			this.transform.position = aux;
		}
	}

	void OnMouseDown(){
		mouseDown = true; 
		initialPosition = this.transform.position; 
	}

	void OnMouseUp(){
		mouseDown = false; 
		this.transform.position = initialPosition;
	}

	public void ResetMouse(){
		mouseDown = false; 
		this.transform.position = initialPosition;
	}
}
