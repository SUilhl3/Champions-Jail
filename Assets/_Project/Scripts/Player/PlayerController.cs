using UnityEngine;
<<<<<<< HEAD

public class PlayerController : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movementInput;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovementInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        movementInput = new Vector3(x, 0f, z).normalized;
    }

    private void MovePlayer()
    {
        if (movementInput.magnitude > 0)
        {
            Vector3 targetVelocity = movementInput * moveSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }
}
=======
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public float moveSpeed = 4f;
    public float turnSpeed = 3f;
    private float currentMoveInputX;
    private float currentMoveInputZ;
    private Animator animator;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //movement
        Vector3 moveDir = new Vector3(currentMoveInputX, 0f, currentMoveInputZ);
        rb.linearVelocity = new Vector3(currentMoveInputX, rb.linearVelocity.y, currentMoveInputZ);
        //rotate character in same direction as moving
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, turnSpeed * Time.deltaTime );
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        currentMoveInputX = value.ReadValue<Vector2>().x;
        currentMoveInputZ = value.ReadValue<Vector2>().y;
       
        if (currentMoveInputX != 0)
        {
            animator.SetFloat("Moving", Mathf.Abs(currentMoveInputX));
        } else
        {
            animator.SetFloat("Moving", Mathf.Abs(currentMoveInputZ));

        }

    }
}
>>>>>>> be2f895566d875686654aedfe29da65e9cae5e18
