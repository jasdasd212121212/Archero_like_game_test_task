using UnityEngine;

public class JoystickInputSystem : IInput
{
    private Joystick _joystick;

    public JoystickInputSystem(Joystick joystick) 
    {
        _joystick = joystick;
    }

    public Vector2 GetInputVector()
    {
        return _joystick.Direction;
    }
}