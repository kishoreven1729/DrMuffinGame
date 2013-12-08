using UnityEngine;
using System.Collections;

public class UI_BtnControl : MonoBehaviour {

	public enum BtnType
	{
		GoStart,
		GoMain,
		GoCredit,
		GoHelp,
		DoNothing
	}

	public BtnType type;
	

	void OnClick()
	{
		switch(type)
		{
		case BtnType.GoStart:
			Application.LoadLevel(0);
			break;
		case BtnType.GoMain:
			StartCoroutine("GoMain");
			break;
		case BtnType.GoCredit:
			StartCoroutine("GoCredit");
			break;
		case BtnType.GoHelp:
			StartCoroutine("GoHelp");
			break;
		case BtnType.DoNothing:
			break;
		}
	}

	IEnumerator GoMain()
	{
		UI_Manager.Instance.DeactivateStartPanel();
		yield return new WaitForSeconds(1f);
		UI_Manager.Instance.ActivateMainGamePanel();
//		Camera.main.SendMessage("ZoomOut");
		Camera.main.SendMessage("Focus", true);
		yield return new WaitForSeconds(1f);
		UI_Manager.Instance.BackCount();
	}

	IEnumerator GoCredit()
	{
		UI_Manager.Instance.DeactivateStartPanel();
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(1);
	}

	IEnumerator GoHelp()
	{
		UI_Manager.Instance.DeactivateStartPanel();
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(2);
	}

}
