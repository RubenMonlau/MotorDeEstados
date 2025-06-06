﻿using UnityEngine;

public class AttackState : IenemyState
{
    //Inicializa el estado Ataque. Crea objetos para las coordenadas del disparo enemigo,
    //un offset aplicado dependiendo del enemigo que dispare y un objeto de tipo disparo
    //llamado ball, que es una pelota de goma.
    EnemyAI myEnemy;
    float actualTimeBetweenShoots = 0;
    public Vector3 enemyPosition;
    public Quaternion enemyRotation;
    public Transform enemytransform;
    public GameObject ball;
    public float offset_y = 0;

    public float bulletSpeed = 10f;
    public string enemyTag = "Player";


    public AttackState(EnemyAI enemy)
    {
        myEnemy = enemy;
        ball = (GameObject)Resources.Load("Ball", typeof(GameObject));
    }

    //Coloca la luz del enemigo a rojo. Gestiona la cadencia del disparo del enemigo.
    public void UpdateState()
    {
        myEnemy.enemyColor.SetColor("_Color", Color.red);
        actualTimeBetweenShoots += Time.deltaTime;
        if(actualTimeBetweenShoots > 5)
        {
            GoToAlertState();
        }
    }

    //No hace nada, ya que ya está en el modo de ataque.
    public void GoToAttackState() { }

    //No hace nada porque primero a de pasar por el estado de alerta.
    public void GoToPatrolState() { }

    //Si no encuentra al jugador, pasa a estado alerta.
    public void GoToAlertState() {
        myEnemy.currentState = myEnemy.alertState;
    }

    //Si entra en el collider, no hace nada, ya que ya se encuentra atacando a alguien.
    public void OnTriggerEnter(Collider col)
    {
        Shoot(col);
    }

    //Si sigue dentro de la zona del collider, comprueba si tiene cadencia de disparo. Si la tiene,
    //realiza los cálculos para dispararle y le lanza el raycast. Si le impacta al jugador, dependiendo
    //del tipo de enemigo, lanzará un tipo de bola u otro hacia el jugador, lo genera y le aplica velocidad.
    public void OnTriggerStay(Collider col)
    {
        Shoot(col);
    }

    //Si ha salido de la zona del collider, el enemigo pasa a modo Alerta.
    public void OnTriggerExit(Collider col) {
        GoToAlertState();
    }

    public void Shoot(Collider col)
    {
        if (actualTimeBetweenShoots > myEnemy.timeBetweenShoots)
        {
            actualTimeBetweenShoots = 0;
            Vector3 lookDirection = col.transform.position - myEnemy.transform.position;
            myEnemy.transform.rotation = Quaternion.FromToRotation(Vector3.forward, lookDirection);

            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy == null) return; // No enemies to shoot at

            // Spawn the bullet at the player's position
            enemyPosition = myEnemy.transform.position;
            enemyRotation = col.transform.rotation;
            enemytransform = col.transform;
            GameObject bullet = GameObject.Instantiate(ball, new Vector3(enemyPosition.x, enemyPosition.y + offset_y, enemyPosition.z), enemyRotation);

            bullet.AddComponent<EnemyBullet>();

            // Get Rigidbody and set the bullet's velocity to move towards the enemy
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = (closestEnemy.position - myEnemy.transform.position).normalized; // Direction towards the enemy
            rb.velocity = direction * bulletSpeed;
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = myEnemy.transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }

}
