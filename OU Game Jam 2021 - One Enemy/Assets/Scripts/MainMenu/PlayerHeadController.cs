using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeadController : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if (slider is null) Debug.LogWarning("PlayerHeadController: slider not set in inspector");
    }

    private void Update()
    {
        if (slider != null) transform.localScale = new Vector3(slider.value+1, slider.value+1, 1f);
    }
}
