using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayerInteraction()
    {
        //fill out a list of player interactions based on the objects type
        //Debug.Log("PlayerInteraction on: " + name);
        switch (name)
        {
            case "Lever":
                GetComponent<LeverController>().Activate();
                tag = "Untagged";
                break;
            case "Laser gun":
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    public void BossInteraction()
    {

    }
}
