using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParents
{
    public Transform GetKitchenObjectFollowTransform();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();
    public void ClearKichenObject();
    public bool HasKitchenObject();


}
