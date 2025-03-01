
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




        PlayerControls control;
        public PlayerInput()
        {
            control = new PlayerControls();
            control.Enable();
            //control.Iteract.Attack.performed += Attack;
        }

        public Vector2 Direction
        {
            get => control.Movement.Move.ReadValue<Vector2>();            
        }

        public UnityEvent OnAttack { get => onAttack; set => onAttack = value; }
        public UnityEvent InAttack { get => inAttack; set => inAttack = value; }
        public UnityEvent AfterAttack { get => afterAttack; set => afterAttack = value; }

        private void Attack(InputAction.CallbackContext context)
        {
            OnAttack.Invoke();
        }

        bool isSwitch = false;
        public void Update()
        {

            if (control.Iteract.Attack.inProgress)
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
