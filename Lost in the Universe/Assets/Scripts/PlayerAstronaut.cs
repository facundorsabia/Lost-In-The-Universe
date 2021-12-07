using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAstronaut : MonoBehaviour
{
    //DESIGN DATA
    [SerializeField] private float speed = 10f;
	[SerializeField] private float turnSpeed = 80f;
	[SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private float playerScale = 10f;
    private InventoryManager mgInventory;
    private Animator anim;
    private Animator animPlayer;
	private CharacterController controller;
	[SerializeField] private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    Vector3 velocity;
    [SerializeField] private string playerName = "Harlan";
    private bool isFlip = false;
    private bool isRun = false;
    private bool isJump = false;
    private bool gateActivated = false;
    private float cameraAxis;
    Vector3 _initialPosition;
    private float healCounter;
    [SerializeField] private GameObject spaceShip;

    //Events

    public static event Action onDeath;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Consigue gemas para transformar en combustible y escapar en tu nave de este horrible planeta. Los Aliens y algunas plantas son venenosos, ten cuidado.");
        transform.localScale = new Vector3 (playerScale, playerScale, playerScale);
        _initialPosition = transform.position;
        animPlayer = GetComponent<Animator>();
        mgInventory = GetComponent<InventoryManager>();
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        animPlayer.SetBool("isRun", isRun);
        Flip();
        animPlayer.SetBool("isFlip", isFlip);
        Jump();
        animPlayer.SetBool("isJump", isJump);

        if (Input.GetKeyUp(KeyCode.G))
        {
            UseItem();
        }

        GameOver();
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

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
      Debug.Log(hit.gameObject.name);
  }

 
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
        GameManager.DamagePlayer(); 
        }

        if (collision.gameObject.CompareTag("bullet"))
        {
        GameManager.DamagePlayer(); 
        }

    }


    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Debug.Log("Has conseguido combustible para tu nave! Ya tienes " + GameManager.getScore() + " gemas. Consigue 20 y estar�s listo.");
            GameManager.addScore();
            GameObject gem = other.gameObject;
            gem.SetActive(false);
            mgInventory.AddInventoryOne(gem);
            mgInventory.CountGem(gem);
            AudioManager.instance.GemSFX();
        }
        if (other.gameObject.CompareTag("GemPlant"))
        {
            Debug.Log("Esta planta hace crecer las gemas a su alrededor");
        }
    }

    private void OnTriggerStay (Collider other)
     {
         if (other.gameObject.layer == 9)
         {
             healCounter += Time.deltaTime;
             if (healCounter >= 5)
             {

                GameManager.HealPlayer();
        
                 healCounter = 0;
             }
         }

        if (other.gameObject.layer == 10)
         {
             healCounter += Time.deltaTime;
             if (healCounter >= 5)
             {
                GameManager.DamagePlayer();
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
     
    private void GameOver()
    {
        if (GameManager.GetPlayerLives() <= 0)
        {
            Debug.Log("Game Over");
            onDeath?.Invoke();
        }
    }

}

