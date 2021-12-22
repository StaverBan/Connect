using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Field : MonoBehaviour
    {
    [SerializeField] private SpriteRenderer _shadow;
    [SerializeField] private bool _isLastPoint;
    [SerializeField] private List<Figure> _reservedFigure;

    [Header(header:"Near Fields", order = 0)]
    
    [SerializeField] private Field _fieldUp;
    [SerializeField] private Field _fieldDown;
    [SerializeField] private Field _fieldRight;
    [SerializeField] private Field _fieldLeft;
    
    public List<Figure> Figure => _reservedFigure;
    public event Action<Field, Field> OnSwapField;

    private Vector2 dragVector;
    private Vector2 startDragPos;
    private bool _isHide;
    
    private void OnMouseDown()
    {
        startDragPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if(_isLastPoint) return;
        
        var mousePosition = (Vector2) Input.mousePosition;
        dragVector = startDragPos - mousePosition;

        if (!(dragVector.magnitude > 10)) return;
        
        var x = dragVector.x;
        var y = dragVector.y;

        if (Math.Abs(x) > Mathf.Abs(y))
        {
            var field = x > 0 ? _fieldLeft : _fieldRight;
            OnSwapField?.Invoke(this, field);
        }
        else
        {
            var field = y > 0 ? _fieldDown : _fieldUp;
            OnSwapField?.Invoke(this, field);
        }
    }

    public void ReserveField(List<Figure> figure)
    {
        if (figure == null)
        {
            _reservedFigure.Clear();
            return;
        }

        foreach (var figure1 in figure)
        {
            figure1.SetNextPosition(transform.position, _isLastPoint);
        }
        
        _reservedFigure.AddRange(figure);
    }

    private void OnFigureEndMove(Figure obj)
    {
        throw new NotImplementedException();
    }

    public void Hide()
    {
        _isHide = true;
    }

    private void Update()
    {
        if (_isHide)
            HideShadowAndPath();
    }


    private void HideShadowAndPath()
    {

        var color = _shadow.color;

        color = Color.Lerp(color, Color.clear, Time.deltaTime * 5);

        _shadow.color = color;
    }

    public bool CheckImNull()
    {
        if (_isLastPoint) return false;
        return _reservedFigure.Count != 0;
    }
    }
}