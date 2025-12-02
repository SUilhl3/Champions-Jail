using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public float moveSpeed = 4f;
    public float turnSpeed = 3f;
    public float playerDamage = 25f;
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

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("Attacking");
            ChestMonster monster = collision.collider.GetComponent<ChestMonster>();
            monster.TakeDamage(playerDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Attacking");
            ChestMonster monster = other.GetComponent<ChestMonster>();
            monster.TakeDamage(playerDamage);
        }
    }
}
