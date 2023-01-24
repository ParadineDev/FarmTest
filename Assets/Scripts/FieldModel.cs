using UnityEngine;
[CreateAssetMenu (fileName = "Field", menuName = "Field")]
public class FieldModel : ScriptableObject
{
    public GameObject cell ;
    public int Width;
    public int Length;
    public float Distance;
}
