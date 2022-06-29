using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button soundButton;
    [SerializeField] Button quitButton;

    bool isSoundOn = true;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayGame);
        soundButton.onClick.AddListener(SoundToggle);
        quitButton.onClick.AddListener(QuitGame);
    }
    
    void PlayGame()
    {
        print("play game");
    }

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
    }

    void QuitGame()
    {
        //Application.Quit();
        print("quitting game");
    }
}
