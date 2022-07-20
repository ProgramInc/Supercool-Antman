using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MainMenuManager mainMenuManager;
    Animator animator;
    AudioSource audioSource;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
        mainMenuManager = FindObjectOfType<MainMenuManager>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isMouseOnButton", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isMouseOnButton", true);
        audioSource.PlayOneShot(mainMenuManager.mouseOverSound);
    }
}
