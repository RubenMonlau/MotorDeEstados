﻿using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    //Crea e inicializa las variables de estados, la luz asociada al enemigo, la vida del enemigo,
    //la cadencia de disparo, el daño que hace el enemigo, las coordendas de disparo,
    //el objeto skull para cuando el enemigo muere, una variable booleana isDead que controla si
    //el enemigo está vivo o no, y los waypoints en forma de array que marcan el camino del estado Patrol.
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IenemyState currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    //public float life = 100;
    public float timeBetweenShoots = 1.0f;
    //public float damageForce = 10f;
    public float rotationTime = 3.0f;
    //public float shootHeight = 0.5f;
    public TextMeshProUGUI enemyName;
    public Transform[] waypoints;
    public Material enemyColor { get; set; }

    public Transform player; // Reference to the player
    public Transform fleePoint;
    private bool isFleeing = false;
    public float fleeSpeed = 4f;
    public float fleeDistance = 20f; // Distance to flee

    // Inicializa los diferentes estados e indica que ha de ignorar el Raycast para que no hagan agujeros las balas
    // en el collider de detección
    void Start()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        currentState = patrolState;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyColor = GetComponent<Renderer>().material;
        enemyName.text = "Javi";
    }

    // Comprueba constantemente el cambio de estado y si el enemigo está vivo o no
    void Update()
    {
        currentState.UpdateState();
    }

    public void StartFleeing()
    {
        if (player != null)
        {
            Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
            Vector3 fleePosition = transform.position + directionAwayFromPlayer * fleeDistance;

            fleePoint.position = fleePosition; // Move fleePoint to the calculated position
            isFleeing = true;
        }
    }

    //Comprueban las colisiones de los colliders. Dependiendo de ellos, el enemigo pasa a un estado u otro del motor de estados.
    void OnTriggerEnter(Collider col)
    {
        currentState.OnTriggerEnter(col);
    }
    void OnTriggerStay(Collider col)
    {
        currentState.OnTriggerStay(col);
    }
    void OnTriggerExit(Collider col)
    {
        currentState.OnTriggerExit(col);
    }
}
