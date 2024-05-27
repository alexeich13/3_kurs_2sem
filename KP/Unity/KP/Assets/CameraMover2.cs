using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover2 : MonoBehaviour
{
    public Transform target; // Целевое положение и поворот камеры
    public float speed = 2.0f; // Скорость движения камеры
    public bool moveToTarget = false; // Флаг для начала движения
    public float positionThreshold = 0.1f; // Допустимое расстояние до целевой позиции
    public float rotationThreshold = 1.0f; // Допустимый угол поворота до целевой ориентации

    void Update()
    {
        if (moveToTarget)
        {
            // Плавное перемещение камеры к целевым координатам
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Плавный поворот камеры к целевому повороту
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed * 100 * Time.deltaTime);

            // Логи для отладки
            Debug.Log("Current Position: " + transform.position + ", Target Position: " + target.position);
            Debug.Log("Current Rotation: " + transform.rotation.eulerAngles + ", Target Rotation: " + target.rotation.eulerAngles);

            // Проверка на достижение цели
            if (Vector3.Distance(transform.position, target.position) < positionThreshold && 
                Quaternion.Angle(transform.rotation, target.rotation) < rotationThreshold)
            {
                moveToTarget = false; // Останавливаем движение
                Debug.Log("Camera reached the target position and rotation.");
            }
        }
    }

    public void StartMovingToTarget()
    {
        moveToTarget = true; // Начинаем движение к цели
    }
}
