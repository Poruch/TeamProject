using Assets.Scripts.GeneralGame.Entities.Creatures;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
/// <summary>
/// �������� �����, ������������ ��������� �� ����
/// </summary>
public class Doll : Entity
{
    public override void Collide()
    {
        Debug.Log("����� ���������");
    }
    
}
