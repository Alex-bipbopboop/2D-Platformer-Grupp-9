using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyLayer;
    private int enemyLayerFlying;
    private GameObject[] enemies;
    private GameObject[] enemiesFlying;
    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        enemyLayerFlying = LayerMask.NameToLayer("EnemyFlying");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesFlying = GameObject.FindGameObjectsWithTag("EnemyFlying");
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

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().didRespawn = false;
    }
}

