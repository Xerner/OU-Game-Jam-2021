using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController2D : MonoBehaviour
{
	[Header("Arms")]
	[SerializeField] Transform GunArm;
	[SerializeField] Transform LeftArm;
	[SerializeField] Transform RightArm;
	Transform disabledArm;

	PlayerCombatController combatController;
	bool m_FacingRight = true;
	float aimThreshold = 0.5f; // Threshold should prevent the arm flicking backwards when the analog stick is released

	private void Awake()
    {
		combatController = GetComponent<PlayerCombatController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    private void Start()
    {
		if (GunArm is null) Debug.LogWarning(name + " CharacterController2D: Please initialize the GunArm object in the Inspector");
		disabledArm = RightArm;
		ChangeCombatMode(combatController.startRanged);
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
		transform.position += new Vector3(move.x, move.y, 0f);
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;

        // keep the GunArm on the same side when the player flips
        if (FacingRight(theScale))
        {
            GunArm.localPosition = new Vector3(-Mathf.Abs(GunArm.localPosition.x), GunArm.localPosition.y, GunArm.localPosition.z);
			if (combatController.IsRanged)
            {
				LeftArm.gameObject.SetActive(true);
				RightArm.gameObject.SetActive(false);
            }
			disabledArm = RightArm;
		}
        else
        {
            GunArm.localPosition = new Vector3(Mathf.Abs(GunArm.localPosition.x), GunArm.localPosition.y, GunArm.localPosition.z);
			if (combatController.IsRanged)
            {
				LeftArm.gameObject.SetActive(false);
				RightArm.gameObject.SetActive(true);
            }
			disabledArm = LeftArm;
		}

        transform.localScale = theScale;
	}

    private static bool FacingRight(Vector3 scale)
    {
        return scale.x > 0;
    }

    private bool GunArmOnLeftSide()
    {
		bool onleftside = transform.localScale.x > 0 && GunArm.localPosition.x > 0 || transform.localScale.x < 0 && GunArm.localPosition.x < 0; ;
		Debug.Log("On Left Side: " + onleftside);
		return onleftside;
	}

    public void ChangeCombatMode(bool ranged)
    {
		combatController.IsRanged = ranged;
		GunArm.gameObject.SetActive(ranged);
		disabledArm.gameObject.SetActive(!ranged);
	}

	public void SwitchCombatMode(CallbackContext context)
	{
		if (context.ReadValueAsButton() == true && context.performed == false)
        {
			ChangeCombatMode(!combatController.IsRanged);
			//Debug.Log("Ranged Combat: " + combatController.IsRanged);
		}
	}

	public void AimListener(CallbackContext context)
	{
		Vector2 aim = context.ReadValue<Vector2>();
		//Debug.Log(context.control.path);
		if (combatController.IsRanged)
        {
			AimGunArm(context, aim);
        }
    }

    private void AimGunArm(CallbackContext context, Vector2 aim)
    {
        if (context.control.path == "/Mouse/delta")
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            aim = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            if (aim.magnitude > aimThreshold / 10)
            {
                Debug.Log(Vector2.Distance(transform.position, mousePosition));
                float rotation = Vector2.SignedAngle(Vector2.down, aim);
                GunArm.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rotation * transform.localScale.x));
            }
        }
        else
        {
			// Threshold should prevent the arm flicking backwards when the analog stick is released
            if (aim.magnitude > aimThreshold / 10)
            {
                float rotation = Vector2.Angle(Vector2.down, aim);
                GunArm.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rotation * Mathf.Sign(aim.x) * transform.localScale.x));
            }
        }
    }
}
