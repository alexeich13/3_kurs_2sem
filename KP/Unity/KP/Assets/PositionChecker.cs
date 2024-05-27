using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    // Родительский объект, содержащий части пистолета
    public Transform parentObject;

    void Start()
    {
        // Проверка наличия родительского объекта
        if (parentObject == null)
        {
            Debug.LogError("Parent object is not assigned.");
            return;
        }

        // Перебор всех дочерних объектов родительского объекта
        foreach (Transform child in parentObject)
        {
            // Вывод локальной позиции дочернего объекта с точностью до 6 знаков после запятой
            Debug.Log("Local Position of " + child.name + ": " + child.localPosition.ToString("F6"));

            // Вывод мировой позиции дочернего объекта с точностью до 6 знаков после запятой
            Debug.Log("World Position of " + child.name + ": " + child.position.ToString("F6"));
        }
    }
}
