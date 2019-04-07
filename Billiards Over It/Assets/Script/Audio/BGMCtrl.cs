using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMCtrl : MonoBehaviour
{
	public Slider bgmSlider;  // 배경음 슬라이더

	public AudioSource bgm;  // 배경음

	float temp_BGM = 1;  // 소리의 초기값 1

	private void Awake()
	{
		GameObject.Find("Main Canvas").transform.Find("Option Canvas").gameObject.SetActive(true);
		bgmSlider = GameObject.Find("BgmSlider").GetComponent<Slider>();
		GameObject.Find("Main Canvas").transform.Find("Option Canvas").gameObject.SetActive(false);

		temp_BGM = PlayerPrefs.GetFloat("temp_BGM", 1);  // 배경음 수치가 없으면 1로 초기화
		bgmSlider.value = temp_BGM;  // 슬라이더 값을 bgm 값으로 초기화
		bgm.volume = bgmSlider.value;  // 볼륨을 value로 초기화
	}

	void Start()
	{
		temp_BGM = PlayerPrefs.GetFloat("temp_BGM", 1);  // 배경음 수치가 없으면 1로 초기화
		bgmSlider.value = temp_BGM;  // 슬라이더 값을 bgm 값으로 초기화
		bgm.volume = bgmSlider.value;  // 볼륨을 value로 초기화
	}

	void Update()
	{
		BGM_Slider();
		if (bgmSlider == null)
		{
			GameObject.Find("Main Canvas").transform.Find("Option Canvas").gameObject.SetActive(true);
			bgmSlider = GameObject.Find("BgmSlider").GetComponent<Slider>();
			GameObject.Find("Main Canvas").transform.Find("Option Canvas").gameObject.SetActive(false);
		}
	}

	public void BGM_Slider()
	{
		bgm.volume = bgmSlider.value;  // 볼륨을 슬라이더 벨류값으로

		temp_BGM = bgmSlider.value;  // 임시 BGM을 슬라이더 벨류값으로
		PlayerPrefs.SetFloat("temp_BGM", temp_BGM);  // temp_BGM저장
	}
}
