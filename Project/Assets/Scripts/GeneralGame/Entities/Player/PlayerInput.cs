
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    //Класс для получения ввода для управления кораблем
    internal class PlayerInput
    {
        PlayerControls control;
        public PlayerInput()
        {
            control = new PlayerControls();
            control.Enable();
        }
        public Vector2 Direction
        {
            get => control.Movement.Move.ReadValue<Vector2>();            
        }
    }
}
