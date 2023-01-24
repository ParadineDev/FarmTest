using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private List<PlantModel> _plants;
    [SerializeField] private GameObject _button;
    [SerializeField] private int _buttonDistance;
    private bool _isBusy;
    private bool _isPlayerHere;
    private List<GameObject> _buttons = new();
    private Plant _currentPlant;    
    private void OnMouseDown()
    {
        if (!GlobalData.AreChooseButtonsExist)
        {
            if (_currentPlant!=null)
            {
                _currentPlant.DoActionBasedOnType();                                
            }      
            else if (!_isBusy)
            {
                ShowAvailablePlants();
            }    
        }
    }
    private void ShowAvailablePlants()
    {
        GlobalData.AreChooseButtonsExist = true;
        for (int i = 0; i < _plants.Count; i++)
        {
            int x = i;
            float step = 360 / _plants.Count;
            GameObject obj = Instantiate(_button, transform.position, Quaternion.identity, GlobalRegistry.Canvas.transform);
            _buttons.Add(obj);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.position = GlobalRegistry.Camera.WorldToScreenPoint(transform.position);
            rect.Rotate(0, 0, i * step);
            rect.anchoredPosition += (Vector2)rect.transform.up * _buttonDistance;
            rect.Rotate(0, 0, -i * step);
            obj.GetComponent<Image>().sprite = _plants[i].Icon;
            obj.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(SeedPlant(_plants[x])));
        }
    }
    private IEnumerator SeedPlant(PlantModel plant)
    {
        _isBusy = true;
        DestroyButtons();
        CallPlayer();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => _isPlayerHere);
        SpawnPlant(plant);
    }
    private void SpawnPlant(PlantModel plant)
    {
        GameObject tempPlant = Instantiate(plant.Object, transform.position, Quaternion.identity);
        _currentPlant = tempPlant.GetComponent<Plant>();
        _currentPlant.Grow(plant.GrowTime);
        _currentPlant.SetExperienceAmount(plant.ExperienceAmount);
        _currentPlant.SetName(plant.Name);
        _currentPlant.SetType(plant.Type);
        _isPlayerHere = false;
        _isBusy = false;
        _currentPlant.OnDead += (() => _currentPlant = null);
    }
    private void CallPlayer()
    {
        GlobalRegistry.Player.AddTarget(transform);
        GlobalRegistry.Player.AddAnimation("Seed");
        GlobalRegistry.Player.OnArrived += SetPlayerHere;
    }
    private void DestroyButtons()
    {
        GlobalData.AreChooseButtonsExist = false;
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(_buttons[i]);
        }
        _buttons.Clear();
    }
    private void SetPlayerHere(Transform targetOfPlayer)
    {
        if (targetOfPlayer==transform)
        {
            GlobalRegistry.Player.OnArrived -= SetPlayerHere;
            _isPlayerHere = true;            
        }        
    }
}
