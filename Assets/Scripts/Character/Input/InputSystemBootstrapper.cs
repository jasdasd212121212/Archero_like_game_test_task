using UnityEngine;

public class InputSystemBootstrapper : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    private void Awake()
    {
        Boot();
    }

    private void Boot()
    {
        InputSystemPhasade.Initialize(_joystick);
    }
}