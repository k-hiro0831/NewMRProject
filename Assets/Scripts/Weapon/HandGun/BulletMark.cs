using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMark : MonoBehaviour
{

    private GameObject _parentObj = null;
    private BulletMarkManager _managerScript;

    [SerializeField,Header("非表示になるまでの時間")]
    private float _autoRemoveTime = 2f;

    private void Start()
    {
        //親取得
        _parentObj = this.gameObject.transform.parent.gameObject;
        _managerScript = _parentObj.GetComponent<BulletMarkManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(RemoveThis());
    }

    private IEnumerator RemoveThis()
    {
        yield return new WaitForSeconds(_autoRemoveTime);
        _managerScript.RemoveObj(this.gameObject);
        yield break;
    }
}
