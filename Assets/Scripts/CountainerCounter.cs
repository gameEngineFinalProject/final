using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class CountainerCounter : BaseCounter
{
    /*public void Start()
    {
        correctIcon.SetActive(false);
        wrongIcon.SetActive(false);
    }*/

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ScoreManager scoreManager; // 拖入 ScoreManager 物件

    [SerializeField] private GameObject correctIcon;
    [SerializeField] private GameObject wrongIcon;

    public void Awake()
    {
        // 自動抓子物件中名為 CorrectIcon / WrongIcon 的 GameObject
        //correctIcon = transform.Find("CorrectIcon")?.gameObject;
        //wrongIcon = transform.Find("WrongIcon")?.gameObject;

        if (correctIcon != null) correctIcon.SetActive(false);
        if (wrongIcon != null) wrongIcon.SetActive(false);
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;

        KitchenObject playerObject = player.GetKitchenObject();
        KitchenObjectSO playerObjectSO = playerObject.GetKitchenObjectSO();

        if (playerObjectSO == kitchenObjectSO)
        {
            // 丟入正確，破壞物件，加分
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Debug.Log("Correct");
            GameObject.Destroy(playerObject.gameObject);
            player.ClearKichenObject();
            scoreManager.AddScore(1);

            StartCoroutine(ShowIcon(correctIcon));
        }
        else
        {
            // 丟錯物品，不處理或提示錯誤
            Debug.Log("錯誤物品，無法投入！");
            StartCoroutine(ShowIcon(wrongIcon));
        }
        /*if (player.HasKitchenObject())
        {
            // 玩家手上有東西，將其銷毀
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Destroy(player.GetKitchenObject().gameObject);
            player.ClearKichenObject();
        }
        */

    }
    private IEnumerator ShowIcon(GameObject icon)
    {
        if (icon == null) yield break;

        icon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        icon.SetActive(false);
    }

   

}
