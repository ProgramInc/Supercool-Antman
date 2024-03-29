using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathButtonManager : MonoBehaviour
{
    [SerializeField] GameObject buttonsPanel;
    /*[SerializeField] Image restartImage;
    [SerializeField] Image menuImage;*/
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;

    public AudioClip mouseOverSound;
    public AudioClip mouseClickSound;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(BackToMainMenu);
        PlayerStats.OnPlayerDeath += WrapperForActivateDeathButtons;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= WrapperForActivateDeathButtons;
    }

    private void Start()
    {
        buttonsPanel.SetActive(false);
        /*restartImage.enabled = false;
        menuImage.enabled = false;
        restartButton.enabled = false;
        restartImage.enabled = false;*/
    }

    private IEnumerator ActivateDeathButtons()
    {
        yield return new WaitForSeconds(5);
        buttonsPanel.SetActive(true);
        /*restartImage.enabled = true;
        menuImage.enabled = true;
        restartButton.enabled = true;
        restartImage.enabled = true;

        print("suppose to activate panel");*/
    }

    private void WrapperForActivateDeathButtons()
    {
        StartCoroutine(nameof(ActivateDeathButtons));
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
        
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
