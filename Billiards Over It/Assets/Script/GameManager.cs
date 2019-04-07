using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject clearPanel;
	public GameObject overPanel;
	public GameObject startPanel;

	Image clearPanela;
	Image overPanela;
	Image startPanela;

	Color tempColor;
	Color color;
	Color wallColor;

	public static GameManager instance;

	public WaitForFixedUpdate fixupdate = new WaitForFixedUpdate();

	public int isclear = 0;

	public int demandStar;
	public int maxDrag;
	public int curDrag = 0;

	public Text swipeText;

	public GameObject hole;
	public GameObject wall;
	public GameObject wall_Whole;
	public GameObject wall_Half;
	SpriteRenderer wallsr;
	
	public bool isRestart;
	bool clear = false;



	private void Awake()
	{
		if (instance)
		{
			Destroy(instance);
		}
		instance = this;
		clearPanel.SetActive(true);
		overPanel.SetActive(true);
		startPanel.SetActive(true);

		clearPanela = clearPanel.GetComponent<Image>();
		overPanela = overPanel.GetComponent<Image>();
		startPanela = startPanel.GetComponent<Image>();

		clearPanel.SetActive(false);
		overPanel.SetActive(false);
		startPanel.SetActive(false);

	}

	private void Start()
	{
		isRestart = false;
		PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString(), isclear);
		curDrag = 0;
		wallsr = wall.GetComponent<SpriteRenderer>();
		StartCoroutine(StartPanelFadeOut());
	}

	private void Update()
	{
		if (demandStar == 0 && clear == false)
		{
			clear = true;
			StartCoroutine(WallFadeOut());
		}

		swipeText.text = curDrag.ToString() + " / " + maxDrag.ToString();
	}

	// Coroutine
	public IEnumerator WallFadeOut()
	{
		color = tempColor;
		wall_Half.SetActive(true);
		wall_Whole.SetActive(false);
		hole.SetActive(true);
		wallColor.a = 1;
		wallColor.r = 1;
		wallColor.g = 1;
		wallColor.b = 1;
		while (true)
		{
			if (wallsr.color.a <= 0)
			{
				yield break;
			}
			wallColor.a -= 0.05f;
			wallsr.color = wallColor;
			yield return fixupdate;
		}
	}

	public IEnumerator StartPanelFadeIn()  // 재시작시 점점 어두워지게
	{
		color = tempColor;
		color.a = 0;
		color.r = 0;
		color.g = 0;
		color.r = 0;
		startPanela.color = color;
		startPanel.SetActive(true);
		while (true)
		{
			if (startPanela.color.a >= 1)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				yield break;
			}
			color.a += 0.05f;
			startPanela.color = color;
			yield return fixupdate;
		}
	}

	public IEnumerator NextPanelFadeIn()  // 다음 스테이지 갈때 점점 어두워지게
	{
		color = tempColor;
		color.a = 0;
		color.r = 0;
		color.g = 0;
		color.r = 0;
		startPanela.color = color;
		startPanel.SetActive(true);
		while (true)
		{
			if (startPanela.color.a >= 1)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				yield break;
			}
			color.a += 0.05f;
			startPanela.color = color;
			yield return fixupdate;
		}
	}

	public IEnumerator PreviousPanelFadeIn()  // 이전 스테이지 갈 때 점점 어두워지게
	{
		color = tempColor;
		color.a = 0;
		color.r = 0;
		color.g = 0;
		color.r = 0;
		startPanela.color = color;
		startPanel.SetActive(true);
		while (true)
		{
			if (startPanela.color.a >= 1)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
				yield break;
			}
			color.a += 0.05f;
			startPanela.color = color;
			yield return fixupdate;
		}
	}

	public IEnumerator StartPanelFadeOut()  // 시작시 점점 밝아지게
	{
		color = tempColor;
		color.a = 1;
		color.r = 0;
		color.g = 0;
		color.r = 0;
		startPanela.color = color;
		startPanel.SetActive(true);
		while (true)
		{
			if (startPanela.color.a <= 0)
			{
				startPanel.SetActive(false);
				yield break;
			}
			color.a -= 0.05f;
			startPanela.color = color;
			yield return fixupdate;
		}
	}

	public IEnumerator ClearPanelFadeIn()  // 클리어시 점점 어두워지게
	{
		color = tempColor;
		color.a = 0;
		color.r = 0;
		color.g = 0;
		color.r = 0;
		clearPanela.color = color;
		clearPanel.SetActive(true);
		while (true)
		{
			if (clearPanela.color.a >= 1)
			{
				yield break;
			}
			color.a += 0.05f;
			clearPanela.color = color;
			yield return fixupdate;
		}
	}

	public IEnumerator OverPanelFadeIn()  // 게임오버시 점점 어두워지게
	{
		color = tempColor;
		color.a = 0;
		color.r = 0;
		color.g = 0;
		color.r = 0;
		overPanela.color = color;
		overPanel.SetActive(true);
		while (true)
		{
			if (overPanela.color.a >= 1)
			{
				yield break;
			}
			color.a += 0.05f;
			overPanela.color = color;
			yield return fixupdate;
		}
	}
}
