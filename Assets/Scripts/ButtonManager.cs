using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Animator buttonAnim;

    // Start is called before the first frame update
    void Start()
    {
        buttonAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnim.SetBool("isMouseOver", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnim.SetBool("isMouseOver", false);
        buttonAnim.SetBool("isPressed", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonAnim.SetBool("isPressed", true);
    }
}
