using UnityEngine;
using UnityEngine.UI;

public class AudioControls : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private void Start()
    {
        ReadVolumesFromPlayerPrefs();
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music Volume", volume);
        musicPlayer.volume = volume;        
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX Volume", volume);
        sfxPlayer.volume = volume;
    }

    void ReadVolumesFromPlayerPrefs()
    {
        musicPlayer.volume = PlayerPrefs.GetFloat("Music Volume", 0.5f);
        sfxPlayer.volume = PlayerPrefs.GetFloat("SFX Volume", 0.5f);
        musicSlider.value = musicPlayer.volume;
        sfxSlider.value = sfxPlayer.volume;
    }
}
