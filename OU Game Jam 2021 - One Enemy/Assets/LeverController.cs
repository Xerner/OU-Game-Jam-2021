using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LeverController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (name != "Lever") Debug.LogError("Lever instance is not named \"Lever\". Please name it that so it works with the interactable system.\nCurrent name: " + name);
    }

    public void Activate()
    {
        spriteRenderer.sprite = active;
    }

    public void DeActivate()
    {
        spriteRenderer.sprite = inactive;
    }
}
