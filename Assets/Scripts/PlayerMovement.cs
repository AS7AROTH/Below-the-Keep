
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float runSpeed = 8f;

    [Header("Salto y gravedad")]
    public float gravity = -9.81f;
    public float jumpForce = 6f;

    [Header("Estado del jugador")]
    public bool hasKey = false;

    // Componentes del jugador
    private CharacterController controller;
    private Animator animator;

    // Velocidad vertical para el salto y la gravedad
    private float verticalVelocity;

    // Indica si el jugador está en el aire
    private bool isJumping;

    void Start()
    {
        // Obtiene el CharacterController del objeto Player
        controller = GetComponent<CharacterController>();

        // Busca el Animator del personaje, incluso si está en un objeto hijo
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // -----------------------------------------
        // 1. Leer las teclas de movimiento
        // -----------------------------------------

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y = 1;

        if (Keyboard.current.sKey.isPressed)
            input.y = -1;

        if (Keyboard.current.aKey.isPressed)
            input.x = -1;

        if (Keyboard.current.dKey.isPressed)
            input.x = 1;


        // -----------------------------------------
        // 2. Movimiento relativo a la cámara
        // -----------------------------------------

        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 movement = forward * input.y + right * input.x;

        // Evita moverse más rápido en diagonal
        if (movement.magnitude > 1)
            movement.Normalize();


        // -----------------------------------------
        // 3. Caminar y correr
        // -----------------------------------------

        bool isMoving = movement.magnitude > 0.1f;

        // Shift izquierdo permite correr mientras se mueve
        bool isRunning = isMoving &&
                         Keyboard.current.leftShiftKey.isPressed;

        // Selecciona la velocidad según el estado
        float currentSpeed = isRunning ? runSpeed : speed;

        movement *= currentSpeed;


        // -----------------------------------------
        // 4. Actualizar las animaciones
        // -----------------------------------------

        if (animator != null)
        {
            // 0 = quieto, 1 = caminar, 2 = correr
            float animationSpeed = 0f;

            if (isMoving)
                animationSpeed = isRunning ? 2f : 1f;

            animator.SetFloat("Speed", animationSpeed);

            // Activa la animación de correr
            animator.SetBool("IsRunning", isRunning);
        }


        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }


        // -----------------------------------------
        // 5. Salto y gravedad
        // -----------------------------------------

        if (controller.isGrounded)
        {
            // Mantiene al jugador pegado al suelo
            verticalVelocity = -2f;

            // Salta al presionar Espacio
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = jumpForce;
                isJumping = true;
            }
        }
        else
        {
            // La gravedad aumenta la velocidad hacia abajo
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Actualiza el parámetro de la animación de salto
        if (animator != null)
            animator.SetBool("IsJumping", isJumping);


        // -----------------------------------------
        // 6. Aplicar movimiento vertical
        // -----------------------------------------

        movement.y = verticalVelocity;

        // Mueve al jugador teniendo en cuenta el tiempo entre frames
        controller.Move(movement * Time.deltaTime);

        // Al aterrizar, desactiva el estado de salto
        if (controller.isGrounded && verticalVelocity <= 0)
        {
            isJumping = false;
        }
    }
}
