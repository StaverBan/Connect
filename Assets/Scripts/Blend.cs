using System.Collections.Generic;
using UnityEngine;

public class Blend : MonoBehaviour
{
    [SerializeField] private List<BlendPoint> _points;
    [SerializeField] private Color _natureColor;
    [SerializeField] private Color _towardColor;
    [SerializeField] private float _smoothScale;
    [SerializeField] private float _timeRate;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _waitForCircle;

    private float _timer;
    private int _nowPoint;
    private Queue<BlendPoint> _pointsQueue;
    private void Awake()
    {
        _timer = _timeRate;
        _nowPoint = 0;
        _pointsQueue = new Queue<BlendPoint>(_points.Count);
        
        foreach (var blendPoint in _points)
        {
            blendPoint.Init(_smoothScale,_natureColor,_towardColor,_waitTime);
            _pointsQueue.Enqueue(blendPoint);
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0) return;
        
        SignalPointInQueue();
    }

    private void SignalPointInQueue()
    {
        var point = _pointsQueue.Dequeue();

        _nowPoint++;
        
        point.ChangeColor();
        
        _pointsQueue.Enqueue(point);

        if (_nowPoint == _points.Count)
        {
            _timer = _timeRate + _waitForCircle;
            _nowPoint = 0;
        }
        else
        {
            _timer = _timeRate;
        }
    }
}
