using UnityEngine;

public class CarController : MonoBehaviour
{
    public float carSpeed = 10f;
    public float rotationSpeed = 100f;

    private void FixedUpdate() {
        // Двигаем машину с помощью трансформа
        float move = Input.GetAxis("Vertical") * carSpeed;
        transform.Translate(Vector3.forward * move * Time.fixedDeltaTime);

        // Поворачиваем так же
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(Vector3.up * rotate * Time.fixedDeltaTime);
    }
}
    // Этот код будет доработан в будущем
