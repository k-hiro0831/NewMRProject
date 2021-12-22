using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _poolParent = null;

    private ObjectPool _gunPool;
    [SerializeField]
    private GameObject _gunPrefab = null;
    [SerializeField]
    private int _gunCount = 10;

    private ObjectPool _swordPool;
    [SerializeField]
    private GameObject _swordPrefab = null;
    [SerializeField]
    private int _swordCount = 10;

    //���̓e�X�g�p�Ȃ̂ł��Ƃō폜
    [SerializeField]
    private List<GameObject> testList = new List<GameObject>();
    [SerializeField]
    private GameObject testEnemy = null;
    //

    private void Start()
    {
        _gunPool = new ObjectPool();
        _gunPool.Pool(_poolParent, _gunPrefab, _gunCount);

        _swordPool = new ObjectPool();
        _swordPool.Pool(_poolParent, _swordPrefab, _swordCount);
    }

    private void Update()
    {

        //���̓e�X�g�p�Ȃ̂ł��Ƃō폜
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < testList.Count; i++)
            {
                testList[i].GetComponent<WeaponAttack>().Attack(testEnemy);
            }
        }
        //

    }

    /// <summary>
    /// �e�̃v�[������e�����o��
    /// </summary>
    /// <returns></returns>
    public GameObject ReturnGunObj()
    {
        return _gunPool.ReturnObj();
    }

    /// <summary>
    /// ���̃v�[�����猕�����o��
    /// </summary>
    /// <returns></returns>
    public GameObject ReturnSwordObj()
    {
        return _swordPool.ReturnObj();
    }
}
