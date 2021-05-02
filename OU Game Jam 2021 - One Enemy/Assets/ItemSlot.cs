using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] GameObject Item;

    private void Start()
    {
        Item.SetActive(false);
    }

    public void ObtainItem()
    {
        Item.SetActive(true);
    }
}
