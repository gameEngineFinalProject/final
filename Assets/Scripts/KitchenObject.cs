using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO KitchenObjectSO;

    private IKitchenObjectParents kichenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParents kitchenObjectParent)
    {
        if (this.kichenObjectParent != null)
        {
            this.kichenObjectParent.ClearKichenObject();
        }
        this.kichenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("kichenObjectParent already has a KitchenObject!");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParents GetKitchenObjectParents()
    {
        return kichenObjectParent;
    }
   
}
