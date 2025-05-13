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
        GeneralGameUi generalGameUi;
        UnityEvent onPause = new UnityEvent();
        UnityEvent onPauseExit = new UnityEvent();


        public UnityEvent OnPause { get => onPause; set => onPause = value; }
        public UnityEvent OnPauseExit { get => onPauseExit; set => onPauseExit = value; }

        public UiInput(GeneralGameUi general)
        {

            generalGameUi = general;
            control = new UiInputControl();
            control.Menu.Pause.performed += PausePerformed;
            control.Menu.Manual.performed += ManualPerformed;
            control.Enable();
        }

        private void ManualPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            string commandText = @"Manual,_Катаргин,_Киладзе,_Мальшаков_РИС24_4.chm";
	        var proc = new System.Diagnostics.Process();
 	        proc.StartInfo.FileName = commandText;
 	        proc.StartInfo.UseShellExecute = true;
 	        proc.Start();
            PausePerformed(obj);
        }

        private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (!generalGameUi.IsPause)
            {
                OnPause.Invoke();
                generalGameUi.IsPause = true;
            }
            else
            {
                OnPauseExit.Invoke();
                generalGameUi.IsPause = false;
            }         
        }



    }
}
