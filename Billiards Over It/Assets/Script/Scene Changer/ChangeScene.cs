using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	public void stage1_1()
	{
		SceneManager.LoadScene("Stage1-1");
	}

	public void stage1_2()
	{
		SceneManager.LoadScene("Stage1-2");
	}

	public void stage1_3()
	{
		SceneManager.LoadScene("Stage1-3");
	}

	public void stage1_4()
	{
		SceneManager.LoadScene("Stage1-4");
	}

	public void stage1_5()
	{
		SceneManager.LoadScene("Stage1-5");
	}

	public void Restart()
	{
		StartCoroutine(GameManager.instance.StartPanelFadeIn());
	}  // 재시작

	public void nextStage()
	{
		if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 0) == 1)
		{
			StartCoroutine(GameManager.instance.NextPanelFadeIn());
		}
	}

	public void previousStage()
	{
		StartCoroutine(GameManager.instance.PreviousPanelFadeIn());
	}
}
