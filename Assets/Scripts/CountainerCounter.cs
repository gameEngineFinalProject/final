using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ScoreManager scoreManager; // ��J ScoreManager ����

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;

        KitchenObject playerObject = player.GetKitchenObject();
        KitchenObjectSO playerObjectSO = playerObject.GetKitchenObjectSO();

        if (playerObjectSO == kitchenObjectSO)
        {
            // ��J���T�A�}�a����A�[��
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Debug.Log("Correct");
            GameObject.Destroy(playerObject.gameObject);
            player.ClearKichenObject();
            scoreManager.AddScore(1);
            

        }
        else
        {
            // ������~�A���B�z�δ��ܿ��~
            Debug.Log("���~���~�A�L�k��J�I");
        }
        /*if (player.HasKitchenObject())
        {
            // ���a��W���F��A�N��P��
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Destroy(player.GetKitchenObject().gameObject);
            player.ClearKichenObject();
        }
        */

    }


}
