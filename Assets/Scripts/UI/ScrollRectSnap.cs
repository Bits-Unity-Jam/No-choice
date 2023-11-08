using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ScrollRectSnap : MonoBehaviour
    {
        private float[] _points;
        [Tooltip("How many items are there within the content (steps)")]
        [SerializeField]
        private int itemsInContent = 1;
        [Tooltip("How quickly the GUI snaps to each panel")]
        [SerializeField]
        private float snapSpeed;
        [SerializeField]
        private float inertiaCutoffMagnitude;
        private float stepSize;

        private ScrollRect scroll;
        private RectTransform scrollRectTransform;
        private bool LerpH;
        private float targetH;
        [Tooltip("Snap horizontally")]
        [SerializeField]
        private bool snapInH = true;

        private bool LerpV;
        private float targetV;
        [Tooltip("Snap vertically")]
        [SerializeField]
        private bool snapInV = true;

        private bool dragInit = true;
        private int dragStartNearest;
        private int _currentPoint;
        private bool _isDragging = false;

        [Header("Button")]
        [SerializeField]
        private Button nextButton;
        [SerializeField]
        private Button previousButton;

        [SerializeField]
        private AudioSource sweepSound;

        public event Action<int> OnScrollItemChanged;

        public int CurrentPoint
        {
            get => _currentPoint;
            set
            {
                _currentPoint = value;
                OnScrollItemChanged?.Invoke(_currentPoint);
            }
        }

        private void Awake()
        {
            Time.timeScale = 1;
            nextButton.onClick.AddListener(SetNext);
            previousButton.onClick.AddListener(SetPrevious);
        }

        // Use this for initialization
        private void Start()
        {
            scroll = gameObject.GetComponent<ScrollRect>();
            scrollRectTransform = gameObject.GetComponent<RectTransform>();
            scroll.inertia = true;

            if (itemsInContent > 0)
            {
                _points = new float[itemsInContent];
                stepSize = 1 / (float)(itemsInContent - 1);

                for (int i = 0; i < itemsInContent; i++)
                {
                    _points[i] = i * stepSize;
                }
            }
            else
            {
                _points[0] = 0;
            }

            SetInteractable();
        }

        public void SetNext()
        {
            sweepSound.Play();
            CurrentPoint = FindNearest(scroll.horizontalNormalizedPosition, _points);
            int targetPoint = Mathf.Clamp(CurrentPoint + 1, 0, _points.Length - 1);
            if (scroll.horizontal && snapInH && scroll.horizontalNormalizedPosition is > -0.001f and <= 1.001f)
            {
                targetH = _points[targetPoint];
                LerpH = true;
            }
            if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition is >= -0.001f and <= 1.001f)
            {
                targetH = _points[targetPoint];
                LerpH = true;
            }
            CurrentPoint = targetPoint;
        }

        public void SetPrevious()
        {
            sweepSound.Play();
            CurrentPoint = FindNearest(scroll.horizontalNormalizedPosition, _points);
            int targetPoint = Mathf.Clamp(CurrentPoint - 1, 0, _points.Length - 1);
            if (scroll.horizontal && snapInH && scroll.horizontalNormalizedPosition is >= -0.001f and <= 1.001f)
            {
                targetH = _points[targetPoint];
                LerpH = true;
            }
            if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition is >= -0.001f and <= 1.001f)
            {
                targetH = _points[targetPoint];
                LerpH = true;
            }
            CurrentPoint = targetPoint;
        }

        public int GetCurrentPoint()
        {
            return FindNearest(scroll.horizontalNormalizedPosition, _points);
        }

        private void SetInteractable()
        {
            int currentPoint = FindNearest(scroll.horizontalNormalizedPosition, _points);
            int targetPoint = Mathf.Clamp(currentPoint, 0, _points.Length - 1);

            if (nextButton != null)
            {
                nextButton.interactable = _points[targetPoint] == 1 ? false : true;
            }

            if (previousButton != null)
            {
                previousButton.interactable = _points[targetPoint] != 0;
            }
        }

        private void Update()
        {
            SetInteractable();
            if (LerpH)
            {
                scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, targetH, snapSpeed * Time.deltaTime);
                if (Mathf.Approximately(scroll.horizontalNormalizedPosition, targetH)) LerpH = false;
            }
            if (LerpV)
            {
                scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetV, snapSpeed * Time.deltaTime);
                if (Mathf.Approximately(scroll.verticalNormalizedPosition, targetV)) LerpV = false;
            }
            
        }

        public void DragEnd()
        {
           
            int target = FindNearest(scroll.horizontalNormalizedPosition, _points);

            if (target == dragStartNearest && scroll.velocity.sqrMagnitude > inertiaCutoffMagnitude * inertiaCutoffMagnitude)
            {
                if (scroll.velocity.x < 0)
                {
                    target = dragStartNearest + 1;
                }
                else if (scroll.velocity.x > 1)
                {
                    target = dragStartNearest - 1;
                }
                target = Mathf.Clamp(target, 0, _points.Length - 1);
                
            }

            if (scroll.horizontal && snapInH && scroll.horizontalNormalizedPosition > 0f && scroll.horizontalNormalizedPosition < 1f)
            {
                targetH = _points[target];
                LerpH = true;
            }
            if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition > 0f && scroll.verticalNormalizedPosition < 1f)
            {
                targetH = _points[target];
                LerpH = true;
            }

            dragInit = true;
            CurrentPoint = target;
        }

        public void OnDrag()
        {
            if (dragInit)
            {
                dragStartNearest = FindNearest(scroll.horizontalNormalizedPosition, _points);
                dragInit = false;
            }

            LerpH = false;
            LerpV = false;
        }

        public void SnapToItem(RectTransform target)
        {
            //integrate scroll to item logic later if needed
        }

        private int FindNearest(float f, float[] array)
        {
            float distance = Mathf.Infinity;
            int output = 0;
            for (int index = 0; index < array.Length; index++)
            {
                if (Mathf.Abs(array[index] - f) < distance)
                {
                    distance = Mathf.Abs(array[index] - f);
                    output = index;
                }
            }
            return output;
        }
    }
}
