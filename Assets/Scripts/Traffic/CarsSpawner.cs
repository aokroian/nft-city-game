using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Traffic
{
    public class CarsSpawner : MonoBehaviour
    {
        public bool beginSpawnOnStart = true;
        public List<Transform> spawnPoints;
        public int spawnAtEachSpawnPoint;

        public List<CarController> carVariants;

        private void Awake()
        {
            spawnPoints ??= new List<Transform>();
        }

        private void Start()
        {
            if (beginSpawnOnStart) StartSpawnAll();
        }

        public void StartSpawnAll()
        {
            StartCoroutine(SpawnAllCarsCoroutine());
        }

        private IEnumerator SpawnAllCarsCoroutine()
        {
            var needToSpawn = spawnAtEachSpawnPoint * spawnPoints.Count;
            while (needToSpawn > 0)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    var randomCar = carVariants[Random.Range(0, carVariants.Count)];
                    var car = Instantiate(randomCar, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
                    var controller = car.GetComponent<CarController>();
                    controller.currentWaypoint = spawnPoint.GetComponent<Waypoint>();
                    needToSpawn--;
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}