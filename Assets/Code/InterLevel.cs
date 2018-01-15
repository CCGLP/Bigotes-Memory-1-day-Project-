using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InterLevel : MonoBehaviour {
	[SerializeField]
	private Text text_0;
	[SerializeField]
	private Text text_1;
	[SerializeField]
	private Text text_2;
	[SerializeField]
	private Text text_3;
	[SerializeField]
	private Text text_4;
	[SerializeField]
	private Text text_5;

	[SerializeField]
	private Text text_total;

	[SerializeField]
	private Text textWarn; 


	[SerializeField]
	private Text roundText; 
	// Use this for initialization
	void Start () {
		text_0.text = StaticVariables.dayGanances [0].ToString ();
		text_1.text = StaticVariables.dayGanances [1].ToString();
		text_2.text = StaticVariables.dayGanances [2].ToString();
		text_3.text = StaticVariables.dayGanances [3].ToString ();
		text_4.text = StaticVariables.dayGanances [4].ToString ();
		text_5.text = StaticVariables.dayGanances [5].ToString ();

		text_0.gameObject.SetActive (StaticVariables.dayGanances [0] != 0);
		text_1.gameObject.SetActive (StaticVariables.dayGanances [1] != 0);
		text_2.gameObject.SetActive (StaticVariables.dayGanances [2] != 0);
		text_3.gameObject.SetActive (StaticVariables.dayGanances [3] != 0);
		text_4.gameObject.SetActive (StaticVariables.dayGanances [4] != 0);
		text_5.gameObject.SetActive (StaticVariables.dayGanances [5] != 0);
		int totalRound = 0; 

		for (int i = 0; i < StaticVariables.dayGanances.Length; i++) {
			totalRound += StaticVariables.dayGanances [i];	
		}


		roundText.text = "Round: " + totalRound.ToString ();
		StaticVariables.levelIndex++;
		text_total.text = "Total: " + StaticVariables.money.ToString();

		if (StaticVariables.money < 0) {
			text_total.color = Color.red;
			textWarn.gameObject.SetActive (true);
			StaticVariables.levelIndex = 1; 
			StaticVariables.money = 0;

		} else {
			textWarn.gameObject.SetActive (false);

		}
	}


	public void OnRestartClick(){
		SceneManager.LoadScene (1); 
	}
	// Update is called once per frame
	void Update () {
	
	}
}
