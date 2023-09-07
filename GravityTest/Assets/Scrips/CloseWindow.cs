using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    private Button button;
    [SerializeField] GameObject WindowToClose;
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => CloseObj());

    }

  
    public void CloseObj()
    {
        SoundClick.Instance.SoundClicking2();
        WindowToClose.SetActive(false);
    }
}
