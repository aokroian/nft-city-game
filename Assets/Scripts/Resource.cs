using Enums;
using Events;
using UnityEngine;

public class Resource : Interactable
{
    public ResourceType resourceType;
    public int resourceAmount;
    public ServerApiProvider apiProvider;
    [SerializeField] private InfoPlate infoPlate;
    [SerializeField] private InfoPlate collectedSign;

    #region MonoBehaviour

    public void CollectResource()
    {
        Debug.Log("collecting");
        apiProvider.concreteApi.CollectResource(resourceType, CollectingResult, err =>
        {
            Debug.Log("Collecting resource error");
            // TODO: Err!
        });
    }

    public void CollectingResult(bool result)
    {
        if (result)
        {
            Destroy(gameObject, 1f);
        }
        else
        {
            Debug.Log("Can't collect");
            // TODO: Message "can't collect"
        }
    }

    #endregion

}