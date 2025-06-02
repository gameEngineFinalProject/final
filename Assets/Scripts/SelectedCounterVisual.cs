using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] visualGameObjectArray;
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();

        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
        
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
