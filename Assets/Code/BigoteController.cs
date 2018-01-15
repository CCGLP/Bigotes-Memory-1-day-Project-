using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using DG.Tweening;

public class BigoteController : MonoBehaviour {
	
	public enum GameStates
	{
		think,
		put,
		reward

	}

	[SerializeField]
	private Button buttonLeft;

	[SerializeField]
	private Button buttonRight; 


	[SerializeField]
	private Text peopleText; 

	Tween tween; 
	[SerializeField]
	private int numberOfPeople = 3; 
	[SerializeField]
	private Text goodText;

	[SerializeField]
	private Text badText;

	private int actualIndex = 0;
	private float previousTriggerTime; 
	private Transform parentChar; 
	[SerializeField]
	private GameObject secondPhaseUI;
	[SerializeField]
	private GameObject firstPhaseUI; 
	[SerializeField]
	private Text timeText; 
	[SerializeField]
	private Text secondTimeText; 
	[SerializeField]
	private float timeMaxPut = 4; 
	[SerializeField]
	private float timeMaxThink = 3; 
	private float timer = 0; 
	[SerializeField]
	private List<Sprite> bigotes; 
	[SerializeField]
	private List<Sprite> chars; 

	[SerializeField]
	private GameObject characterPrefab; 
	private List<GameObject> characters;
	private GameStates state; 

	private Character actualChar; 
	private Bigote[] mostachosMostrador; 

	// Use this for initialization
	void Start () {
		
		secondPhaseUI.SetActive (false);
		switch (StaticVariables.levelIndex) {
		case 1: 
			numberOfPeople = 1;
			break;
		case 2: 
			numberOfPeople = 2;
			break;
		case 3: 
			numberOfPeople = 3;
			break;
		case 4: 
			numberOfPeople = 3;
			break;
		case 5: 
			numberOfPeople = 4;
			break;
		case 6: 
			numberOfPeople = 4;
			break;
		case 7: 
			numberOfPeople = 5;
			break;
		case 8: 
			numberOfPeople = 5;
			break;
		case 9: 
			numberOfPeople = 5;
			break;
		case 10: 
			numberOfPeople = 6;
			break;
		case 11: 
			numberOfPeople = 6;
			break;
		case 12: 
			numberOfPeople = 6;
			break;
		default:
			numberOfPeople = 6;
			break;

		}
		peopleText.text = numberOfPeople.ToString () + " People";
		timeMaxThink *= numberOfPeople;
		timeMaxPut *= numberOfPeople;
		state = GameStates.think; 
		goodText.gameObject.SetActive (false);
		badText.gameObject.SetActive (false);
		parentChar = GameObject.Find ("Characters").transform;
		mostachosMostrador = GameObject.Find ("Mostrador").GetComponentsInChildren<Bigote> (); 
		HideMostrador ();
		GenerateCharacters ();
		parentChar.position = -Vector3.right * 6;


		for (int i = 0; i < 6; i++) {
			StaticVariables.dayGanances [i] = 0;
		}
		if (parentChar.position.x <= (-6 * numberOfPeople))
			buttonRight.gameObject.SetActive (false); 
		
		if (parentChar.position.x >= -6)
			buttonLeft.gameObject.SetActive (false);
	}


	private void HideMostrador(){
		for (int i = 0;i< mostachosMostrador.Length; i++) {
			mostachosMostrador [i].gameObject.SetActive (false);
		}
	}


	private void ShowMostrador(){
		for (int i = 0;i< mostachosMostrador.Length; i++) {
			mostachosMostrador [i].gameObject.SetActive (true);
		}
	}

	private bool IsEqualToArray(int[] arr, int id){
		for (int i = 0; i < arr.Length; i++) {
			if (id == arr [i])
				return true; 
		}
		return false; 
	}

	private void GenerateBigotesMostrador(int id){
		int[] randIds = new int[3];

		int randAux;

		for (int i = 0; i < randIds.Length; i++) {
			do{
				randAux = Random.Range (0, bigotes.Count); 
				randIds [i] = Random.Range (0, randAux); 
			} while(IsEqualToArray(randIds, randAux));
		}


		randIds [2] = id;
		//Shuflle
		int aux; 
		int rand;
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < randIds.Length; j++) {
				aux = randIds [j];
				rand = Random.Range (0, randIds.Length); 
				randIds [j] = randIds [rand];
				randIds [rand] = aux; 
			}
		}

		//end shuffle

		for (int i = 0; i < mostachosMostrador.Length; i++) {
			mostachosMostrador [i].SetId (randIds [i], bigotes[randIds[i]]);
		}
	
	}

	private void GenerateCharacters(){
		characters = new List<GameObject> ();
		Vector3 aux = Vector3.right * 6f ;
		List<Sprite> spritesAux = chars; 
		int randS; 
		for (int i = 0; i < numberOfPeople; i++) {
			characters.Add(((GameObject) Instantiate (characterPrefab, aux, Quaternion.identity))); 
			randS =Random.Range (0, spritesAux.Count);
			characters [i].GetComponent<SpriteRenderer> ().sprite = spritesAux[randS];
			spritesAux.RemoveAt (randS); 

			characters [i].transform.SetParent (parentChar);
			aux += Vector3.right * 6; 
			Bigote auxBigote = characters [i].GetComponentInChildren<Bigote> (); 
			int rand = Random.Range (0, bigotes.Count); 
			auxBigote.SetId (rand, bigotes [rand]); 
		}
	}


	public void OnRightClick(){
		if (parentChar.position.x > (-6 * numberOfPeople)) {
			parentChar.position -= Vector3.right * 6; 
			buttonLeft.gameObject.SetActive (true);
		}

		if (parentChar.position.x <= (-6 * numberOfPeople))
			buttonRight.gameObject.SetActive (false); 


			
	}

	public void OnLeftClick(){
		if (parentChar.position.x < -6) {
			parentChar.position += Vector3.right * 6;
			buttonRight.gameObject.SetActive (true); 
		}
		if (parentChar.position.x >= -6)
			buttonLeft.gameObject.SetActive (false);
	}


	private void ChangeGameState(GameStates state){

		this.state = state;

		if (this.state == GameStates.put) {
			firstPhaseUI.SetActive (false);
			secondPhaseUI.SetActive (true);
			int rand = Random.Range (0, characters.Count);
			actualChar = characters [rand].GetComponent<Character>();

			for (int i = 0; i < characters.Count; i++) {
				if (i != rand) {
					characters [i].SetActive (false);
				}
			}
			characters.RemoveAt (rand);
			int goodId = actualChar.GetComponentInChildren<Bigote> ().GetId ();

			ShowMostrador ();
			GenerateBigotesMostrador(goodId);
			actualChar.HideThinkings ();
			actualChar.GoToShow ();
			actualChar.SetId (goodId);




		}
		//Stuff here

	}

	public void ChangeShowPerson(){
		actualIndex++; 
		Destroy (actualChar.gameObject); 
		previousTriggerTime = timer; 
		if (characters.Count > 0) {
			int rand = Random.Range (0, characters.Count); 
			actualChar = characters [rand].GetComponent<Character> ();
			actualChar.gameObject.SetActive (true);
			characters.RemoveAt (rand);
			int goodId = actualChar.GetComponentInChildren<Bigote> ().GetId ();

			ShowMostrador ();
			GenerateBigotesMostrador (goodId);
			actualChar.HideThinkings ();
			actualChar.GoToShow ();
			actualChar.SetId (goodId);
		} else {
			SceneManager.LoadScene (2); 
		}


	}

	public float GetTimeSinceLastTrigger(){
		return timer - previousTriggerTime; 
	}
	IEnumerator WaitForChange(){
		yield return new WaitForSeconds (3);
		ChangeShowPerson ();
	}

	public void TimingChange(){
		for (int i = 0; i < mostachosMostrador.Length; i++) {
			mostachosMostrador [i].GetComponent<MovableBigote> ().ResetMouse ();
		}
		timer -= 1;
		StartCoroutine (WaitForChange());
	}

	public void CorrectMoustache(){
		StaticVariables.money += 20; 
		StaticVariables.dayGanances[actualIndex] = 20; 
		goodText.gameObject.SetActive (true); 
		if (tween != null) {
			tween.Complete ();
		}
		goodText.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;

		tween = goodText.GetComponent<RectTransform> ().DOAnchorPosY (1000, 10);



	}

	public void FailMoustache(){
		StaticVariables.money -= 20; 
		StaticVariables.dayGanances[actualIndex] = -20; 
		badText.gameObject.SetActive (true); 
		if (tween != null) {
			tween.Complete ();
		}
		badText.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;

		tween = badText.GetComponent<RectTransform> ().DOAnchorPosY (1000, 10);
	}
	// Update is called once per frame
	void Update () {

		if (state == GameStates.think) {
			timer += Time.deltaTime; 
			timeText.text = Mathf.CeilToInt (timeMaxThink - timer).ToString ();
			if (timer > timeMaxThink) {
				timer = 0; 
				ChangeGameState (GameStates.put);
			}

		} else if (state == GameStates.put) {
			timer += Time.deltaTime; 
			secondTimeText.text = Mathf.CeilToInt (timeMaxPut - timer).ToString ();
			if (timer > timeMaxPut) {
				timer = 0; 
				for (int i = 0; i < characters.Count; i++) {
					FailMoustache ();
				}
				SceneManager.LoadScene (2);


			}
		}

	}
}
