using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public float speedPlayer;
    public Vector3 playerDirection;

    public CharacterController playerController;
    public Animator anim;
    

    //Camara
    public Camera mainCamera;
    public Vector3 camForward;
    public Vector3 camRigth;

    //Gravedad
    public float gravity = -9.8f;
    Vector3 playerMove;
    public float fallVelocity;

    //Jump / salto
    public float jumpForze;
    
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerDirection = new Vector3(horizontalMove, 0f, verticalMove).normalized;

        //Actualizo la direccion de la camara
        CameraDirection();

        //Vinculando la direccion local del personaje al eje en relacion a la camara
        playerMove = playerDirection.x * camRigth + playerDirection.z * camForward;

        playerMove = playerMove * speedPlayer;

        float currentSpeed = playerController.velocity.magnitude;

        float speedNormalize = currentSpeed / speedPlayer;

        if (speedNormalize <= 0f) speedNormalize = 0;

        anim.SetFloat("Speed", speedNormalize);



        //Indicando que el jugador mire hacia la direccion donde estamos presionando la tecla.
        playerController.transform.LookAt(playerController.transform.position + playerMove);

        //Gravedad
        SetGravity();
        Salto();

        playerController.Move(playerMove * Time.deltaTime);

        Debug.Log(playerController.velocity.magnitude);

    }

    void CameraDirection()
    {
        camForward = mainCamera.transform.forward;
        camRigth = mainCamera.transform.right;

        camForward.y = 0f;
        camRigth.y = 0f;

        camForward = camForward.normalized;
        camRigth = camRigth.normalized;
    }

    void SetGravity()
    {
        if (!playerController.isGrounded)
        {
            fallVelocity += gravity * Time.deltaTime;
            playerMove.y = fallVelocity;
        }
        else 
        {
            fallVelocity = gravity * Time.deltaTime;
            playerMove.y = fallVelocity;
        }
    }

    void Salto()
    {
        if (playerController.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForze;
            playerMove.y = fallVelocity;
            
        }

    }
}
