using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class NewResourceSpawner : MonoBehaviour
{
    [Serializable]
    public struct ResourceKeyValue
    {
        public ResourceType type;
        public GameObject prefab;
    }

    public Transform containerForSpawnedObjects;
    public List<ResourceKeyValue> resourcePrefabs;

    public LayerMask layersToCheckBeforeSpawn;
    public Vector2 xAxisBounds;
    public Vector2 zAxisBounds;

    public UnityEvent<GameObject> onSpawned;

    public ServerApiProvider apiProvider;

    public void Spawn(FullSaveDto fullData)
    {
        Spawn(fullData.mapResources);
    }

    public void Spawn(Dictionary<ResourceType, int> resources)
    {
        Debug.Log("Spawning " + resources);
        foreach (var res in resources)
        {
            for (int i = 0; i < res.Value; i ++)
            {
                while (!SpawnSingle(res.Key, resourcePrefabs.Find(it => it.type == res.Key).prefab))
                {
                    // NOTHING
                }
            }
        }
    }

    // TODO: Rewrite without useless instantiations!!1
    public bool SpawnSingle(ResourceType type, GameObject prefab)
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
            spawnedCopy.GetComponent<Resource>().apiProvider = apiProvider;
            onSpawned.Invoke(spawnedCopy);
            return true;
        }
        else
        {
            Destroy(spawnedCopy);
            return false;
        }
    }

    protected virtual bool ValidateSpawnPosition(Vector3 spawnPosition, float radius, LayerMask layersToCheck)
    {
        var isAllowed = false;

        var nearbyObjects = new Collider[20];
        var fakeCollider = Physics.OverlapSphereNonAlloc(spawnPosition, radius, nearbyObjects, layersToCheck);

        isAllowed = nearbyObjects.ToList().Find(elem => elem != null) == null;
        Debug.Log(isAllowed + " " + spawnPosition + " " + radius + " " + nearbyObjects.ToList().Find(elem => elem != null));
        return isAllowed;
    }
}
