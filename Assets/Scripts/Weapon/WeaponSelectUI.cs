using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectUI = null;
    private GameObject _selectParent = null;

    private bool _isOpen = false;

    [SerializeField]
    private AudioSource _openSound = null;
    [SerializeField]
    private AudioSource _closeSound = null;

    private void Start()
    {
        _selectParent = this.gameObject;
        _isOpen = false;
        CloseSelectUI();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    OpenSelectUI();
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    CloseSelectUI();
        //}
    }

    /// <summary>
    /// ����w����ʂ��J��
    /// </summary>
    public void OpenSelectUI()
    {
        if (_isOpen) { return; }

        _isOpen = true;
        _selectUI.SetActive(true);
        _openSound.Play();
    }
    /// <summary>
    /// ����w����ʂ����
    /// </summary>
    public void CloseSelectUI()
    {
        if (!_isOpen) { return; }

        _isOpen = false;
        _selectUI.SetActive(false);
        _closeSound.Play();
    }
}
