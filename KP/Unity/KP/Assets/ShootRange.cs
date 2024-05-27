using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRange : MonoBehaviour
{
    public GameObject gameObject1;
    public Camera mainCamera;
    private bool isCameraEnabled;
    private CursorLockMode initialCursorLockState;
    private bool initialCursorVisible;

    // Start is called before the first frame update
    void Start()
    {
        // Save initial cursor states
        initialCursorLockState = Cursor.lockState;
        initialCursorVisible = Cursor.visible;

        // Ensure the main camera is enabled at the start
        mainCamera.enabled = true;
        isCameraEnabled = true;
        gameObject1.SetActive(false);

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ToggleCam()
    {
        isCameraEnabled = false;
        mainCamera.enabled = false;
        gameObject1.SetActive(true);

        // Update cursor state only if the camera is disabled
        if (isCameraEnabled)
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ResetCam()
    {
        // Reset camera and cursor to initial states
        mainCamera.enabled = true;
        isCameraEnabled = true;
        gameObject1.SetActive(false);

        Cursor.lockState = initialCursorLockState;
        Cursor.visible = initialCursorVisible;
    }
}
