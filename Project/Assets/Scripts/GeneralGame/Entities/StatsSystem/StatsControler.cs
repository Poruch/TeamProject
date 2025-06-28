using Assets.Scripts.GeneralGame.Entities.StatsSystem;
using MyTypes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class StatsControler : MonoBehaviour
    {
        [SerializeField]
        int currentLevel = 0;
        [SerializeField]
        PointStruct exp = new PointStruct(30, true);
        UnityEvent<int> onLevelGrow = new UnityEvent<int>();
        IDamageable damageable = null;

        public UnityEvent<int> OnLevelGrow { get => onLevelGrow; set => onLevelGrow = value; }
        internal IDamageable Damageable { get => damageable; set => damageable = value; }

        public void SetIDemageable(IDamageable damageable)
        {
            this.Damageable = damageable;
        }
        public void AddExp(float ex)
        {
            int count = 0;
            while (ex != 0 && count < 10)
            {
                count++;
                ex = exp.Increase(ex);
                if (exp.isFull())
                {
                    currentLevel = currentLevel + 1;
                    FloatingTextManager.Instance.CreateFloatingText($"Level up {currentLevel}", new Vector2(0, 0), Color.green);
                    OnLevelGrow.Invoke(currentLevel);
                    exp.SetNewMax(exp.MaxPoint * 1.5f);
                    exp.Reset();
                }
            }
        }

    }
}
