
using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    internal class BonusManager
    {
        //static BonusManager instance;
        //public static BonusManager Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new BonusManager();
        //        return instance;
        //    }
        //}
        public BonusManager(BonusConfig bonusConfig)
        {
            bonuses = bonusConfig.Bonuses;
        }
        GameObject[] bonuses = null;
        public void SetConfig()
        {

        }

        public void CreateBonus(Vector2 position)
        {
            int bonus = Random.Range(0, bonuses.Length);
            var met = GameObject.Instantiate(bonuses[bonus], position, Quaternion.identity).GetComponent<Moveable>();
            Destroyer.Instance.Destroy(met.gameObject, TimeManager.Instance.CreateTimer(GameManager.AreaWidth / met.Speed.MaxPoint));
        }

    }
}
