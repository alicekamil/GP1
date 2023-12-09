using UnityEngine;
using UnityEngine.InputSystem;

namespace GP1.Gameplay
{
    [RequireComponent(typeof(Character))]
    public class UserInput : MonoBehaviour
    {
        public bool Locked
        {
            get => _inputLocked;
            set
            {
                _inputLocked = value;
                if (_inputLocked)
                {
                    _character.SetInput(CharacterInput.Empty);
                }
            }
        }

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        // Callbacks
        private void OnMove(InputValue value)
        {
            if (_inputLocked)
                return;
            _input.Move = value.Get<float>();
            _character.SetInput(_input);
        }
        
        [SerializeField]
        private bool _inputLocked = true;

        private Character _character;
        private CharacterInput _input;
    }
}