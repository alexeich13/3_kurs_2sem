using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover1 : MonoBehaviour
{
    public Transform target1; // Первая целевая позиция и поворот камеры
    public Transform target2; // Вторая целевая позиция и поворот камеры
    public float speed = 2.0f; // Скорость движения камеры
    public float positionThreshold = 0.1f; // Допустимое расстояние до целевой позиции
    public float rotationThreshold = 1.0f; // Допустимый угол поворота до целевой ориентации

    private bool moveToTarget1 = false; // Флаг для начала движения к первой цели
    private bool moveToTarget2 = false; // Флаг для начала движения ко второй цели

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (moveToTarget1)
        {
            MoveCamera(target1);
        }
        else if (moveToTarget2)
        {
            MoveCamera(target2);
        }
    }

    void MoveCamera(Transform target)
    {
        // Плавное перемещение камеры к целевым координатам
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * 5 * Time.deltaTime);

        // Плавный поворот камеры к целевому повороту
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed * 100 * Time.deltaTime);

        // Логи для отладки
        Debug.Log("Current Position: " + transform.position + ", Target Position: " + target.position);
        Debug.Log("Current Rotation: " + transform.rotation.eulerAngles + ", Target Rotation: " + target.rotation.eulerAngles);

        // Проверка на достижение цели
        if (Vector3.Distance(transform.position, target.position) < positionThreshold && 
            Quaternion.Angle(transform.rotation, target.rotation) < rotationThreshold)
        {
            moveToTarget1 = false; // Останавливаем движение к первой цели
            moveToTarget2 = false; // Останавливаем движение ко второй цели
            Debug.Log("Camera reached the target position and rotation.");
        }
    }

    public void StartMovingToTarget1()
    {
        moveToTarget1 = true; // Начинаем движение к первой цели
        moveToTarget2 = false; // Останавливаем движение ко второй цели, если было
    }

    public void StartMovingToTarget2()
    {
        moveToTarget2 = true; // Начинаем движение ко второй цели
        moveToTarget1 = false; // Останавливаем движение к первой цели, если было
    }
}
