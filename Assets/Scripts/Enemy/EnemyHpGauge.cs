using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpGauge : MonoBehaviour
{
    /// <summary>
    /// HP�����炷����
    /// </summary>
    /// <param name="_enemyHp">������</param>
    public int MinusEnemyHp(int _enemyHp, int _gunAtk)
    {
        _enemyHp -= _gunAtk;
        return _enemyHp;
    }
}
