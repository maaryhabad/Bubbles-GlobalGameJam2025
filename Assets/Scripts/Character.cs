using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            StartBlowing();
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            StopBlowing();
        }
    }

    public void StartBlowing()
    {
        if (animator == null) {
            Debug.LogError("Animator is null");
        }
        animator.Play("blowing");
    }

    public void StopBlowing()
    {
        if (animator == null) {
            Debug.LogError("Animator is null");
        }

        // Se o personagem estiver no último frame da animação de sopro, ele pode tocar a animação idle
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            animator.Play("idle");
            return;
        }
    }
}
