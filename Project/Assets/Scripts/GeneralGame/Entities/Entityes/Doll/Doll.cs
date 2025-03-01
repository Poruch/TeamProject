using Assets.Scripts.GeneralGame.Entities.Creatures;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
/// <summary>
/// Сущность кукла, регистрирует попадания по себе
/// </summary>
public class Doll : Entity
{
    public override void Collide()
    {
        Debug.Log("Кукла атакована");
    }
    
}
