using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class WeaponObjectCllick : MonoBehaviour
{

    private CllickObjectList cllickObjectList = default;

    private const string LIST_OBJECT = "List";

    private void Awake()
    {
        //リストを管理しているオブジェクトを取得
        cllickObjectList = GameObject.FindGameObjectWithTag(LIST_OBJECT).GetComponent<CllickObjectList>();
    }

    /// <summary>
    /// 武器をクリックしてリストに追加する
    /// クリックイベントで呼び出す
    /// </summary>
    /// <param name="eventData"></param>
    public void WeaponCllickEvent(MixedRealityPointerEventData eventData)
    {
        cllickObjectList.WeaponObjectSet(this.gameObject);
    }

}
