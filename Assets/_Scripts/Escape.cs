using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class Escape : MonoBehaviour
{
  public PlayerInput playerInput;
  private InputAction escapeAction;
  void escapeInput(InputAction.CallbackContext inn)
  {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;  
    #endif
    Application.Quit();
  }
    void OnEnable()
    {
        if (playerInput != null)
        {
            escapeAction = playerInput.actions.FindAction("Escape");
            escapeAction.performed += escapeInput;
            escapeAction.canceled += escapeInput;
        }
    }
    void OnDisable()
    {
        if (playerInput != null)
        {
            escapeAction = playerInput.actions.FindAction("Escape");
            escapeAction.performed -= escapeInput;
            escapeAction.canceled -= escapeInput;
        }
    }
}
