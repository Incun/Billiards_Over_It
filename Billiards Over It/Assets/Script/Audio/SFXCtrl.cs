using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXCtrl : MonoBehaviour
{
	public Slider sfxSlider;  // 효과음 슬라이더

	public AudioSource sfx;  // 효과음

	float temp_SFX = 1;  // 소리의 초기값 1

	void Start()
	{
		temp_SFX = PlayerPrefs.GetFloat("temp_SFX", 1);  // 소리를 1로 초기화
		sfxSlider.value = temp_SFX;  // 슬라이더 값을 1로 초기화
		sfx.volume = sfxSlider.value;  // 볼륨을 1로 초기화
	}

	void Update()
	{
		SFX_Slider();
	}

	public void SFX_Slider()
	{
		sfx.volume = sfxSlider.value;  // 볼륨을 슬라이더 벨류값으로

		temp_SFX = sfxSlider.value;  // 임시 SFX을 슬라이더 벨류값으로
		PlayerPrefs.SetFloat("temp_SFX", temp_SFX);  // temp_SFX저장
	}

}
