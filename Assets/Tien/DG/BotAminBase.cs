using UnityEngine;

public class BaseAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Reset()
    {
        if (!animator) animator = GetComponentInChildren<Animator>();
    }
    public void SetOnlyOneTrue(string trueParam)
    {
        if (!animator) return;

        foreach (var p in animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Bool)
                animator.SetBool(p.name, p.name == trueParam);
        }
    }
}

