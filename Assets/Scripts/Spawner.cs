using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public int leftToSpawnCount = 18;
    public Transform containerForSpawnedObjects;
    public GameObject spawnPrefab;
    public List<GameObject> randomSpawnPrefabs;
    [Range(0, 3000)] public float minSpawnWait;
    [Range(0, 3000)] public float maxSpawnWait;

    public LayerMask layersToCheckBeforeSpawn;
    public Vector2 xAxisBounds;
    public Vector2 zAxisBounds;

    public UnityEvent<GameObject> onSpawned;

    private Coroutine _spawningCoroutine;

    #region MonoBehaviour

    private void Awake()
    {
        randomSpawnPrefabs ??= new List<GameObject>();
    }

    private void Update()
    {
        if (leftToSpawnCount <= 0 || _spawningCoroutine != null) return;
        StopAllCoroutines();
        _spawningCoroutine = StartCoroutine(SpawningCoroutine());
    }

    public void SetLeftToSpawnCount(int count)
    {
        leftToSpawnCount = count;
    }

    private IEnumerator SpawningCoroutine()
    {
        while (leftToSpawnCount > 0)
        {
            var spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
            if (maxSpawnWait <= minSpawnWait)
            {
                spawnWait = minSpawnWait;
            }

            yield return new WaitForSeconds(spawnWait);

            var prefab = spawnPrefab;
            if (randomSpawnPrefabs.Count > 0)
            {
                prefab = randomSpawnPrefabs[Random.Range(0, randomSpawnPrefabs.Count)];
            }

            if (prefab != null)
            {
                var randX = Random.Range(xAxisBounds.x, xAxisBounds.y);
                var randZ = Random.Range(zAxisBounds.x, zAxisBounds.y);
                var randomPosition = new Vector3(randX, 0, randZ);

                var spawnedCopy = Instantiate(prefab, Vector3.up * 1000, Quaternion.identity,
                    containerForSpawnedObjects);

                // check spawn position
                var boxCollider = spawnedCopy.GetComponent<BoxCollider>();
                var bounds = boxCollider.bounds;
                var colliderSize = Math.Max(bounds.size.x, bounds.size.y);

                if (ValidateSpawnPosition(randomPosition, colliderSize, layersToCheckBeforeSpawn))
                {
                    spawnedCopy.transform.position = randomPosition;
                    spawnedCopy.name = spawnedCopy.name.Replace("(Clone)", "");
                    onSpawned.Invoke(spawnedCopy);
                    leftToSpawnCount--;
                    if (leftToSpawnCount <= 0)
                    {
                        _spawningCoroutine = null;
                        yield break;
                    }
                }
                else
                {
                    Destroy(spawnedCopy);
                }
            }
        }
    }

    protected virtual bool ValidateSpawnPosition(Vector3 spawnPosition, float radius, LayerMask layersToCheck)
    {
        var isAllowed = false;

        var nearbyObjects = new Collider[20];
        var fakeCollider = Physics.OverlapSphereNonAlloc(spawnPosition, radius, nearbyObjects, layersToCheck);

        isAllowed = nearbyObjects.ToList().Find(elem => elem != null) == null;
        return isAllowed;
    }

    #endregion
}