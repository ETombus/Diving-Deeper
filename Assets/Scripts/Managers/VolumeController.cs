using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            (Resources.Load("MainMixer") as AudioMixer).SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            (Resources.Load("MainMixer") as AudioMixer).SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        }
    }

    public void MusicSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        (Resources.Load("MainMixer") as AudioMixer).SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SfxSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", sfxSlider.value);
        (Resources.Load("MainMixer") as AudioMixer).SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
    }
}
