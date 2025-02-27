using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private void Update()
    {
        Vector2 direction = Vector2.right;

        if (Input.GetMouseButtonDown(0))
        {
            var instance = Instantiate(bullet,transform.position + Vector3.right,Quaternion.identity);
            
        }
    }
}
