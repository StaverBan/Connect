using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private CanvasController controller;
        [SerializeField] private int nowLevel;
        public bool _isFigureMove;

        private Animator _animator;
        private List<Field> _fields;
        private List<Figure> _figures;
        public bool _isEndGame;
        public LevelSettings _level;
        private List<Figure> movedFigures = new List<Figure>();
        
        private void Update()
        {
            if (_isEndGame && !_isFigureMove)
                EndGame();
        }

        private void Start()
        {
            controller.NextLevelCover += OnCoverAnimationEnd;
            InitLevelSettings();
        }

        private void Load()
        {
            
            foreach (var field in _fields)
            {
                field.OnSwapField += OnTouchField;
            }

            foreach (var figure in _figures)
            {
                figure.iMove += FigureMove;
                figure.iEndMove += FigureEndMove;
                figure.Crash += OnCrash;
            }
        }

        private void OnCrash()
        {
            _isFigureMove = false;
            UnSubscribe();
            InitLevelSettings();
        }

        private void OnCoverAnimationEnd()
        {
            nowLevel++;
            controller.Animator.Play("NextLevel");
            UnSubscribe();
            InitLevelSettings();
        }
        
        private void InitLevelSettings()
        {
            movedFigures.Clear();
            _level = levelLoader.LoadLevel(nowLevel);
            controller.SetLevelText(nowLevel);
            _fields = _level.Fields;
            _figures = _level.Figures;
            _animator = _level.Animator;

            _level.OnLevelCompleteAnimationEnd += OnLevelCompeteAnimation;
            
            Load();
        }

        private void OnLevelCompeteAnimation()
        {
            _level.OnLevelCompleteAnimationEnd -= OnLevelCompeteAnimation;
            controller.Animator.Play("HideWindow");
        }

        private void FigureMove(Figure figure)
        {
            movedFigures.Add(figure);
            _isFigureMove = true;
        }

        private void FigureEndMove(Figure figure)
        {
            movedFigures.Remove(figure);

            if (movedFigures.Count == 0)
            {
                CheckOnEndGame();
                _isFigureMove = false;
            }
        }

        private void CheckOnEndGame()
        {
            bool endGame = false;
            foreach (var field in _fields)
            {
                if (endGame != true)
                    endGame = field.CheckImNull();
            }
            
            _isEndGame = !endGame;
        }
        
        private void OnEndGame()
        {
            _isEndGame = true;
        }
        
        private void EndGame()
        {
            _isEndGame = false;
            
            _animator.Play("EndGame");
            
            UnSubscribe();
        }

        private void UnSubscribe()
        {
            foreach (var field in _fields)
            {
                field.OnSwapField -= OnTouchField;
            }

            foreach (var figure in _figures)
            {
                figure.iMove -= FigureMove;
                figure.iEndMove -= FigureEndMove;
                figure.Crash -= OnCrash;
            }
        }
        
        private void OnTouchField(Field obj, Field obj2)
        {
            if(obj2==null) return;

            var objFigure = new List<Figure>();
            
            objFigure.AddRange(obj.Figure);
            
            obj.ReserveField(null);
            obj2.ReserveField(objFigure);
        }
    }
}