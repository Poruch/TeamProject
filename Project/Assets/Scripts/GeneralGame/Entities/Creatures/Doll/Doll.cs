using Assets.Scripts.GeneralGame.Entities.Creatures;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Doll : MonoBehaviour, IPhysical
{
    public void Collide()
    {
        Debug.Log("Кукла атакована");
    }
    
}
