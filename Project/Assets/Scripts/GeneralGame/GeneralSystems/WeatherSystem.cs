using Assets.Scripts.Accessory;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    //Система для смены погоды
    internal class WeatherSystem
    {
        Timer meteorTimer = TimeManager.Instance.CreateTimer(4);
        Timer bonus = TimeManager.Instance.CreateTimer(8);
        GameObject meteor = null;
        BonusManager bonusManager = null;
        public WeatherSystem(BonusManager manager)
        {
            meteor = (GameObject)Resources.Load("Prefabs/Meteor");
            if (meteor == null)
                Debug.Log("Error miss meteor prefab");
            bonusManager = manager;
        }
        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// Здесь должно быть что то другое, но пока так
        /// 
        /// 
        /// 
        /// </summary>

        public void Update()
        {
            if (meteorTimer.IsTime)
            {
                var met = GameObject.Instantiate(meteor, new Vector3(10, Random.Range(-6, 6), 0), Quaternion.identity).GetComponent<PhysicsBullet>();
                met.Dir = Vector2.left;
                Destroyer.Instance.Destroy(met.gameObject, TimeManager.Instance.CreateTimer(GameManager.AreaWidth / met.Speed.MaxPoint));
            }
            if (bonus.IsTime)
            {
                bonusManager.CreateBonus(new Vector3(10, Random.Range(-6, 6), 0));
            }
        }
    }
}
