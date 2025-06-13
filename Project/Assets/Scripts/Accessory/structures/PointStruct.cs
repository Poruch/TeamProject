using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
namespace MyTypes
{
    /// <summary>
    /// ��������� ��� �������� ������������� �������� �������� (��������, ��)
    /// </summary>
    [Serializable]
    public class PointStruct
    {
        public PointStruct(float maxPoint)
        {
            MaxPoint = maxPoint;
            CurrentPoint = MaxPoint;
        }
        [NonSerialized]
        UnityEvent onEmpty = new UnityEvent(); 
        [NonSerialized]
        UnityEvent<float, float> onValueChange = new UnityEvent<float, float>();
        [SerializeField]
        float maxPoint = 0;
        public float MaxPoint { get => maxPoint; private set => maxPoint= value; }
        [SerializeField]
        float cccurrentPoint = 0;
        public float CurrentPoint { get => cccurrentPoint; private set => cccurrentPoint = value; }

        public UnityEvent OnEmpty { get => onEmpty; set => onEmpty = value; }
        public UnityEvent<float, float> OnValueChange { get => onValueChange; set => onValueChange = value; }

        /// <summary>
        /// Fall point on absolute value
        /// </summary>
        /// <param name="count"></param>
        public void Reduce(float count)
        {
            float current = CurrentPoint;
            CurrentPoint -= MathF.Abs(count);
            OnValueChange.Invoke(CurrentPoint, CurrentPoint - current);
            if (CurrentPoint <= 0)
            {
                CurrentPoint = 0;
                OnEmpty.Invoke();
            }
        }

        /// <summary>
        /// Grow point on absolute value
        /// </summary>
        /// <param name="count"></param>
        public void Increase(float count)
        {
            float current = CurrentPoint;
            CurrentPoint = Math.Clamp(CurrentPoint + MathF.Abs(count), 0, MaxPoint);
            OnValueChange.Invoke(CurrentPoint,CurrentPoint - current);
        }
        public void Reset()
        {
            CurrentPoint = 0;
            OnValueChange.Invoke(0, CurrentPoint);
        }
        public void Recover()
        {
            OnValueChange.Invoke(MaxPoint - CurrentPoint, MaxPoint);
            CurrentPoint = MaxPoint;
        }
        public bool isEmpty()
        {
            return CurrentPoint == 0;
        }
        public float GetRatio()
        {
            return CurrentPoint / MaxPoint;
        }


    }
}
