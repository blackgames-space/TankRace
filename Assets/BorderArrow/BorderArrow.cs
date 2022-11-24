using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Roman.BorderArrow
{
    public class BorderArrow : MonoBehaviour
    {

        internal Action<BorderArrow> TargetExitScreenEvent { get; private set; }
        internal Action<BorderArrow> TargetEnterScreenEvent { get; private set; }

        [SerializeField] private float _borderOffset;
        [SerializeField] private Camera _canvasCamera;
        [SerializeField] private Transform _arrowPivot;
        [SerializeField] private Transform _target;

        internal float BorderOffset
        {
            get { return _borderOffset; }
            set
            {
                _borderOffset = value;
                _screenXh = Screen.width / 2f - value;
                _screenYh = Screen.height / 2f - value;
                _center = new Vector2(_screenXh + value, _screenYh + value);
            }
        }

        internal Transform Target
        {
            get => _target;
            set => _target = value;
        }

        private float _screenYh;
        private float _screenXh;
        private float _screenTg;
        private bool _IsInside;
        private Animator _animator;
        private Billboard _iconBillboard;
        private Vector2 _center;

        void Awake()
        {
            Setup();
        }

        internal void Setup()
        {
            _animator = _arrowPivot.GetComponent<Animator>();
            _iconBillboard = this.transform.GetComponentInChildren<Roman.BorderArrow.Billboard>();
            BorderOffset = _borderOffset;

            _screenTg = _screenYh / _screenXh;

            if (_canvasCamera == null)
            {
                _canvasCamera = Camera.main;
            }

            _iconBillboard.SetCameraTranform(_canvasCamera.transform);
        }

        internal void SetTarget(Transform target)
        {
            _target = target;
        }



        // Update is called once per frame
        void Update()
        {
            Vector2 targetRelativePosition = (Vector2)_canvasCamera.WorldToScreenPoint(_target.position) - _center;

            bool IsInside = this.IsInsideScreen(targetRelativePosition);

            if (!_IsInside && IsInside)
                OnTargetEnteredScreen();
            if (_IsInside && !IsInside)
                OnTargetExitedScreen();

            _IsInside = IsInside;

            if (_IsInside)
                UpdateInsideScreen(targetRelativePosition);
            else
                UpdateOutsideScreen(targetRelativePosition);

        }

        bool IsInsideScreen(Vector2 targetRelativePosition)
        {
            return targetRelativePosition.x < _screenXh
                && targetRelativePosition.x > -_screenXh
                && targetRelativePosition.y < _screenYh
                && targetRelativePosition.y > -_screenYh;
        }

        void UpdateInsideScreen(Vector2 targetRelativePosition)
        {
            _arrowPivot.localPosition = targetRelativePosition + _center;
            _arrowPivot.transform.localRotation = Quaternion.RotateTowards(
                _arrowPivot.transform.localRotation,
                Quaternion.LookRotation(Vector3.down, Vector3.back)
                , 720f * Time.deltaTime);
        }

        void UpdateOutsideScreen(Vector2 targetRelativePosition)
        {
            Vector2 arrowPosition = Vector2.zero;

            float ySign = Mathf.Sign(targetRelativePosition.y);
            float xSign = Mathf.Sign(targetRelativePosition.x);

            float yMod = Mathf.Abs(targetRelativePosition.y);
            float xMod = Mathf.Abs(targetRelativePosition.x);

            float targetTg = (xMod == 0) ? float.MaxValue : yMod / xMod;
            if (targetTg > _screenTg)
            {
                arrowPosition.y = ySign * _screenYh;
                arrowPosition.x = xSign * Mathf.Abs(arrowPosition.y) / targetTg;
            }
            else
            {
                arrowPosition.x = xSign * _screenXh;
                arrowPosition.y = ySign * Mathf.Abs(arrowPosition.x) * targetTg;
            }
            arrowPosition += _center;
            _arrowPivot.localPosition = arrowPosition;
            _arrowPivot.transform.localRotation = Quaternion.LookRotation(targetRelativePosition, Vector3.back);
        }

        void OnTargetEnteredScreen()
        {
            _animator.SetTrigger("EnterScreen");
            TargetEnterScreenEvent?.Invoke(this);
        }

        void OnTargetExitedScreen()
        {
            _animator.SetTrigger("ExitScreen");
            TargetExitScreenEvent?.Invoke(this);
        }
    }
}