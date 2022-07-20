using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel;

    public delegate void PauseMenuToggleAction();
    public static PauseMenuToggleAction OnPauseMenuToggled;

    private void OnEnable()
    {
        OnPauseMenuToggled += PauseMenuToggle;
    }

    private void OnDisable()
    {
        OnPauseMenuToggled -= PauseMenuToggle;
    }

    private void PauseMenuToggle()
    {
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeInHierarchy);
        Time.timeScale = pauseMenuPanel.activeInHierarchy ? 0 : 1;
    }

}
