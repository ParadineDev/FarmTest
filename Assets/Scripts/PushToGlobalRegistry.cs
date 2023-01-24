using UnityEngine;

public class PushToGlobalRegistry : MonoBehaviour
{
    public enum Bakers {Canvas, Camera, Player}
    [SerializeField] private Bakers _baker;
    private void Awake()
    {        
        if (_baker==Bakers.Canvas) GlobalRegistry.Canvas = gameObject.transform;
        if (_baker==Bakers.Camera) GlobalRegistry.Camera = GetComponent<Camera>();
        if (_baker==Bakers.Player) GlobalRegistry.Player = GetComponent<Player>();
        Destroy(this);
    }    
}
