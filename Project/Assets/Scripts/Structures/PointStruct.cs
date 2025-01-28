using System;

namespace Mytypes
{
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
