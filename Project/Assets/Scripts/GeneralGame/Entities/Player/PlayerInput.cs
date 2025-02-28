
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class PlayerInput : MonoBehaviour
    {
        PlayerControls control;
        private void Awake()
        {
            control = new PlayerControls();
            control.Enable();
        }
        private void OnEnable()
        {
            control.Enable();
        }
        public Vector2 Direction
        {
            get
            {
                return control.Movement.Move.ReadValue<Vector2>();
            }
        }

        private void Update()
        {

        }

        private void OnDisable()
        {
            control.Disable();
        }
    }
}
