using UnityEngine;

public class ResetGlobals : MonoBehaviour
{
    private void Awake()
    {
        GlobalData.Reset();
        Destroy(this);        
    }    
}
