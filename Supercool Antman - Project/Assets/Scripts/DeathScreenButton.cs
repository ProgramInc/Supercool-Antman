using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AudioSource audioSource;

    Animator animator;
    DeathButtonManager deathButtonManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        deathButtonManager = GetComponentInParent<DeathButtonManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(deathButtonManager.mouseOverSound);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(deathButtonManager.mouseClickSound);
    }
}
