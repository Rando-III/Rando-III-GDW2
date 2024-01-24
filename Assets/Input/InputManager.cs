using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static InputActions _input;

    public static void Init(Player myPlayer)
    {
        _input = new InputActions();

        _input.Permanent.Enable();

        _input.InGame.LeftRight.performed += mtx =>
        {
            myPlayer.SetMovementDirection(mtx.ReadValue<Vector2>());
        };
        _input.InGame.Jump.performed += mtx =>
        {
            myPlayer.Jump();
        };
    }

    public static void SetGameControls()
    {
        _input.InGame.Enable();
        _input.UI.Disable();
    }
    public static void SetUIControls()
    {
        _input.UI.Enable();
        _input.InGame.Enable();
    }

}
