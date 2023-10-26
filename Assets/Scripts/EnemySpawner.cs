using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    int spawningAmount = 4;

    [SerializeField]
    float spawningRange = 100.0f;

    [SerializeField]
    float spawningSafeRange = 50.0f;

    List<GameObject> spawningEnemies = new List<GameObject>();

    Vector3 spawningPoint;


    private void Start()
    {

        for (int i = 0; i < spawningAmount; i++)
        {
            GetSpawningPoint();
            while (Vector3.Distance(spawningPoint, Vector3.zero) < spawningSafeRange)
            {
                GetSpawningPoint();
            }

            GameObject spawningObject = Instantiate(enemyPrefab, spawningPoint, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));

            spawningObject.transform.parent = this.transform;
            spawningEnemies.Add(spawningObject);
        }
    }

    void GetSpawningPoint()
    {
        spawningPoint = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        if (spawningPoint.magnitude > 1.0f)
        {
            spawningPoint.Normalize();
        }

        spawningPoint *= spawningRange;
    }
}
