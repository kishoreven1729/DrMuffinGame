using UnityEngine;
using System.Collections;

public class UI_GameControl : MonoBehaviour {


	public enum BtnType
	{
		Food,
		Trap
	}

	public BtnType type;

	void OnClick()
	{
		switch(type)
		{
		case BtnType.Food:
			//print ("SetFood!");
			SoundManager.Instance.PlaySound(SoundManager.SoundType.SetCheese);
			MainCharLogic.Instance.DropFood();
			break;
		case BtnType.Trap:
			//print ("SetTrap!");
			SoundManager.Instance.PlaySound(SoundManager.SoundType.SetPoison);
			MainCharLogic.Instance.DropTrap();
			break;
		}
	}
}
