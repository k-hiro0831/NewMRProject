using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegister : MonoBehaviour
{
    [SerializeField, Header("�e��ނ̓G�������ɒǉ��i�S21��ށj")]
    private GameObject[] _enemys = default;

    private Vector3 _originPosition;

    void Start()
    {
        //�e�I�u�W�F�N�g�Ɋi�[
        //for (int i = 0; i < 21; i++)
        //{
        //    //�e�o�^����10
        //    for (int j = 0; j < 10; j++)
        //    {
        //        GameObject obj = GameObject.Instantiate(_enemys[i], _originPosition, Quaternion.identity);
        //        obj.transform.parent = this.transform.GetChild(i).gameObject.transform;
        //        obj.SetActive(false);
                
        //    }
            
        //}
    }
}
