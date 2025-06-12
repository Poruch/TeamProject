using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine.Events;
namespace MyTypes
{
    /// <summary>
    /// Структура для хранения максимального текущего значения (например, ХП)
    /// </summary>
    [Serializable]
    public class PointStruct
    {
        public PointStruct(float maxPoint)
        {
            MaxPoint = maxPoint;
            currentPoint = MaxPoint;
        }
        [NonSerialized]
        UnityEvent onEmpty = new UnityEvent(); 
        [NonSerialized]
        UnityEvent<float, float> onValueChange = new UnityEvent<float, float>();
        public readonly float MaxPoint;
        public float currentPoint;

        public UnityEvent OnEmpty { get => onEmpty; set => onEmpty = value; }
        public UnityEvent<float, float> OnValueChange { get => onValueChange; set => onValueChange = value; }

        /// <summary>
        /// Fall point on absolute value
        /// </summary>
        /// <param name="count"></param>
        public void Reduce(float count)
        {
            float current = currentPoint;
            currentPoint -= MathF.Abs(count);
            OnValueChange.Invoke(currentPoint, currentPoint - current);
            if (currentPoint <= 0)
            {
                currentPoint = 0;
                OnEmpty.Invoke();
            }
        }

        /// <summary>
        /// Grow point on absolute value
        /// </summary>
        /// <param name="count"></param>
        public void Increase(float count)
        {
            float current = currentPoint;
            currentPoint = Math.Clamp(currentPoint + MathF.Abs(count), 0, MaxPoint);
            OnValueChange.Invoke(currentPoint,currentPoint - current);
        }
        public void Reset()
        {
            currentPoint = 0;
            OnValueChange.Invoke(0, currentPoint);
        }
        public void Recover()
        {
            OnValueChange.Invoke(MaxPoint - currentPoint, MaxPoint);
            currentPoint = MaxPoint;
        }
        public bool isEmpty()
        {
            return currentPoint == 0;
        }
        public float GetRatio()
        {
            return currentPoint / MaxPoint;
        }


    }
}
