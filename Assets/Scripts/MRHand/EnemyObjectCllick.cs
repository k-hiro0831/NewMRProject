using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;


public class EnemyObjectCllick : MonoBehaviour
{

    private CllickObjectList cllickObjectList = default;

    private const string LIST_OBJECT = "List";

    private void Awake()
    {
        //���X�g���Ǘ����Ă���I�u�W�F�N�g���擾
        cllickObjectList = GameObject.FindGameObjectWithTag(LIST_OBJECT).GetComponent<CllickObjectList>();
    }

    /// <summary>
    /// �I�u�W�F�N�g��pointerHandler��OnPointerDown����Ăяo��
    /// ���X�g�ɃI�u�W�F�N�g��ǉ�
    /// </summary>
    /// <param name="eventData"></param>
    public void EnemyCllickEvent(MixedRealityPointerEventData eventData)
    {
        cllickObjectList.EnemyObjSet(this.gameObject);
    }

}
