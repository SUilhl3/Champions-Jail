using UnityEngine;
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
        rb.linearVelocity = new Vector3(currentMoveInputX * moveSpeed, rb.linearVelocity.y, currentMoveInputZ * moveSpeed);
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

    public void OnAttack(InputAction.CallbackContext value)
    {
        animator.SetTrigger("Attacking");
        Debug.Log("Attacking");
    }
}
