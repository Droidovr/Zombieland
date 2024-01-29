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
            // ����������� ������ ��������
            transitionTimer += Time.deltaTime;

            // ������� ���������� ��������
            float weight = Mathf.Clamp01(1f - transitionTimer / transitionTime);
            animator.SetLayerWeight(0, weight);

            // ���� ������ ���������� �������, �������� ������� � ��������
            if (transitionTimer >= transitionTime)
            {
                transitioningToRagdoll = true;
                EnableRagdoll();
            }
        }
    }

    void EnableRagdoll()
    {
        // ��������� ��������
        animator.enabled = false;

        // �������� �������
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}
