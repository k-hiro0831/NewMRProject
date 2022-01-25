using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpGauge : MonoBehaviour
{
    /// <summary>
    /// HP‚ğŒ¸‚ç‚·ˆ—
    /// </summary>
    /// <param name="_enemyHp">Œ¸­—Ê</param>
    public int MinusEnemyHp(int _enemyHp, int _gunAtk)
    {
        _enemyHp -= _gunAtk;
        return _enemyHp;
    }
}
