using UnityEngine;

public class PartClick : MonoBehaviour
{
    private GunDisassembly gunDisassembly;

    void Start()
    {
        gunDisassembly = FindObjectOfType<GunDisassembly>();
        if (gunDisassembly == null)
        {
            Debug.LogError("GunDisassembly script not found in the scene.");
        }
    }

    void OnMouseDown()
    {
        if (gunDisassembly != null)
        {
            gunDisassembly.OnPartClicked(transform);
        }
    }
}
