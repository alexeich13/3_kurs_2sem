using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorChanger : MonoBehaviour
{
    public GameObject gameObject1;
    public Camera mainCamera;
    private bool isPaused = false;

    private FirstPersonController firstPersonController;
    private MonoBehaviour[] scripts;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = gameObject1.GetComponent<FirstPersonController>();
        scripts = gameObject1.GetComponents<MonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            ToggleControl(isPaused);
        }
    }
    void ToggleControl(bool paused)
    {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            Cursor.visible = true; // Show cursor

            // Disable character control components
            firstPersonController.enabled = false;
            foreach (var script in scripts)
            {
                script.enabled = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            Cursor.visible = false; // Hide cursor

            // Enable character control components
            firstPersonController.enabled = true;
            foreach (var script in scripts)
            {
                script.enabled = true;
            }
        }
    }
}
