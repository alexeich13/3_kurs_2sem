using UnityEngine;
using UnityEngine.UI;

public class GunDisassembly : MonoBehaviour
{
    public Transform[] parts; // Массив частей пистолета
    public Vector3[] targetPositions; // Массив целевых позиций для перемещения частей
    public float moveSpeed = 2.0f; // Скорость перемещения части
    private int currentPartIndex = 0; // Текущий индекс части, которую нужно разобрать
    private bool isMoving = false; // Флаг для проверки движения
    public Text displayText;
    public string[] stepsDescription; 
    void Start()
    {
        HighlightNextPart(); 
        UpdateDisplayText(); 
    }
    void Update()
    {
        if (isMoving && currentPartIndex < parts.Length)
        {
            Transform part = parts[currentPartIndex];
            Vector3 target = targetPositions[currentPartIndex];
            part.position = Vector3.Lerp(part.position, target, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(part.position, target) < 0.01f)
            {
                part.position = target; 
                isMoving = false; 
                currentPartIndex++; 
                HighlightNextPart(); 
                UpdateDisplayText(); 
            }
        }
    }
    public void HighlightNextPart()
    {
        if (currentPartIndex < parts.Length)
        {
            Renderer renderer = parts[currentPartIndex].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
        }
    }
    public void OnPartClicked(Transform part)
    {
        if (!isMoving && part == parts[currentPartIndex])
        {
            Renderer renderer = part.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }
            isMoving = true;
        }
    }
    private void UpdateDisplayText()
    {
        if (displayText != null)
        {
            string displayString = "Разборка/сборка пистолета:\n";
            if (currentPartIndex < stepsDescription.Length)
            {
                displayString += stepsDescription[currentPartIndex] + "\n";
            }
            displayText.text = displayString;
        }
    }
}
