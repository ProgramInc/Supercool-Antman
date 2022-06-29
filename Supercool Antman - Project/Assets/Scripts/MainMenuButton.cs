using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isMouseOnButton", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isMouseOnButton", true);
    }
}
