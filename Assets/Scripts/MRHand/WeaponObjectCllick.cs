using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class WeaponObjectCllick : MonoBehaviour
{

    private CllickObjectList cllickObjectList = default;

    private const string LIST_OBJECT = "List";

    private void Awake()
    {
        //���X�g���Ǘ����Ă���I�u�W�F�N�g���擾
        cllickObjectList = GameObject.FindGameObjectWithTag(LIST_OBJECT).GetComponent<CllickObjectList>();
    }

    /// <summary>
    /// ������N���b�N���ă��X�g�ɒǉ�����
    /// �N���b�N�C�x���g�ŌĂяo��
    /// </summary>
    /// <param name="eventData"></param>
    public void WeaponCllickEvent(MixedRealityPointerEventData eventData)
    {
        cllickObjectList.WeaponObjectSet(this.gameObject);
    }

}
