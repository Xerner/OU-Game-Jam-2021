using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	//[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.


    private void Awake()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    public void Move(Vector2 move)
	{
		// If the input is moving the player right and the player is facing left...
		if (move.x > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move.x < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		transform.position += new Vector3(move.x, move.y, 0);
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
