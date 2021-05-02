using UnityEngine;

public class PlayerInputManager: MonoBehaviour {
    [Tooltip("Sensitivity of camera when looking around")]
    public float LookSensitivity = 0.1f;

    // GameManager m_GameManager;
    PlayerCharacterController m_PlayerCharacterController;
    bool m_LeftMouseClickWasHeld;

    void Start()
    {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, PlayerInputManager>(
            m_PlayerCharacterController, this, gameObject);
        // m_GameManager = FindObjectOfType<GameManager>();
        // DebugUtility.HandleErrorIfNullFindObject<GameManager, PlayerInputManager>(m_GameManager, this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        m_LeftMouseClickWasHeld = GetLeftMouseClickHeld();
    }

    public bool CanProcessInput()
    {
        // return Cursor.lockState == CursorLockMode.Locked && !m_GameManager.GameIsEnding;
        return true; // remove later
    }

    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            Vector3 move = new Vector3(
                Input.GetAxis("Horizontal"),
                0f,
                Input.GetAxis("Vertical")
            );

            // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        return Vector3.zero;
    }

    public float GetLookInputsHorizontal()
    {
        return GetMouseLookAxis("Mouse X");
    }

    public float GetLookInputsVertical()
    {
        return GetMouseLookAxis("Mouse Y");
    }

    public bool GetJumpInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetButtonDown("Jump");
        }

        return false;
    }

    public bool GetSubmitInputDown() {
        if (CanProcessInput()) {
            return Input.GetButtonDown("Submit");
        }
        return false;
    }

    public bool GetJumpInputHeld()
    {
        if (CanProcessInput())
        {
            return Input.GetButton("Jump");
        }

        return false;
    }

    public bool GetFireInputDown()
    {
        return GetLeftMouseClickHeld() && !m_LeftMouseClickWasHeld;
    }

    public bool GetFireInputReleased()
    {
        return !GetLeftMouseClickHeld() && m_LeftMouseClickWasHeld;
    }

    public bool GetLeftMouseClickHeld()
    {
        if (CanProcessInput())
        {
            return Input.GetMouseButtonDown(0);
        }

        return false;
    }

    public bool GetRightMouseClickHeld()
    {
        if (CanProcessInput())
        {
            bool i = Input.GetMouseButtonDown(1);
            return i;
        }

        return false;
    }

    public bool GetInteractInputHeld()
    {
        if (CanProcessInput())
        {
            return Input.GetButton("Interact");
        }

        return false;
    }

    public bool GetCrouchInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetButtonDown("Crouch");
        }

        return false;
    }

    public bool GetCrouchInputReleased()
    {
        if (CanProcessInput())
        {
            return Input.GetButtonUp("Crouch");
        }

        return false;
    }

    public bool GetReloadButtonDown()
    {
        if (CanProcessInput())
        {
            // return Input.GetButtonDown(GameConstants.k_ButtonReload);
        }

        return false;
    }


        float GetMouseLookAxis(string mouseInputName)
    {
        if (CanProcessInput())
        {
            // Check if this look input is coming from the mouse
            float i = Input.GetAxis(mouseInputName);

            // apply sensitivity multiplier
            i *= LookSensitivity;

            return i;
        }

        return 0f;
    }
}
