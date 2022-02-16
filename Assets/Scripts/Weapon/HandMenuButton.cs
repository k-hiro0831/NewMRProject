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
        _isOpen = _selectScript.ReturnOpenFlag();

        if (_isOpen)
        {
            _selectScript.CloseSelectUI();
        }
        else
        {
            _selectScript.OpenSelectUI();
        }
    }
}
