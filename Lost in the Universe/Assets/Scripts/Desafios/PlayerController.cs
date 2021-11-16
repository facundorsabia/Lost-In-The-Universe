using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
	private CharacterController controller;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float turnSpeed = 80f;
	[SerializeField] private Vector3 moveDirection = Vector3.zero;
	[SerializeField] private float gravity = -9.8f;
    private bool isGrounded;
    [SerializeField] private float jumpHeight = 1f;
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
    private bool jumping = false;
    private bool gateActivated = false;

    private Animator animPlayer;

    private float gateTime;

    private float cooldownTime = 0.2f;
    private float cameraAxis;

    Vector3 _initialPosition;

    private float counterWall;

    [SerializeField] private GameObject [] wallPoints;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bienvenidos a mi primer juego");
        transform.localScale = new Vector3 (playerScale, playerScale, playerScale);
        _initialPosition = transform.position;
        animPlayer = GetComponent<Animator>();
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
            Debug.Log("flip");
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
            Debug.Log("jump");
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

    //Method to detect gameObject names on collision
    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    //Method to transform player Scale with triggering the gate
    private void OnTriggerEnter (Collider other)
    {
        if (Time.time > gateTime)
        {
            if ((other.gameObject.layer == 7) && (gateActivated == false))
            {
                transform.localScale = new Vector3 (playerScaleGate, playerScaleGate, playerScaleGate);
                gateActivated = true;
                gateTime = Time.time + cooldownTime;
            }else
            {
                transform.localScale = new Vector3 (playerScale, playerScale, playerScale);
                gateActivated = false;
            }
        }
    }

      //Method to move wall on triggerStay
     private void OnTriggerStay (Collider other)
     {
         if (other.gameObject.layer == 6)
         {
             counterWall += Time.deltaTime;
             if (counterWall >= 2)
             {
                 Debug.Log("movimiento");
                int wallPointsIndex = Random.Range (0, wallPoints.Length);
                 other.gameObject.transform.position = wallPoints[wallPointsIndex].transform.position;
                 other.gameObject.transform.rotation = wallPoints[wallPointsIndex].transform.rotation;
                 counterWall = 0;
             }
         }
     }

}
