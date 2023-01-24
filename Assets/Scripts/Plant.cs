using System.Collections;
using UnityEngine;
using System;

public class Plant : MonoBehaviour
{    
    public event Action OnDead;
    [HideInInspector] public bool IsGrown { get; private set; }
    private PlantModel.Types _type;
    private int _experienceAmount;
    private bool _isPlayerHere;
    private PlantModel.Names _name;
    public void Grow(float timeInSeconds)
    {
        StartCoroutine(IncreaseSize(timeInSeconds));
    }
    public void SetType(PlantModel.Types type)
    {
        _type = type;
    }
    public void SetName(PlantModel.Names name)
    {
        _name = name;
    }
    public void SetExperienceAmount(int value)
    {
        _experienceAmount = value;
    }
    public void DoActionBasedOnType()
    {
        if (_type==PlantModel.Types.Immortal)
        {
            Debug.Log("Can't be picked up or destroyed");            
        }
        if (_type==PlantModel.Types.CanBeDestroyed)
        {
            StartCoroutine(BecomeDestroyed());                                    
        }
        if (_type==PlantModel.Types.CanBePickedUp)
        {
            StartCoroutine(BecomePickedUp());            
        }
    }
    private IEnumerator IncreaseSize(float timeInSeconds)
    {
        float time = 0;
        while (time < timeInSeconds)
        {
            time += Time.deltaTime;
            Mathf.Clamp(time, 0, timeInSeconds);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.InverseLerp(0, timeInSeconds, time));
            yield return new WaitForEndOfFrame();
        }
        IsGrown = true;
        GlobalData.AddExperience(_experienceAmount);        
    }
    private IEnumerator BecomePickedUp()
    {
        GlobalRegistry.Player.AddTarget(transform);
        GlobalRegistry.Player.AddAnimation("Pick");
        GlobalRegistry.Player.OnArrived += SetPlayerHere;

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => _isPlayerHere);
        
        GlobalData.AddCounterValue(_name, _experienceAmount);
        OnDead?.Invoke();
        Destroy(gameObject);        
    } 
    private IEnumerator BecomeDestroyed()
    {
        GlobalRegistry.Player.AddTarget(transform);
        GlobalRegistry.Player.AddAnimation("Kill");
        GlobalRegistry.Player.OnArrived += SetPlayerHere;
        
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => _isPlayerHere);
        OnDead?.Invoke();
        Destroy(gameObject);        
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
