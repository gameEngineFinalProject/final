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
            // �T�{�Ҧ� ClearCounter �O�_���O�Ū�
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
                // �Ҧ��M���d�x���šA�ӥB�ثe�S�U���U �� �ͦ��U���U
                Transform trashBagTransform = Instantiate(trashBagSO.prefab);
                KitchenObject kitchenObject = trashBagTransform.GetComponent<KitchenObject>();

                if (kitchenObject == null)
                {
                    Debug.LogError("TrashBag prefab �W�S���� KitchenObject �}���I");
                    return;
                }

                kitchenObject.SetKitchenObjectParent(this);
                //Debug.Log("�U���U���\�ͦ��ó]�� ButtonCounter �l����");
                


                isReadyToSpawn = true;
            }
        }
        else
        {
            // �ĤG�����U �� �M���U���U�æb ClearCounter �W�H���ͦ����~
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

            isReadyToSpawn = false; // ���m���A
        }

    }
}
