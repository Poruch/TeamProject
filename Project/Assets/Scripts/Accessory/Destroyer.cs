using MyTypes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Accessory
{
    public class Destroyer : MonoBehaviour
    {
        int countInstanse = 0;
        static Destroyer instance;
        public static Destroyer Instance { get => instance; }

        Dictionary<GameObject,Timer> destroyed = new Dictionary<GameObject,Timer>();

        private void Awake()
        {
            countInstanse++;
            if (instance == null)
                instance = this;
            if (countInstanse > 1)
                Debug.LogWarning("Destroyer should not be more one");
        }
        private void Start()
        {
            
        }
        public void Destroy(GameObject gameObject, Timer timer)
        {
            destroyed.Add(gameObject, timer);
        }

        private void Update()
        {
            var keys = destroyed.Keys.ToArray();
            foreach (GameObject @object in keys) 
            {
                if (destroyed[@object].IsTime)
                {
                    GameObject.Destroy(@object);
                    destroyed.Remove(@object);
                }
            }
            foreach (Timer timer in destroyed.Values)
            {                               
                timer.Tick();
            }
        }


    }
}
