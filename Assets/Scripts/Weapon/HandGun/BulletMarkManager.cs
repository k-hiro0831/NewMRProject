using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMarkManager : MonoBehaviour
{
    private ObjectPool _pool;
    [SerializeField]
    private GameObject _bulletMarkPrefab = null;
    [SerializeField, Header("����������")]
    private int _poolObjCount = 10;
    [SerializeField,Header("�e���\�����")]
    private int _showLimit = 5;

    private int _showCounter = 0;

    private List<GameObject> _markList = new List<GameObject>();

    private void Start()
    {
        _pool = new ObjectPool();
        _pool.Pool(this.gameObject,_bulletMarkPrefab,_poolObjCount);


    }

    private void Update()
    {
        
    }

    public GameObject ReturnBulletMarkPrefab()
    {
        GameObject _obj = _pool.ReturnObj();
        if (_showCounter < _showLimit)
        {
            _showCounter++;
        }
        //����𒴂����Ƃ�
        else
        {
            _markList[0].SetActive(false);
            _markList.RemoveAt(0);
        }
        _markList.Add(_obj);
        return _obj;
    }

    /// <summary>
    /// �w�肵���e���v���n�u�����X�g���珜�O���A�v�[���ɖ߂�
    /// </summary>
    /// <param name="_obj"></param>
    public void RemoveObj(GameObject _obj)
    {
        _markList.Remove(_obj);
        _showCounter--;
        _obj.SetActive(false);
    }
}
