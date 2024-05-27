using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField] // Объект, вокруг которого будет вращаться камера
    float rotationSpeed = 5f;
    [SerializeField] // Скорость вращения камеры
    float zoomSpeed = 5f;
    [SerializeField] // Скорость приближения/удаления камеры
    float minZoomDistance = 1f;
    [SerializeField] // Минимальное расстояние приближения камеры
    float maxZoomDistance = 10f; // Максимальное расстояние приближения камеры
    private float currentZoomDistance; // Текущее расстояние от камеры до цели
    private float initialRotationY;

    void Start()
    {
        currentZoomDistance = Vector3.Distance(transform.position, target.position);
        initialRotationY = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        // Вращение камеры при нажатии правой кнопки мыши
        if (Input.GetMouseButton(1))
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            float newRotationY = transform.rotation.eulerAngles.y + (rotationSpeed * horizontalInput);

            // Проверяем, чтобы угол вращения не выходил за пределы определенного диапазона
            if (Mathf.Abs(newRotationY - initialRotationY) <= 65f)
            {
                transform.RotateAround(target.position, Vector3.up, rotationSpeed * horizontalInput);
            }
        }

        // Приближение/удаление камеры через колесо мыши
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoomDistance -= scrollInput * zoomSpeed;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

        // Обновление позиции камеры
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * currentZoomDistance;
    }
}
