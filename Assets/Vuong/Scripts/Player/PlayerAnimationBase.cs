using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimationBase : MonoBehaviour
{
    [Header("Animation")]
    protected Animator animator;
    [SerializeField] protected List<string> animator_Name;

    [Header("Other")]
    protected PlayerMovementBase player_Movement;
    protected PlayerAttackBase player_Attack;
    protected PlayerSpecialSkillBase player_Skill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player_Attack = GetComponent<PlayerAttackBase>();
        player_Movement = GetComponent<PlayerMovementBase>();
        player_Skill = GetComponent<PlayerSpecialSkillBase>();
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckAnimation();
    }
    protected abstract void CheckAnimation();
    protected void SetAnimation(string Name)
    {
        animator.SetBool(Name, true);
        foreach (var s in animator_Name)
            if (!s.Equals(Name))
            {
                animator.SetBool(s, false);
            }

    }
}
