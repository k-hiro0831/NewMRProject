using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectUI = null;

    private GameObject _selectParent = null;

    private void Start()
    {
        _selectParent = this.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenSelectUI();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseSelectUI();
        }
    }

    /// <summary>
    /// ����w����ʂ��J��
    /// </summary>
    public void OpenSelectUI()
    {
        _selectUI.SetActive(true);
    }
    /// <summary>
    /// ����w����ʂ����
    /// </summary>
    public void CloseSelectUI()
    {
        _selectUI.SetActive(false);
    }

}
