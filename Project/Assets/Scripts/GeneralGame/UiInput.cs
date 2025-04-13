using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Events;

namespace Assets.Scripts.GeneralGame
{
    internal class UiInput
    {
        UiInputControl control;

        UnityEvent onPause = new UnityEvent();
        UnityEvent onPauseExit = new UnityEvent();


        public UnityEvent OnPause { get => onPause; set => onPause = value; }
        public UnityEvent OnPauseExit { get => onPauseExit; set => onPauseExit = value; }

        public UiInput()
        {
            control = new UiInputControl();
            control.Menu.Pause.performed += PausePerformed;
            control.Enable();
        }

        bool paused = false;
        private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (!paused)
            {
                OnPause.Invoke();
                paused = true;
            }
            else
            {
                OnPauseExit.Invoke();
                paused = false;
            }         
        }


    }
}
