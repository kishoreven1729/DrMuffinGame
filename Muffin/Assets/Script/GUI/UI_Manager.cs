using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour {
	
	public GameObject mainGamePanel;
	public GameObject play;
	public GameObject help;
	public GameObject credit;
	public GameObject gameOverPanel;
	
	public GameObject HUDpanel;

	public static UI_Manager Instance;


	void Awake ()
	{
		Instance = this;
	}

	void Start()
	{
		ActivateStartPanel();
	}


	public void ActivateStartPanel()
	{
		StartCoroutine("_ActivateStartPanel");
	}

	IEnumerator _ActivateStartPanel()
	{
		yield return new WaitForSeconds(0.2f);

		play.animation.Play("PlayMoveIn");
		SendMessage("PlayTheSound");

		yield return new WaitForSeconds(0.2f);

		help.animation.Play("HelpMoveIn");
		SendMessage("PlayTheSound");

		yield return new WaitForSeconds(0.2f);

		credit.animation.Play("CreditMoveIn");
		SendMessage("PlayTheSound");
	}


	public void DeactivateStartPanel()
	{
		StartCoroutine("_DeactivateStartPanel");
	}

	IEnumerator _DeactivateStartPanel()
	{
		play.animation.Play("PlayMoveOut");
		SendMessage("PlayTheSound");
		
		yield return new WaitForSeconds(0.2f);

		help.animation.Play("HelpMoveOut");
		SendMessage("PlayTheSound");

		yield return new WaitForSeconds(0.2f);

		credit.animation.Play("CreditMoveOut");
		SendMessage("PlayTheSound");
	}

	public void ActivateMainGamePanel()
	{
		mainGamePanel.animation.Play();
		SendMessage("PlayTheSound");
	}

	public void DeactivateMainGamePanel()
	{
		mainGamePanel.animation.Play("MainGamePanelOut");
		SendMessage("PlayTheSound");
	}

	public void ActivateGameOverPanel()
	{
		gameOverPanel.animation.Play();
		SendMessage("PlayTheSound");
	}

	public void DeactivateGameOverPanel()
	{
		gameOverPanel.animation.Play("GameOverMoveOut");
		SendMessage("PlayTheSound");
	}

	public void BackCount()
	{
		HUDpanel.SendMessage("BackCount");
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 10, 10), "i"))
		{
			ActivateGameOverPanel();
			DeactivateMainGamePanel();
		}
	}

}
