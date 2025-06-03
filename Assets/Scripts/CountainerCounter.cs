using System;
using System.Collections;
using UnityEngine;

public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ScoreManager scoreManager; // ��J ScoreManager ����

    [SerializeField] private GameObject correctIcon;
    [SerializeField] private GameObject wrongIcon;

    public void Awake()
    {
        // �۰ʧ�l���󤤦W�� CorrectIcon / WrongIcon �� GameObject
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
            // ��J���T�A�}�a����A�[��
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Debug.Log("Correct");
            GameObject.Destroy(playerObject.gameObject);
            player.ClearKichenObject();
            scoreManager.AddScore(1);

            SoundManager.Instance.PlaySound(SoundManager.Instance.GetAudioClipRefs().Success, transform.position);

            StartCoroutine(ShowIcon(correctIcon));
        }
        else
        {
            // ������~�A���B�z�δ��ܿ��~
            Debug.Log("���~���~�A�L�k��J�I");

            SoundManager.Instance.PlaySound(SoundManager.Instance.GetAudioClipRefs().Fail, transform.position);
            StartCoroutine(ShowIcon(wrongIcon));
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
    private IEnumerator ShowIcon(GameObject icon)
    {
        if (icon == null) yield break;

        icon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        icon.SetActive(false);
    }



}
