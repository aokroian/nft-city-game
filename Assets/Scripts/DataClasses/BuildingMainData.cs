using Enums;
using UnityEngine;

namespace DataClasses
{
    public class BuildingMainData: MonoBehaviour
    {
        public int id;
        public HouseStatus status;
        public int tier;
        public float timer;
        public bool isBought;
    }
}