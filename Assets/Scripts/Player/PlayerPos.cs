using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private Vector3 _vec;

    void Update()
    {
        _vec = new Vector3(this.transform.position.x, 0.2f, this.transform.position.z);
    }
}
