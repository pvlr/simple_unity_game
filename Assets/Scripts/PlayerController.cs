using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float mouseSensetivity = 2f;
    public float jumpForce = 5f;

    private Rigidbody self_rb;
    private Transform self_tr, camera_tr;

    private bool isGrounded;


    private void Awake() {
        // Кэшируем необходимые компоненты
        self_rb = GetComponent<Rigidbody>();
        self_tr = GetComponent<Transform>();
        camera_tr = Camera.main.GetComponent<Transform>();
    }


    private void Update() {
        // Проверяем нажата ли кнопка "Пробел" и находится ли персонаж на земле
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            // Если да, вызывается функция прыжка
            Jump();
        }
    }


    private void FixedUpdate() {
        // Получаем данные о нажатых кнопках движения (WASD или стрелки)
        float moveH = Input.GetAxis("Horizontal") * playerSpeed;
        float moveV = Input.GetAxis("Vertical") * 0.8f * playerSpeed;
        // Двигаем игрока
        self_rb.velocity = transform.TransformDirection(moveH, self_rb.velocity.y, moveV);

        // Получаем данные о движениях мыши по горизонтали
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity;
        // Камера является дочерним объктом игрока, поэтому вокруг оси Y я поворачиваю его, а камера следует за этим поворотом
        self_tr.Rotate(new Vector3(0f, mouseX, 0f));

        // Получаем данные о движениях мыши по вертикали
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity;
        // Получаем текущий угол наклона камеры по X и корректируем его с учетом ввода с мыши
        float newAngleX = camera_tr.localEulerAngles.x - mouseY;
        // После поворота камеры вверх выше середины, угол становится не отрицательным, а 360. Здесь это исправляется
        if (newAngleX > 180f) {newAngleX -= 360f;}
        // Для того чтобы ограничитьзначение угла, используем Mathf.Clamp()
        newAngleX = Mathf.Clamp(newAngleX, -75, 75);
        // Наконец, поворачиваем камеру
        camera_tr.localEulerAngles = new Vector3(newAngleX, 0f, 0f);
    }


    void Jump() {
        // Добавляем игроку силу, направленную вверх
        self_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        // Он больше не на земле, укажем на это чтобы не было бесконечных прыжков
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision other) {
        // Отслеживаем столкновения персонажа. Когда оно происходит - считаем что он на земле (знаю что неправильно, позже изменю)
        isGrounded = true;
    }
}
