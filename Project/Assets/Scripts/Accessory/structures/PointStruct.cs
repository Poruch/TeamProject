using System;

namespace MyTypes
{
    /// <summary>
    /// Структура для хранения максимального текущего значения (например, ХП)
    /// </summary>
    [Serializable]
    public struct PointSruct 
    {
        public PointSruct(float maxPoint)
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
            currentPoint = MaxPoint;
        }
        public bool isEmpty()
        {
            return currentPoint == 0;
        }
    }
}
