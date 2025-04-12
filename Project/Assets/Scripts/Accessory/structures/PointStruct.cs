using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;

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
        public readonly float MaxPoint;
        public float currentPoint;
        public void Reduce(float count)
        {
            currentPoint = Math.Clamp(currentPoint - MathF.Abs(count), 0, MaxPoint);
        }
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

    }
}
