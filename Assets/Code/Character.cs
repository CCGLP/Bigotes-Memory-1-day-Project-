using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	GameObject thinkings; 
	private int id; 
	private BigoteController controller; 
	// Use this for initialization
	void Awake () {
		thinkings = this.transform.GetChild (0).gameObject;
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<BigoteController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HideThinkings(){
		thinkings.SetActive (false); 
	}

	public void ShowThinkings(){
		thinkings.SetActive (true); 

	}
	public void SetId(int id){
		this.id = id; 
	}

	public void GoToShow(){
		this.transform.position = Vector3.zero;
		this.transform.localScale *= 1.2f; 
	}

	void OnTriggerEnter2D(Collider2D coll){
		Bigote bigote = coll.GetComponent<Bigote> ();
		if (bigote != null) {
			if (controller.GetTimeSinceLastTrigger () > 0.8f) {
				if (bigote.GetId () == id) {
					print ("Good"); 
					controller.CorrectMoustache ();
					controller.ChangeShowPerson ();
				} else {
					print ("Fail"); 
					controller.FailMoustache ();
					controller.ChangeShowPerson ();
				}
			}
		}
	}



}
