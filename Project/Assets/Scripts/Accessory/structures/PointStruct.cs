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
        public readonly float MaxPoint;
        public float currentPoint;

        public UnityEvent OnEmpty { get => onEmpty; set => onEmpty = value; }

        /// <summary>
        /// Fall point on absolute value
        /// </summary>
        /// <param name="count"></param>
        public void Reduce(float count)
        {
            currentPoint -= count;
            if (currentPoint <= 0)
            {
                currentPoint = 0;
                OnEmpty.Invoke();
            }
            
            if(currentPoint > MaxPoint)
                currentPoint = MaxPoint;
        }

        /// <summary>
        /// Grow point on absolute value
        /// </summary>
        /// <param name="count"></param>
        public void Increase(float count)
        {
            currentPoint = Math.Clamp(currentPoint + MathF.Abs(count), 0, MaxPoint);
        }
        public void Reset()
        {
            currentPoint = 0;
        }
        public void Recover()
        {
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
