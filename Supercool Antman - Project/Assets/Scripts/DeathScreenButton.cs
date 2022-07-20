using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isMouseOnButton", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isMouseOnButton", false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
