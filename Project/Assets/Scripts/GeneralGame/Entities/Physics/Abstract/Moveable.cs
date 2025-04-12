using UnityEngine;
using MyTypes;
namespace Assets.Scripts.GeneralGame.Entities.Physics.Abstract
{

/// <summary>
/// Класс для движущихся объектов
/// </summary>
    public class Moveable :Entity
    {
        [SerializeField]
        Vector2 dir;
        [SerializeField]
        private PointStruct speed = new PointStruct(1.0f);
        public Vector2 Dir { get => dir; set => dir = value; }
        public PointStruct Speed { get => speed; set => speed = value; }

        /// <summary>
        /// Действие между тем как посчитался вектор следующего движения и добавления его к позиции
        /// </summary>
        protected virtual void AddFixedUpdate()
        {

        }
        protected Vector2 move;
        private void FixedUpdate()
        {
            move = Dir * Speed.currentPoint * 0.02f;
            AddFixedUpdate();
            Position += move;
        }
    }

   
}
