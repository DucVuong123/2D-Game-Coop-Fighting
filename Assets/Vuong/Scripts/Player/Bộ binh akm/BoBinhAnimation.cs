using UnityEngine;

public class BoBinhAnimation : PlayerAnimationBase
{
    protected override void Start()
    {
        base.Start();
        animator_Name = new() { "isAttacking", "isJumping", "isIdling", "isRunning" };
    }
    protected override void CheckAnimation()
    {
        if (!player_Movement.IsGrounded)
        {
            if (player_Attack.isAttack)
                SetAnimation("isAttacking");
            else
                SetAnimation("isJumping");
        }
        else if (player_Movement.IsGrounded)
        {
            if (player_Attack.isAttack)
                SetAnimation("isAttacking");
            else if (!player_Movement.isRightAndLeftDown() && player_Movement.isRightOrLeftDown())
                SetAnimation("isRunning");
            else
                SetAnimation("isIdling");
        }
    }
}
