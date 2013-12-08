using UnityEngine;
using System.Collections;

public class UI_LabelControl : MonoBehaviour {

	public enum LabelType 
	{
		CurrentScore,
		HighScore,
		FoodNumber,
		TrapNumber,
		GameTime,
	}

	public LabelType type;

	private UILabel label;
	// Use this for initialization
	void Start () {
		label = GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		SetLabel();
	}

	void SetLabel()
	{
		int n = 0;

		switch(type)
		{
		case LabelType.CurrentScore:
			n = GameDirector.gameInstance.currentScore;
			break;
		case LabelType.HighScore:
			n = GameDirector.gameInstance.highScore;
			break;
		case LabelType.FoodNumber:
			n = MainCharLogic.Instance.foodNo;
//				GameDirector.gameInstance.foodNumber;
			break;
		case LabelType.TrapNumber:
			n = MainCharLogic.Instance.trapNo;
//				GameDirector.gameInstance.trapNumber;
			break;
		case LabelType.GameTime:
			n = GameDirector.gameInstance.survivalTime;
			break;
		}

		label.text = n.ToString();
	}
}
