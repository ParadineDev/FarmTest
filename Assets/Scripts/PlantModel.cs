using UnityEngine;
[CreateAssetMenu(fileName = "Plant.PlantModel", menuName = "Plant")]
public class PlantModel : ScriptableObject
{
    public enum Names { Tree, Grass, Carrot }
    public Names Name;
    public GameObject Object;
    public Sprite Icon;
    public float GrowTime;
    public int ExperienceAmount;
    public enum Types { Immortal, CanBeDestroyed, CanBePickedUp }
    public Types Type;
}
