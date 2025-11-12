using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Comportement de l'ennemi")]
    public float speed = 3f;              // Vitesse patrouille
    public float chaseSpeed = 6f;         // Vitesse poursuite
    public float patrolRadius = 5f;       // Rayon de patrouille
    public float detectionRange = 8f;     // detection du mob
    public float changePatrolTime = 3f;   // changement direction quand balade
    
    Transform player;
    Vector3 patrolTarget;
    float patrolTimer;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialise un premier point de patrouille
        NewPatrolPoint();
    }

    void FixedUpdate()
    {
        if (!player) return;
        
        float dist = Vector3.Distance(transform.position, player.position);
        
        Vector3 dir = (dist <= detectionRange ? player.position : patrolTarget) - transform.position;

        // Change vitesse en fonction du mode
        float moveSpeed = dist <= detectionRange ? chaseSpeed : speed;

        // Déplacement
        rb.MovePosition(transform.position + dir.normalized * moveSpeed * Time.fixedDeltaTime);

        // Patrouille si joueur trop loin
        if (dist > detectionRange)
        {
            patrolTimer -= Time.fixedDeltaTime;

            // Nouveau point 
            if (patrolTimer <= 0 || Vector3.Distance(transform.position, patrolTarget) < 0.5f)
                NewPatrolPoint();
        }

        // Orientation mob sur horizontalr
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    // Balade du mob
    void NewPatrolPoint()
    {
        Vector2 rnd = Random.insideUnitCircle * patrolRadius;  // Point aléatoire dans cercle
        patrolTarget = transform.position + new Vector3(rnd.x, 0, rnd.y);
        patrolTimer = changePatrolTime;                        // Réinitialise timer
    }

    // Collision joueur pour plus tard
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Player"))
            Debug.Log(" Mob a touché coulé !");
    }

    // Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
