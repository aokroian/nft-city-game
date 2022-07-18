using System.Collections.Generic;
using UnityEngine;

namespace Car_Traffic
{
    public class Waypoint : MonoBehaviour
    {
        public Waypoint previousWaypoint;
        public Waypoint nextWaypoint;

        public List<Waypoint> branches = new();


        [Range(0f, 1f)] public float branchRatio = 0.5f;
    }
}