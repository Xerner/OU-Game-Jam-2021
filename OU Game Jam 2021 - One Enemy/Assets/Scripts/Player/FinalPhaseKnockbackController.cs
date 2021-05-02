using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPhaseKnockbackController : MonoBehaviour
{
    [SerializeField]
    private AudioSource knockback;
    void Start()
    {
        StartCoroutine(BeginningKnockback());
    }
    IEnumerator BeginningKnockback()
    {
        PlayerMovement2D movement = GetComponent<PlayerMovement2D>();
        movement.DisbaleUserInput();
        yield return new WaitForSeconds(1);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 2000f);
        knockback.Play();
        yield return new WaitForSeconds(1f);
        movement.EnableUserInput();
    }
    public void ExternalKnockbackHandler(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * 100f);
    }
}
