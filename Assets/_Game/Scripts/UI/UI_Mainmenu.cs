using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_Mainmenu : UICanvas
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text playerName;

    private void Start()
    {
        inputField.onEndEdit.AddListener((string text) => 
            ChangeNamePlayer() 
        );
    }

    private void ChangeNamePlayer()
    {
        playerName.text = inputField.text;
    }
}
