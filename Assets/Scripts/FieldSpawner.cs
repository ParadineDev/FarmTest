using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    [SerializeField] private FieldModel[] _fields;    
    [SerializeField] private int _chosenField;
    [SerializeField] private Vector3 _spawnPosition;
    private void Start()
    {
        CreateField(_fields[_chosenField]);
    }        
    private void CreateField(FieldModel field)
    {
        Vector3 position = new();
        float shiftX = ( (field.Width-1) ) / 2;
        float shiftZ = ( (field.Length-1) ) / 2;
        
        for (int x = 0; x < field.Width; x++)
        {
            position.x = (x - shiftX)*field.Distance;
            for (int z = 0; z < field.Length; z++)
            {
                position.z = (z - shiftZ)*field.Distance;                                
                Instantiate(field.cell, position, Quaternion.identity );
            }
        }
        position.x = 0;
        position.z+=5;
        GlobalRegistry.Player.SetDefaultPosition(position);
    }
}
