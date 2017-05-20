using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup UIElement;
    private float startAlpha;
    private SoundController soundSource;

    void Start()
    {
        if (GetComponent<CanvasGroup>() != null)
        {
            UIElement = GetComponent<CanvasGroup>();
            startAlpha = UIElement.alpha;
            soundSource = (GameObject.Find("SoundController")).GetComponent<SoundController>();
        }
        else
            throw new System.Exception("Dodaj component CanvasGroup do calego przycisku");
    }
    // Use this for initialization
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIElement.alpha = 1;
        soundSource.playHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIElement.alpha = startAlpha;
    }
}
