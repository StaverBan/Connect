using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private List<Figure> _figures = new List<Figure>();
    private List<Field> _fields = new List<Field>();
    
    public Animator Animator => _animator;
    public List<Figure> Figures => _figures;
    public List<Field> Fields => _fields;

    public event Action OnLevelCompleteAnimationEnd;

    public void OnEnable()
    {
        _figures.AddRange(transform.GetComponentsInChildren<Figure>());
        _fields.AddRange(transform.GetComponentsInChildren<Field>());
    }

    public void OnAnimationEnd()
    {
        OnLevelCompleteAnimationEnd?.Invoke();
    }
}
