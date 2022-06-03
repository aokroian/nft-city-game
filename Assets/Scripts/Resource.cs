using Enums;
using UnityEngine;

public class Resource: Interactable
{
        public ResourceType resourceType;
        [SerializeField] private InfoPlate infoPlate;
        [SerializeField] private GameObject collectedSign;

        #region MonoBehaviour

        public void RenderInfoPlate()
        {
        }

        public void RenderCollectedSign()
        {
                
        }

        #endregion

}