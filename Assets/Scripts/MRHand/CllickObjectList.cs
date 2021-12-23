using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CllickObjectList : MonoBehaviour
{
    //-------------------------------------------------------------------------------------------------------------
    //�I�����Ă���G�ƕ���̃I�u�W�F�N�g�̃��X�g
    public List<GameObject> _weaponObjList = new List<GameObject>();

    public List<GameObject> _enemyObjList = new List<GameObject>();


    /// <summary>
    /// ����̃I�u�W�F�N�g��I�������Ƃ��ɑI�𕐊�̃��X�g�ɃI�u�W�F�N�g��ǉ�
    /// ����I�u�W�F�N�g����Ăяo��
    /// </summary>
    /// <param name="obj">�ǉ�����I�u�W�F�N�g</param>
    public void WeaponObjectSet(GameObject obj)
    {
        if (_weaponObjList.Contains(obj))
        {
            
        }
        else
        {
            //���X�g�ɃI�u�W�F�N�g�ǉ�
            _weaponObjList.Add(obj);
        }

    }

    /// <summary>
    /// �G�I�u�W�F�N�g��I�������Ƃ��ɃI�u�W�F�N�g�����X�g�Ɋi�[
    /// �G�I�u�W�F�N�g����Ăяo��
    /// </summary>
    /// <param name="obj"></param>
    public void EnemyObjSet(GameObject obj)
    {
        if (_enemyObjList.Contains(obj))
        {

        }
        else
        {
            //���X�g�ɃI�u�W�F�N�g�ǉ�
            _enemyObjList.Add(obj);
        }
    }

    /// <summary>
    /// �U�����탊�X�g���N���A����
    /// </summary>
    public void WeaponListClear()
    {
        
        _weaponObjList.Clear();
    }

    /// <summary>
    /// �G���X�g���N���A����
    /// </summary>
    public void EnemyListClear()
    {
        
        _enemyObjList.Clear();
    }
}
