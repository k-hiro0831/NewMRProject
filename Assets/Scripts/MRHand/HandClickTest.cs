using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class HandClickTest : MonoBehaviour
{
    [SerializeField]
    private GameObject testobj = default;

    [SerializeField]
    private ObjGetClickTest objGetClickTest = default;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void EventTest(MixedRealityPointerEventData eventData)
    {
        objGetClickTest.SetObject(this.gameObject);




    }



}
