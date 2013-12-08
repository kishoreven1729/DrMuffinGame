using UnityEngine;
using System.Collections;

public class UI_GameOverControl : MonoBehaviour {

	public enum BtnType
	{
		GoStart,
		GoReplay
	}

	public BtnType type;

	void OnClick()
	{
		switch(type)
		{
		case BtnType.GoStart:
			GameDirector.gameInstance.SwitchState(States.Start);
			UI_Manager.Instance.DeactivateGameOverPanel();
			UI_Manager.Instance.DeactivateMainGamePanel();
			UI_Manager.Instance.ActivateStartPanel();
			Camera.main.SendMessage("Focus", false);
			break;
		case BtnType.GoReplay:
			GameDirector.gameInstance.SpawnCharacter(true);

			UI_Manager.Instance.DeactivateGameOverPanel();
			UI_Manager.Instance.ActivateMainGamePanel();
			UI_Manager.Instance.BackCount();
			break;
		}
	}
}
