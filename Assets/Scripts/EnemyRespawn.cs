using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyLayer;
    private int enemyLayerFlying;
    private int pickupLayer;
    private GameObject[] enemies;
    private GameObject[] enemiesFlying;
    private GameObject[] pickups;
    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        enemyLayerFlying = LayerMask.NameToLayer("EnemyFlying");
        pickupLayer = LayerMask.NameToLayer("Default");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesFlying = GameObject.FindGameObjectsWithTag("EnemyFlying");
        pickups = GameObject.FindGameObjectsWithTag("Pickup");
    }

    public void EnemyRespawn()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.layer = enemyLayer;
            enemy.GetComponent<SpriteRenderer>().enabled = true;
            enemy.GetComponent<AudioSource>().enabled = true;
            enemy.transform.position = enemy.GetComponent<EnemyMovement>().spawnPosition;
        }

        foreach (GameObject enemy in enemiesFlying)
        {
            enemy.layer = enemyLayerFlying;
            enemy.GetComponent<SpriteRenderer>().enabled = true;
            enemy.GetComponent<AudioSource>().enabled = true;
            enemy.transform.position = enemy.GetComponent<EnemyMovementFlying>().spawnPosition;
        }

        foreach (GameObject pickup in pickups)
        {
            pickup.layer = enemyLayerFlying;
            pickup.GetComponent<SpriteRenderer>().enabled = true;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().didRespawn = false;
    }
}

