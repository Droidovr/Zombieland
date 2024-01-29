using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;
    private float transitionTimer = 0f;
    private bool transitioningToRagdoll = false;

    void Update()
    {
        if (animator.enabled && !transitioningToRagdoll)
        {
            // Увеличиваем таймер перехода
            transitionTimer += Time.deltaTime;

            // Плавное выключение анимации
            float weight = Mathf.Clamp01(1f - transitionTimer / transitionTime);
            animator.SetLayerWeight(0, weight);

            // Если прошло достаточно времени, начинаем переход к рэгдоллу
            if (transitionTimer >= transitionTime)
            {
                transitioningToRagdoll = true;
                EnableRagdoll();
            }
        }
    }

    void EnableRagdoll()
    {
        // Отключаем аниматор
        animator.enabled = false;

        // Включаем рэгдолл
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}
