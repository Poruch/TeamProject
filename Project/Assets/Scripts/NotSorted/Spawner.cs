using MyTypes;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Timer timer;
    [SerializeField] GameObject enemy;

    
    private void Start()
    {
        timer = new Timer(5);
    }
    private void Update()
    {
        if (timer.isTime)
        {
            GameObject.Instantiate(enemy,new Vector3(Random.value*10,Random.value*10,0),Quaternion.identity);
            
        }

        timer.Tick();
    }
}
