using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    public AudioClip mouseOverSound;
    public AudioClip mouseClickSound;
/*    [SerializeField] Button soundButton;*/

    bool isSoundOn = true;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
/*        soundButton.onClick.AddListener(SoundToggle);*/
    }
    
    void PlayGame()
    {
        print("play game");

        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
/*
    void SoundToggle()
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            print("sound is off");
        }
        else
        {
            isSoundOn = true;
            print("sound is on");
        }
    }*/
}
