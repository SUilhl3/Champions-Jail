using UnityEngine;
using UnityEngine.Rendering;

public class ChestMonster : MonoBehaviour
{
    StateMachine stateMachine;
    public GameObject player;
    public float distanceToChase = 10;
    public bool InFront;
    public float distanceToAttack = 2;
    public float distanceToEvade = 5;
    public float FOV = 60;
    public float patrolSpeed = 1;
    public float chaseSpeed = 2;
    public float evadeSpeed = 3;
    public float attackPower = 5f;
    public Vector3[] patrolPoints;
    public int currentPatrol;

    public float csCosFOV_2;
    public bool isEvading = false;
    public float health = 100f;
    public float criticalHealth = 25f;
    public Animator animator;


    void Start()
    {
        stateMachine = new StateMachine();
        csCosFOV_2 = Mathf.Cos(FOV * 0.5f * Mathf.Deg2Rad);


        //Patrolling state 
        var patrol = stateMachine.CreateState("Patrol");
        patrol.onEnter = delegate
        {
            
        };
        patrol.onStay = delegate
        {
            HandlePatrol();
        };

        patrol.onExit = delegate
        {
            
        };

        //Chasing state 
        var chase = stateMachine.CreateState("Chase");
        chase.onEnter = delegate
        {
          
        };
        chase.onStay = delegate
        {
            HandleChase();
        };
        chase.onExit = delegate
        {
           
        };

        //Evading state
        var evade = stateMachine.CreateState("Evade");
        evade.onEnter = delegate
        {

        };
        evade.onStay = delegate
        {
            HandleEvade();
        };
        evade.onExit = delegate
        {

        };

        //Attacking state
        var attack = stateMachine.CreateState("Attack");
        attack.onEnter = delegate
        {
            
        };
        attack.onStay = delegate
        {
            HandleAttack();
        };
        attack.onExit = delegate
        {

        };

        //Death state
        var death = stateMachine.CreateState("Death");
        death.onEnter = delegate
        {
            animator.SetBool("IsDead", true);
        };
        death.onStay = delegate
        {

        };
        death.onExit = delegate
        {

        };

    }

    void HandlePatrol()
    {
        Vector3 playerHeading = player.transform.position - this.transform.position;
        float distanceToPlayer = playerHeading.magnitude;
        Vector3 directionToPlayer = playerHeading.normalized;
        float csFwd2Ply = Vector3.Dot(this.transform.forward, directionToPlayer);
        Vector3 targetPoint = patrolPoints[currentPatrol];
        Vector3 direction = (targetPoint - transform.position).normalized;

        //so character rotates towards where they are moving
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        transform.position += direction * patrolSpeed * Time.deltaTime;

        float distanceToTargetPoint = Vector3.Distance(transform.position, targetPoint);
        if (distanceToTargetPoint < 0.1f)
        {
            if (currentPatrol == patrolPoints.Length - 1)
            {
                currentPatrol = 0;
            }
            else
            {
                currentPatrol++;
            }
        }

        //transition to chase
        InFront = (Vector3.Dot(this.transform.forward, directionToPlayer) >= csCosFOV_2);
        if (InFront)
        {
            if (distanceToPlayer <= distanceToChase && !isEvading)
            {
                animator.SetBool("IsChasing", true);
                stateMachine.TransitionTo("Chase");
            } else if (distanceToPlayer <= distanceToEvade && isEvading)
            {
                animator.SetBool("IsChasing", true);
                stateMachine.TransitionTo("Evade");
            }
        }

    }

    void HandleChase()
    {
        Vector3 E = this.transform.position;
        Vector3 P = player.transform.position;
        Vector3 Heading = P - E;
        Vector3 HeadingDir = Heading.normalized;
        this.transform.position += HeadingDir * chaseSpeed * Time.deltaTime;

        //so character rotates towards where they are moving
        if (HeadingDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(HeadingDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        Vector3 playerHeading = player.transform.position - this.transform.position;
        float distanceToPlayer = playerHeading.magnitude;
        Vector3 directionToPlayer = playerHeading.normalized;

        InFront = (Vector3.Dot(this.transform.forward, directionToPlayer) >= csCosFOV_2);

        if (InFront && distanceToPlayer <= distanceToAttack)
        {
            animator.SetBool("IsAttacking", true);
            stateMachine.TransitionTo("Attack");
        }

        if (distanceToPlayer >= distanceToChase)
        {
            animator.SetBool("IsChasing", false);
            stateMachine.TransitionTo("Patrol");
        }
    }

    void HandleEvade()
    {
        Vector3 E = this.transform.position;
        Vector3 P = player.transform.position;
        Vector3 Heading = E - P;
        Heading.y = 0;
        Vector3 HeadingDir = Heading.normalized; 
        if(HeadingDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(HeadingDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        this.transform.position += HeadingDir * evadeSpeed * Time.deltaTime;

        Vector3 playerHeading = player.transform.position - this.transform.position;
        float distanceToPlayer = playerHeading.magnitude;

        if (distanceToPlayer > distanceToEvade)
        {
            animator.SetBool("IsChasing", false);
            stateMachine.TransitionTo("Patrol");
        }
    }

    void HandleAttack()
    {
        Vector3 playerHeading = player.transform.position - this.transform.position;
        float distanceToPlayer = playerHeading.magnitude;
        Vector3 directionToPlayer = playerHeading.normalized;

        if (distanceToPlayer >= distanceToAttack)
        {
            animator.SetBool("IsAttacking", false);
            stateMachine.TransitionTo("Chase");
        }

        if (health <= criticalHealth)
        {
            isEvading = true;
            animator.SetBool("IsAttacking", false);
            stateMachine.TransitionTo("Evade");
        }

        if (health <= 0)
        {
            stateMachine.TransitionTo("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Damaging Player");
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.TakeDamage(attackPower);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= criticalHealth)
        {
            isEvading = true;
            animator.SetBool("IsAttacking", false);
            stateMachine.TransitionTo("Evade");
        }

        if (health <= 0)
        {
            stateMachine.TransitionTo("Death");
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
