using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
	public GameObject bgm_ctrl;
	private static int count = 0;
	private int index;
	/*
	private void Awake()
	{
		if(bgm_ctrl.activeSelf == false && GameObject.Find("BGM_Manager") == false)
		{
			bgm_ctrl.SetActive(true);
			DontDestroyOnLoad(bgm_ctrl);  // 씬 넘어갈때 배경음 유지
		}

	}*/

	void Awake()
	{
		index = count;
		count++;

		if (index == 0)
			DontDestroyOnLoad(bgm_ctrl);
		else
			Destroy(bgm_ctrl);
	}
}