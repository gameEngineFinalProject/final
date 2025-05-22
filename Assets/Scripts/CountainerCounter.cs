using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            // Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
        }


    }


}
