using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Player : MonoBehaviour
{   
    public event Action<Transform> OnArrived;
    private Vector3 _defaultPosition;
    private List<Transform> _targets = new();
    private List<string> _animations = new(); 
    private Animator _animator;
    private NavMeshAgent _agent;
    private bool _isAnimationJustFinished;
    
    public void AddAnimation(string animation)
    {
        _animations.Add(animation);
    }
    public void AddTarget(Transform target)
    {
        _targets.Add(target);
        if (_targets.Count==1) StartCoroutine(FindPath(target));
    }    
    public void SetDefaultPosition(Vector3 value)
    {
        _defaultPosition = value;
        StartCoroutine(GoHome());               
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();        
        _agent = GetComponent<NavMeshAgent>();        
    }
    private IEnumerator GoHome()
    {
        _animator.SetBool("Run", true);
        _agent.SetDestination(_defaultPosition);
        yield return new WaitUntil(IsArrived);
        _animator.SetBool("Run", false);
    }   
    private IEnumerator FindPath(Transform target)
    {
        StopCoroutine(GoHome());        
        _animator.SetBool("Run", true);  
        _agent.SetDestination(target.position);
        yield return new WaitUntil(IsArrived);
        PlayAnimation(_animations[0]);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(IsAnimationFinished);
        OnArrived?.Invoke(target);
        _targets.RemoveAt(0);
        _animator.SetBool("Run", false);        

        if (_targets.Count>0) 
        {            
            StartCoroutine(FindPath(_targets[0]));
        }
        else
        {
            StartCoroutine(GoHome());
        }        
    }
    private bool IsArrived()
    {
        bool arrived = !_agent.pathPending && !_agent.hasPath;
        return arrived;
    }
    private void PlayAnimation(string animation)
    {
        _animations.RemoveAt(0);
        _animator.SetTrigger(animation);
    }
    private bool IsAnimationFinished()
    {
        bool result = 
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Run") 
            || _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        return result;
    }      
}
