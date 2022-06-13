using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Traffic
{
    public class CarController : MonoBehaviour
    {
        public NavMeshAgent agent;
        public float speed = 10f;

        public Waypoint currentWaypoint;

        private void Awake()
        {
            agent ??= GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }

        private void Start()
        {
            agent.SetDestination(currentWaypoint.transform.position);
        }

        private void FixedUpdate()
        {
            if (!(Vector3.Distance(transform.position, agent.destination) <= 1f)) return;
            if (currentWaypoint == null)
            {
                Destroy(gameObject);
            }
            var isBranch = currentWaypoint is {branches: {Count: > 0}};
            var next = isBranch
                ? currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)]
                : currentWaypoint.nextWaypoint;

            currentWaypoint = next;
            agent.SetDestination(currentWaypoint.transform.position);
        }
    }
}