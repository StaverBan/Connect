using System;
using DefaultNamespace;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private float _smoothTime;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private FigureCollider _figureCollider;

    public event Action<Figure> iMove;
    public event Action<Figure> iEndMove;
    public event Action Crash;

    private bool _isLastPosition;
    private Vector3 _nextPosition;

    private void Start()
    {
        _nextPosition = transform.position;
        _figureCollider.OnCollision += OnCollider;
        _isLastPosition = false;
    }
    
    public void SetNextPosition(Vector3 nextPosition, bool lastPosition)
    {
        iMove?.Invoke(this);
        _nextPosition = nextPosition;
        _isLastPosition = lastPosition;
    }
    
    private void Update()
    {
        if (Vector3.Distance(transform.position, _nextPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, Time.deltaTime * _smoothTime);
            return;
        }

        if(Vector3.Distance(transform.position,_nextPosition)>0)
        {
            transform.position = _nextPosition;
            transform.localScale =Vector3.one * 1.05f;
            iEndMove?.Invoke(this);
        }
        
        if (_isLastPosition)
            ChangeColor();
    }
    
    private void ChangeColor()
    {
        var color = _spriteRenderer.color;

        color = Color.Lerp(color, Color.white, Time.deltaTime*10);
        
        _spriteRenderer.color = color;
    }

    private void OnCollider()
    {
        Crash?.Invoke();
    }
}
