using _Code_Hub;
using UnityEngine;
using UnityEngine.UI;

namespace _Code__InputController
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private VariableJoystick m_variableJoystick = default;
        [SerializeField] private Button m_fireButton = default;

        private void OnEnable()
        {
            m_fireButton.onClick.AddListener(Fire);
            Hub.onSystemUpdate += JoystickMoved;
        }
        private void OnDisable()
        {
            m_fireButton.onClick.RemoveListener(Fire);
            Hub.onSystemUpdate -= JoystickMoved;
        }

        private void Fire()
        {
            Hub.fire?.Invoke();
        }

        private void JoystickMoved(float time)
        {
            Hub.joysticDirection?.Invoke(m_variableJoystick.Direction, time);
        }

    }
}