using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCounter :BaseCounter
{
    [SerializeField] private KitchenObjectSO[] spawnObjectSOArray;
    [SerializeField] private KitchenObjectSO trashBagSO;


    private bool isReadyToSpawn = false;
    private void Start()
    {
       
    }

    public override void Interact(Player player)
    {
        ClearCounter[] clearCounters = FindObjectsOfType<ClearCounter>();
        
        if (!isReadyToSpawn)
        {
            // 確認所有 ClearCounter 是否都是空的
            bool allClearEmpty = true;
            foreach (ClearCounter counter in clearCounters)
            {
                if (counter.HasKitchenObject())
                {
                    allClearEmpty = false;
                    break;
                }
            }
         
            if (allClearEmpty && !HasKitchenObject())
            {
                // 所有清除櫃台為空，而且目前沒垃圾袋 → 生成垃圾袋
                Transform trashBagTransform = Instantiate(trashBagSO.prefab);
                KitchenObject kitchenObject = trashBagTransform.GetComponent<KitchenObject>();

                if (kitchenObject == null)
                {
                    Debug.LogError("TrashBag prefab 上沒有掛 KitchenObject 腳本！");
                    return;
                }

                kitchenObject.SetKitchenObjectParent(this);
                //Debug.Log("垃圾袋成功生成並設為 ButtonCounter 子物件");
                


                isReadyToSpawn = true;
            }
        }
        else
        {
            // 第二次按下 → 清掉垃圾袋並在 ClearCounter 上隨機生成物品
            if (HasKitchenObject())
            {
                Destroy(GetKitchenObject().gameObject);
                ClearKichenObject();
            }

            int spawnCount = Random.Range(4, Mathf.Min(5, clearCounters.Length + 1));
            List<int> spawnedIndex = new List<int>();

            while (spawnedIndex.Count < spawnCount)
            {
                int randomIndex = Random.Range(0, clearCounters.Length);
                if (!spawnedIndex.Contains(randomIndex) && !clearCounters[randomIndex].HasKitchenObject())
                {
                    spawnedIndex.Add(randomIndex);
                    KitchenObjectSO randomSO = spawnObjectSOArray[Random.Range(0, spawnObjectSOArray.Length)];

                    Transform obj = Instantiate(randomSO.prefab);
                    obj.GetComponent<KitchenObject>().SetKitchenObjectParent(clearCounters[randomIndex]);
                }
            }

            isReadyToSpawn = false; // 重置狀態
        }

    }
}
