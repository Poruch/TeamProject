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
        UnityEvent onPauseExite = new UnityEvent();


        public UnityEvent OnPause { get => onPause; set => onPause = value; }
        public UnityEvent OnPauseExite { get => onPauseExite; set => onPauseExite = value; }

        public UiInput()
        {
            control = new UiInputControl();
            control.Menu.Pause.performed += PausePerformed;
            control.Enable();
        }

        bool paused = false;
        bool isPressDown = true;
        private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            //if (isPressDown)
            //{
                if (!paused)
                {
                    OnPause.Invoke();
                    paused = true;
                }
                else
                {
                    OnPauseExite.Invoke();
                    paused = false;
                }                
                //isPressDown = false;
            //}
            //else
            //{
            //    isPressDown = true;
            //}
        }


    }
}
