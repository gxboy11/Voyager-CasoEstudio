using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject spawningObjectPrefab;

    [SerializeField]
    int spawningAmount = 500;

    [SerializeField]
    float spawningRange = 20.0f;

    [SerializeField]
    float spawningSafeRange = 10.0f;

    List<GameObject> spawningObjects = new List<GameObject>();

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

            GameObject spawningObject = Instantiate(spawningObjectPrefab, spawningPoint, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));

            spawningObject.transform.parent = this.transform;
            spawningObjects.Add(spawningObject);
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
