using Enums;
using UnityEngine;

public class Resource: Interactable
{
        public ResourceType resourceType;
        public int resourceAmount;
        [SerializeField] private InfoPlate infoPlate;
        [SerializeField] private InfoPlate collectedSign;

        #region MonoBehaviour

        private void Awake()
        {
                RenderInfoPlate();
        }

        public void RenderInfoPlate()
        {
                infoPlate.SetText($"{resourceAmount} {resourceType}");
        }
        

        public void CollectResource()
        {
                Destroy(gameObject, 1f);
        }
        

        #endregion

}