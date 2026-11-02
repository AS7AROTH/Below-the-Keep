using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (animator == null || characterController == null)
            return;

        Vector3 velocity = characterController.velocity;
        velocity.y = 0;

        animator.SetFloat("Speed", velocity.magnitude);
    }
}