using UnityEngine;

public class IndicatorOfExperience : MonoBehaviour
{
    [SerializeField] private string _textBeforeValue;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    private void OnEnable()
    {
        GlobalData.OnExperienceValueChanged += SetText;
    }
    private void SetText(int value)
    {
        _text.text = _textBeforeValue + value.ToString();
    }
    private void OnDisable()
    {
        GlobalData.OnExperienceValueChanged += SetText;
    }
}
