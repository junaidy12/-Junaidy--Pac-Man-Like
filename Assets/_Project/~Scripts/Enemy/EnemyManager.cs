using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] float spawnTime;
    WaypointManager waypointManager;

    float spawnedTime;
    private void Awake()
    {
        waypointManager = GetComponent<WaypointManager>();
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        SpawnEnemy();
    }
    private void Update()
    {
        if(spawnTime + spawnedTime <= Time.time)
        {
            spawnedTime = Time.time;
            //Spawn Enemy
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int rndWP = Random.Range(0,waypointManager.WaypointDataList.Count - 1);
        int rndE = Random.Range(0, enemies.Length);
        if (enemies[rndE].gameObject.activeSelf)
        {
            SpawnEnemy();
            return;
        }
        Transform spawnPos = waypointManager.WaypointDataList[rndWP].GetTransform();
        enemies[rndE].transform.position = spawnPos.position - spawnPos.position.y * Vector3.up;
        enemies[rndE].gameObject.SetActive(true);
    }
}

