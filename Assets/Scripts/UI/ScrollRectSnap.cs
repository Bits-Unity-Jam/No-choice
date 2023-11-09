using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ScrollRectSnap : MonoBehaviour
    {
        [SerializeField] private int itemsInContent = 1;
        [SerializeField] private float snapSpeed;
        [SerializeField] private float inertiaCutoffMagnitude;

        private float[] _points;
        private float stepSize;
        [SerializeField]
        private ScrollRect scroll;
        private bool LerpH;
        private float targetH;
        [SerializeField] private bool snapInH = true;
        private bool LerpV;
        private float targetV;
        [SerializeField] private bool snapInV = true;
        private bool dragInit = true;
        private int dragStartNearest;
        private int _currentPoint;
        private bool _isDragging = false;
        [Header("Button")] [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private AudioSource sweepSound;

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
            nextButton?.onClick.AddListener(SetNext);
            previousButton?.onClick.AddListener(SetPrevious);
        }

        private void Start()
        {
            InitializeScroll();
            SetInteractable();
            targetH = 0;
            targetV = 0;
            scroll.horizontalNormalizedPosition = 0;
            scroll.verticalNormalizedPosition = 0;
        }

        private void InitializeScroll()
        {
            scroll ??= gameObject.GetComponent<ScrollRect>();
            gameObject.GetComponent<RectTransform>();
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
                _points = new float[1];
                _points[0] = 0;
            }
        }

        public void SetNext()
        {
            HandleSetAction(1);
        }

        public void SetPrevious()
        {
            HandleSetAction(-1);
        }

        private void HandleSetAction(int direction)
        {
            sweepSound.Play();
            CurrentPoint = FindNearest(scroll.horizontalNormalizedPosition, _points);
            SetPoint(CurrentPoint + direction);
        }

        public void SetPoint(int point)
        {
            int targetPoint = Mathf.Clamp(point, 0, _points.Length - 1);

            if (scroll.horizontal && snapInH && scroll.horizontalNormalizedPosition is > -0.001f and <= 1.001f)
            {
                targetH = _points[targetPoint];
                LerpH = true;
            }

            if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition is >= -0.001f and <= 1.001f)
            {
                targetV = _points[targetPoint];
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
                scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, targetH,
                    snapSpeed * Time.deltaTime);
                if (Mathf.Approximately(scroll.horizontalNormalizedPosition, targetH)) LerpH = false;
            }

            if (LerpV)
            {
                scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetV,
                    snapSpeed * Time.deltaTime);
                if (Mathf.Approximately(scroll.verticalNormalizedPosition, targetV)) LerpV = false;
            }
        }

        public void DragEnd()
        {
            int target = FindNearest(scroll.horizontalNormalizedPosition, _points);

            if (target == dragStartNearest &&
                scroll.velocity.sqrMagnitude > inertiaCutoffMagnitude * inertiaCutoffMagnitude)
            {
                target = Mathf.Clamp((scroll.velocity.x < 0) ? dragStartNearest + 1 : dragStartNearest - 1, 0, _points.Length - 1);
            }

            if (scroll.horizontal)
            {
                if (snapInH && scroll.horizontalNormalizedPosition is > 0f and < 1f)
                {
                    targetH = _points[target];
                    LerpH = true;
                }
            }

            if (scroll.vertical)
            {
                if (snapInV && scroll.verticalNormalizedPosition is > 0f and < 1f)
                {
                    targetV = _points[target];
                    LerpH = true;
                }
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
