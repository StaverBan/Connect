using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FigureCollider: MonoBehaviour
    {
        public event Action OnCollision;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
           OnCollision?.Invoke();
        }
    }
}