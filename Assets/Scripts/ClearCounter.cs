using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField]private KitchenObjectSO kitchenObjectSO;
  
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchenObject there
            if (player.HasKitchenObject())
            {
                //if player carring somthing
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player not carry anthing

            }
        }
        else
        {
            //There is a KitchenObject there
            if (player.HasKitchenObject())
            {
                //Player is carry something
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
      



}
