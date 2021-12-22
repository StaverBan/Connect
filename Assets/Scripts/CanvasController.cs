using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _nextLevel;
    [SerializeField] private Button _lastLevel;
    [SerializeField] private Text _levelText;
    public Animator Animator => _animator;    
    public event Action NextLevelCover;

    public void OnAnimationEnd()
    {
        NextLevelCover?.Invoke();
    }

    public void SetLevelText(int nowLevel)
    {
        _levelText.text = nowLevel.ToString();
    }
}
