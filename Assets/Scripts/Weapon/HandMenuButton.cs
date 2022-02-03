using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuButton : MonoBehaviour
{
    [SerializeField]
    private WeaponSelectUI _selectScript;

    private bool _isOpen = false;

    public void MenuOpenClose()
    {
        if (_isOpen)
        {
            _isOpen = false;
            _selectScript.CloseSelectUI();
        }
        else
        {
            _isOpen = true;
            _selectScript.OpenSelectUI();
        }
    }
}
