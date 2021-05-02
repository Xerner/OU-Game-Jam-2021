using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    [SerializeField] Sprite hoveredImage;
    [SerializeField] Sprite unhoveredImage;

    void Awake() {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoveredImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = unhoveredImage;
    }
}
