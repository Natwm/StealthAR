using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraFocus : MonoBehaviour
{
  void Update()
  {
    if(Input.GetMouseButtonDown(0))
    {
      bool focusSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
      if (!focusSet) 
      {
        Debug.Log ("Failed to focus");
      }
    }
  }

}
