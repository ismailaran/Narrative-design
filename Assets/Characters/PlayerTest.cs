using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float gravity = 20.0f;
    private Camera playerCamera;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;

    private CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [HideInInspector] public bool canMove = true;
    [SerializeField] private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        float curSpeedX = canMove ? walkingSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkingSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        moveDirection.y = movementDirectionY;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        anim.SetFloat("Speed", characterController.velocity.magnitude);

        // Player and Camera rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Pickup")
        {
            NarrativeTrigger hitTrigger = other.gameObject.GetComponent<NarrativeTrigger>();

            if (hitTrigger != null && !hitTrigger.isActivated)
            {
                hitTrigger.TriggerEvent();
            }
            else if (hitTrigger == null)
            {
                Itriggerable trigger = other.gameObject.GetComponent<RespawnTrigger>();
                if (trigger != null)
                {
                    trigger.TriggerEvent();
                }
            }
        }
        if (other.gameObject.tag == "NarrativeTrigger")
        {
            other.gameObject.GetComponent<TriggerNodeInfo>().InvokeTrigger();
        }
    }
}
