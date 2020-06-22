using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public int spawnLimit;
    public float spawnTimer = 5f;
    public float spawnRadius = 10f;

    private List<Enemy> enemies;
    private bool isSpawning;

    private void Start()
    {
        enemies = new List<Enemy>();
    }
    private void Update()
    {
        if (enemies.Count < spawnLimit && !isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnTimer);
        Enemy newEnemy = Instantiate(enemy.GetComponent<Enemy>(), transform);
        enemy.transform.position = Random.insideUnitSphere * spawnRadius;
        enemy.transform.position = new Vector3(enemy.transform.position.x, 2f, enemy.transform.position.z);
        enemies.Add(newEnemy);
        isSpawning = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
