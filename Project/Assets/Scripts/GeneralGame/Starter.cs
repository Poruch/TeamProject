using MyTypes;
using UnityEngine;
using Assets.Scripts.GeneralGame;


/// <summary>
/// ������ ������� ����� ���������� �� ����� ��������� ����
/// </summary>
public class Starter : MonoBehaviour
{
    Timer timer = new Timer(4);

    [SerializeField]
    GameObject meteor = null;


    [SerializeField]
    GeneralGameConfig config;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = new GameManager(config);
    }


    /// <summary>
    /// �������� ���� ���� 
    /// </summary>
    private void Update()
    {
        gameManager.Update();
        if(timer.IsTime)
        {
            var met =Instantiate(meteor,new Vector3(10, Random.Range(-6,6), 0),Quaternion.identity).GetComponent<PhysicsBullet>();
            met.dir = Vector2.left;
        }    
        timer.Tick();
    }
}
