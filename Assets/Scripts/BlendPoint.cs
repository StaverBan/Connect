using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlendPoint : MonoBehaviour
{
    
    private SpriteRenderer _spriteRenderer;
    private float _smoothScale;
    private float _waitTime;
    private Color _color;
    private Color _towardColor;

    private float timer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(float smoothScale, Color color, Color towardColor, float waitTime)
    {
        _smoothScale = smoothScale;
        _color = color;
        _towardColor = towardColor;
        _waitTime = waitTime;
    }

    public void ChangeColor()
    {
        _spriteRenderer.color = _color;
        timer = _waitTime;
    }

    private void Update()
    {
        if(isTimerBehindZero())
            return;
        
        var nowColor = _spriteRenderer.color;
        var smoothColor = Color.Lerp(nowColor, _towardColor, Time.deltaTime * _smoothScale);

        _spriteRenderer.color = smoothColor;
    }

    private bool isTimerBehindZero()
    {
        timer -= Time.deltaTime;

        return timer >= 0;
    }
}
