
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    //Класс для получения ввода для управления кораблем
    internal class PlayerInput
    {
        //Действие во время нажатия на клавишу атаки
        UnityEvent onAttack = new UnityEvent();
        //Действия происходящие во время зажатия клавиши
        UnityEvent inAttack = new UnityEvent();
        //Действие после зажатия атаки
        UnityEvent afterAttack = new UnityEvent();

        //Действие при начале движения
        UnityEvent onStartMove = new UnityEvent();
        //Действие при движении
        UnityEvent inMove = new UnityEvent();

        PlayerControls control;
        public PlayerInput()
        {
            control = new PlayerControls();
            control.Enable();
            control.Movement.Move.performed += MovePerformed;            
        }

        private void MovePerformed(InputAction.CallbackContext obj)
        {
            OnStartMove.Invoke();
        }





        public Vector2 Direction
        {
            get => control.Movement.Move.ReadValue<Vector2>();            
        }

        public UnityEvent OnStartMove => onStartMove;

        public UnityEvent OnAttack { get => onAttack; set => onAttack = value; }
        public UnityEvent InAttack { get => inAttack; set => inAttack = value; }
        public UnityEvent AfterAttack { get => afterAttack; set => afterAttack = value; }
        public UnityEvent InMove { get => inMove; set => inMove = value; }

        bool isSwitch = false;
        public void Update()
        {
            if (control.Movement.Move.IsPressed())
            {
                InMove.Invoke();
            }
            if (control.Iteract.Attack.IsPressed())
            { 
                if(!isSwitch)
                    OnAttack.Invoke();
                isSwitch = true;
                InAttack.Invoke();
            }
            else if (isSwitch)
            {
                isSwitch = false;
                AfterAttack.Invoke();
            }
        }




    }
}
