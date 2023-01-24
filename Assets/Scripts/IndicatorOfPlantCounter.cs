using UnityEngine;

public class IndicatorOfPlantCounter : MonoBehaviour
{
    [SerializeField] private string _textBeforeValue;
    [SerializeField] private PlantModel.Names _plantName;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    private void OnEnable()
    {
        CreateCounter();
    }    
    private void CreateCounter()
    {
        PlantCounter _plant = new(_plantName);
        GlobalData.PlantCounters.Add(_plant);
        GlobalData.OnCounterValueChanged += SetText;        
    }
    private void SetText(PlantModel.Names name, int value)
    {
        if (name==_plantName)
        {
            _text.text = _textBeforeValue + value.ToString();
        }                        
    }
    private void OnDisable()
    {
        GlobalData.OnCounterValueChanged -= SetText;        
    }
}
