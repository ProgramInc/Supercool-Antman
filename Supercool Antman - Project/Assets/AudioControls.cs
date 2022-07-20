using UnityEngine;
using UnityEngine.UI;

public class AudioControls : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    AudioSource musicPlayer;
    AudioSource sfxPlayer;

    private void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
        musicPlayer = GameObject.FindWithTag("MusicPlayer").GetComponent<AudioSource>();
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
