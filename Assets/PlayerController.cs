using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 3.5f;
    [SerializeField] private float gravity = 20.0f;
    private Camera playerCamera;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;

    public CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private GameManager gameManager;
    private StoryManager storyManager;
    
    private PickupController currentPickup;
    private bool canInteract = false;

    private bool canGetTrueEnding = false;
    private bool canDrinkPoison = true;

    private bool ending = false;

    [SerializeField] private Text interactionText;

    [HideInInspector] public bool canMove = true;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        storyManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoryManager>();

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

        //interactions
        if (gameManager.playerHasSpeed) canInteract = true;
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (currentPickup != null)
            {
                if(currentPickup.pickupType == PickUpTypes.Poison)
                {
                    gameManager.playerHasPoison = true;
                    Destroy(currentPickup.gameObject);
                    interactionText.gameObject.SetActive(false);
                    currentPickup = null;
                    storyManager.PlayMarketStoryAudio();
                }
                if (currentPickup.pickupType == PickUpTypes.Speed_Potion && storyManager.playedCreepyGuyIntro)
                {
                    gameManager.playerHasSpeed = true;
                    Destroy(currentPickup.gameObject);
                    interactionText.gameObject.SetActive(false);
                    currentPickup = null;
                    storyManager.PlaySpeedPotionStoryAudio();
                    //activate story node 14 and 6
                    List<int> tempList = new List<int>();
                    tempList.Add(6);
                    tempList.Add(14);
                    gameManager.gameObject.GetComponent<ConnectionsManager>().EnableTriggers(tempList);
                }
            }
            else if (gameManager.playerHasSpeed)
            {
                canInteract = false;
                gameManager.UseSpeedPotion();
                canGetTrueEnding = true;
            }
            else if(canDrinkPoison)
            {
                ending = true;
                interactionText.gameObject.SetActive(false);
                storyManager.PlayOutroStoryAudio();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NarrativeTrigger")
        {
            other.gameObject.GetComponent<TriggerNodeInfo>().InvokeTrigger();
        }

        if(other.gameObject.name == "Narrative_Trigger_MapEnd")
        {
            gameManager.RespawnPlayer();
        }

        if (other.gameObject.tag == "EnemyWeapon")
        {
            GuardController guard = other.gameObject.transform.root.gameObject.GetComponent<GuardController>();
            if (guard.recovering)
            {
                //ROMEO GOT HIT
                canMove = false;
                Debug.Log("RIP Romeo");
                storyManager.StartCoroutine("fadePanelToBlack");
            }
        }

        if(other.gameObject.name == "Paris")
        {
            gameManager.KillParis(other.gameObject);
        }
        if(other.gameObject.name == "Guard")
        {
            gameManager.CallGuards();
        }

        if(other.gameObject.tag == "Button" && !gameManager.calledGuards)
        {
            gameManager.calledGuards = true;
            gameManager.CallGuards();
        }

        if(other.gameObject.name == "EndingTrigger" && canGetTrueEnding)
        {
            storyManager.PlayTrueEndingAudio();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            PickupController pickup = other.GetComponent<PickupController>();

            canInteract = true;

            if (pickup.pickupType == PickUpTypes.Poison)
            {
                if (gameManager.playerHasSpeed)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "You don't have enough money to buy the poison";
                    canInteract = false;
                }
                else
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "Press 'E' to buy the poison";
                }
            }
            else if(pickup.pickupType == PickUpTypes.Speed_Potion)
            {
                if (!storyManager.playedCreepyGuyIntro && !storyManager.playing)
                {
                    storyManager.PlayCreepyGuyIntro();
                }
                else if(storyManager.playedCreepyGuyIntro)
                {
                    if (gameManager.playerHasPoison)
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "You don't have enough money to buy the invisibility potion.";
                        canInteract = false;
                    }
                    else
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "Press 'E' to buy the invisibility potion";
                    }
                }
            }

            currentPickup = pickup;
        }
        else if(other.gameObject.tag == "Interactable" && gameManager.playerHasPoison)
        {
            if (!ending)
            {
                canInteract = true;

                interactionText.gameObject.SetActive(true);
                interactionText.text = "Press 'E' to use the poison";
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactionText.gameObject.activeInHierarchy)
        {
            interactionText.gameObject.SetActive(false);
        }
        canInteract = false;
        currentPickup = null;
    }
}