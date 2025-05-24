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
        if (player.HasKitchenObject())
        {
            // 玩家手上有東西，將其銷毀
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Destroy(player.GetKitchenObject().gameObject);
            player.ClearKichenObject();
        }


    }


}
