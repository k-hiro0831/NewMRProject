using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGetClickTest : MonoBehaviour
{

    [SerializeField]
    private GameObject TestObj = default;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetObject(GameObject obj)
    {
        print("オブジェクトセット");
        print("オブジェクト" + obj);
        TestObj = obj;
    }
}
