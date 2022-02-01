using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectUI = null;
    private GameObject _selectParent = null;

    private bool _isOpen = true;

    [SerializeField]
    private AudioSource _openSound = null;
    [SerializeField]
    private AudioSource _closeSound = null;

    private void Start()
    {
        _selectParent = this.gameObject;
        _isOpen = true;
        OpenSelectUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OpenSelectUI();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseSelectUI();
        }
    }

    /// <summary>
    /// •Šíw“ü‰æ–Ê‚ğŠJ‚­
    /// </summary>
    public void OpenSelectUI()
    {
        if (_isOpen) { return; }

        _isOpen = true;
        _selectUI.SetActive(true);
        _openSound.Play();
    }
    /// <summary>
    /// •Šíw“ü‰æ–Ê‚ğ•Â‚¶‚é
    /// </summary>
    public void CloseSelectUI()
    {
        if (!_isOpen) { return; }

        _isOpen = false;
        _selectUI.SetActive(false);
        _closeSound.Play();
    }
}
