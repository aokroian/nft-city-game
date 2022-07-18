using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MonoBehaviours
{
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
            StartCoroutine(Spawn(fullData.mapResources));
        }

        public IEnumerator Spawn(Dictionary<ResourceType, int> resources)
        {
            var resourcesToSpawn = getUnspawnedResourcesCount(resources.ToDictionary(e => e.Key, e => e.Value));
            foreach (var res in resourcesToSpawn)
            {
                for (int i = 0; i < res.Value; i ++)
                {
                    while (!SpawnSingle(res.Key, resourcePrefabs.Find(it => it.type == res.Key).prefab))
                    {
                        // NOTHING
                    }
                    yield return null;
                }
            }
        }

        private Dictionary<ResourceType, int> getUnspawnedResourcesCount(Dictionary<ResourceType, int> countShouldBe)
        {
            foreach (var item in containerForSpawnedObjects.GetComponentsInChildren<Resource>(false))
            {
                countShouldBe[item.itemSettings.resourceType]--;
            }
            return countShouldBe;
        }

        private bool SpawnSingle(ResourceType type, GameObject prefab)
        {
            var randX = Random.Range(xAxisBounds.x, xAxisBounds.y);
            var randZ = Random.Range(zAxisBounds.x, zAxisBounds.y);
            var randomPosition = new Vector3(randX, 0, randZ);


            // check spawn position
            var boxCollider = prefab.GetComponent<BoxCollider>();
            var colliderSize = Math.Max(boxCollider.size.x, boxCollider.size.y);

            if (ValidateSpawnPosition(randomPosition, colliderSize, layersToCheckBeforeSpawn))
            {
                var spawnedCopy = Instantiate(prefab, Vector3.up * 1000, Quaternion.identity,
                    containerForSpawnedObjects);
                spawnedCopy.transform.position = randomPosition;
                spawnedCopy.name = spawnedCopy.name.Replace("(Clone)", "");
                spawnedCopy.GetComponent<Resource>().apiProvider = apiProvider;
                onSpawned.Invoke(spawnedCopy);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual bool ValidateSpawnPosition(Vector3 spawnPosition, float radius, LayerMask layersToCheck)
        {
            //var isAllowed = false;

            var nearbyObjects = new Collider[20];
            var fakeCollider = Physics.OverlapSphereNonAlloc(spawnPosition, radius, nearbyObjects, layersToCheck);

            //isAllowed = nearbyObjects.ToList().Find(elem => elem != null) == null;
            //Debug.Log(isAllowed + " " + spawnPosition + " " + radius + " " + nearbyObjects.ToList().Find(elem => elem != null));
            return fakeCollider == 0;
        }
    }
}
