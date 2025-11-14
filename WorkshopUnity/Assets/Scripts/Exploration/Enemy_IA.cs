using UnityEngine;
using UnityEngine.SceneManagement;

namespace WU.Exploration
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Comportement de l'ennemi")]
        public float speed = 3f;              // Vitesse patrouille
        public float chaseSpeed = 6f;         // Vitesse poursuite
        public float patrolRadius = 5f;       // Rayon de patrouille
        public float detectionRange = 8f;     // Distance de détection du joueur
        public float changePatrolTime = 3f;   // Temps avant changement de direction

        private Transform player;
        private Vector3 patrolTarget;
        private float patrolTimer;
        private Rigidbody rb;
    
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            // Initialise un premier point
            NewPatrolPoint();
        }

        void FixedUpdate()
        {
            if (!player) return;
        
            float dist = Vector3.Distance(transform.position, player.position);
        
            Vector3 dir = (dist <= detectionRange ? player.position : patrolTarget) - transform.position;
            float moveSpeed = dist <= detectionRange ? chaseSpeed : speed;

            // Déplacement
            rb.MovePosition(transform.position + dir.normalized * (moveSpeed * Time.fixedDeltaTime));
        
            if (dist > detectionRange)
            {
                patrolTimer -= Time.fixedDeltaTime;

                if (patrolTimer <= 0 || Vector3.Distance(transform.position, patrolTarget) < 0.5f)
                    NewPatrolPoint();
            }

            // Regarde le joueur uniquement si détecté
            if (dist <= detectionRange)
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }

        // Nouveau point 
        void NewPatrolPoint()
        {
            Vector2 rnd = Random.insideUnitCircle * patrolRadius;
            patrolTarget = transform.position + new Vector3(rnd.x, 0, rnd.y);
            patrolTimer = changePatrolTime;
        }

        // Collision avec le joueur
        void OnCollisionEnter(Collision c)
        {
            if (c.collider.CompareTag("Player"))
            {
                Debug.Log("mob à touché coulé");
                SceneManager.LoadScene("FightScene");
            }
        }
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
}
