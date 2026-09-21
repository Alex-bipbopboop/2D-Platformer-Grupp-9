using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyLayer;
    private GameObject[] enemies;
    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void EnemyRespawn()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<SpriteRenderer>().enabled = true;
            enemy.GetComponent<AudioSource>().enabled = true;
            enemy.layer = enemyLayer;
            print("enemyrespawn");
        }
        
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().didRespawn = false;
    }
}

