using Enums;
using UnityEngine;

public class Resource: Interactable
{
        public ResourceType resourceType;
        public int resourceAmount;
        [SerializeField] private InfoPlate infoPlate;
        [SerializeField] private InfoPlate collectedSign;

        #region MonoBehaviour

        public void CollectResource()
        {
                Destroy(gameObject, 1f);
        }
        

        #endregion

}