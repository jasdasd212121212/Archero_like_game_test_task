using UnityEngine;

public class KeyboardInputSystem : IInput
{
    public Vector2 GetInputVector()
    {
        return new Vector2(Input.GetAxisRaw(Axis.HORIZONTAL), Input.GetAxisRaw(Axis.VERTICAL));
    }
}