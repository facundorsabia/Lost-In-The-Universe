using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAstronaut : MonoBehaviour
{
    private InventoryManager mgInventory;
    private Animator anim;
	private CharacterController controller;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float turnSpeed = 80f;
	[SerializeField] private Vector3 moveDirection = Vector3.zero;
	[SerializeField] private float gravity = -9.8f;
    private bool isGrounded;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    Vector3 velocity;

    [SerializeField] private int playerLives = 7;
    [SerializeField] private string playerName = "Harlan";
    [SerializeField] private float playerScale = 10f;
    [SerializeField] private float playerScaleGate = 5f;

    private bool isFlip = false;
    private bool isRun = false;
    private bool isJump = false;
    private bool gateActivated = false;

    private Animator animPlayer;

    private float gateTime;

    private float cooldownTime = 0.2f;
    private float cameraAxis;

    Vector3 _initialPosition;

    private float healCounter;

    [SerializeField] private GameObject [] wallPoints;

    [SerializeField] private GameObject spaceShip;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Consigue gemas para transformar en combustible y escapar en tu nave de este horrible planeta. Los Aliens y algunas plantas son venenosos, ten cuidado.");
        transform.localScale = new Vector3 (playerScale, playerScale, playerScale);
        _initialPosition = transform.position;
        animPlayer = GetComponent<Animator>();
        mgInventory = GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
        Move();
        animPlayer.SetBool("isRun", isRun);
        Flip();
        animPlayer.SetBool("isFlip", isFlip);
        Jump();
        animPlayer.SetBool("isJump", isJump);
        PlayerDead();

        if (Input.GetKeyUp(KeyCode.G))
        {
            UseItem();
        }

    }
    // Custom Methods

    private void Move(){

			if(controller.isGrounded){
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);

            if ( ((Input.GetAxis("Horizontal")) != 0) || ((Input.GetAxis("Vertical")) != 0) ){
                isRun = true;
            }else{
                isRun = false;
            }

        
    }

    private void Flip (){
        if (Input.GetKeyDown ("tab")) {
            isFlip = true;
        }else {
             isFlip = false;
        }
    }

    private void Jump(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;  
        if(isGrounded && velocity.y<0)
        {
				velocity.y = -2.0f;
                isJump = false;
		}
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown ("space") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJump = true;
        }
    }

    private void HealPlayer() 
    {
        playerLives = playerLives + 1;
    }

    private void DamagePlayer() 
    {
        playerLives = playerLives - 1;
    }

 
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
        DamagePlayer();
        Debug.Log("Los Aliens son venenosos, intenta no tocarlos. Perdiste una vida y ahora tienes " + playerLives);  
        }

    }

       private void PlayerDead() 
    {
        if(playerLives <= 0){
            Debug.Log("PERDISTE TODAS TUS VIDAS");
            playerLives = 7;
        }
    }

    //Method to transform player Scale with triggering the gate
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Debug.Log("Has conseguido combustible para tu nave! Ya tienes " + GameManager.getScore() + " gemas. Consigue 20 y estarás listo.");
            GameManager.addScore();
            GameObject gem = other.gameObject;
            gem.SetActive(false);
            mgInventory.AddInventoryOne(gem);
        }
    }

    private void OnTriggerStay (Collider other)
     {
         if (other.gameObject.layer == 9)
         {
             healCounter += Time.deltaTime;
             if (healCounter >= 5)
             {
                 Debug.Log("Tus vidas son " + playerLives);
                HealPlayer();
        
                 healCounter = 0;
             }
         }

        if (other.gameObject.layer == 10)
         {
             healCounter += Time.deltaTime;
             if (healCounter >= 5)
             {
                DamagePlayer();
                 Debug.Log("Estas perdiendo vidas, esta planta es VENENOSA: " + playerLives);
                 healCounter = 0;
             }
         }
     }

    private void UseItem()
    {
        GameObject gem = mgInventory.GetInventoryOne();
        gem.SetActive(true);
        gem.transform.position = spaceShip.transform.position + new Vector3(1f, 0, 0);
    }
     
}

