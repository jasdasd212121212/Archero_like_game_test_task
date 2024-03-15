using System;
using UnityEngine;

public static class InputSystemPhasade
{
    private static bool _initialized = false;

    private static KeyboardInputSystem _keyboard;
    private static JoystickInputSystem _joystick;

    public static IInput CurrentInputSystem
    {
        get
        {
            if (_initialized == false)
            {
                Debug.LogError("InputSystemPhasade is not initialized");
                return null;
            }

            if (_keyboard.GetInputVector() != Vector2.zero)
            {
                return _keyboard;
            }
            else
            {
                return _joystick;
            }
        }

        private set => throw new NotImplementedException();
    }

    public static void Initialize(Joystick joystick)
    {
        _keyboard = new KeyboardInputSystem();
        _joystick = new JoystickInputSystem(joystick);  
        
        _initialized = true;
    }
}