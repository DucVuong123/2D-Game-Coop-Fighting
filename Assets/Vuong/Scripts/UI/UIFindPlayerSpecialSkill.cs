using UnityEngine;
using UnityEngine.UI;

public class UIFindPlayerSpecialSkill : MonoBehaviour
{
    private PlayerSpecialSkillBase m_Player;
   [SerializeField] private Text skill_Amount_Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Invoke(nameof(FinPlayer_Delay), 0.2f);
    }
    private void Start()
    {
/*        skill_Amount_Text.text = m_Player.skill_Amount.ToString();
        m_Player.OnEnterSkill += M_Player_OnEnterSkill;*/
    }


   private void FinPlayer_Delay()
    {
        m_Player = FindObjectOfType<PlayerSpecialSkillBase>();
        skill_Amount_Text.text = m_Player.skill_Amount.ToString();
        m_Player.OnEnterSkill += M_Player_OnEnterSkill;
    }



    private void M_Player_OnEnterSkill(object sender, System.EventArgs e)
    {
        skill_Amount_Text.text = (sender as PlayerSpecialSkillBase).skill_Amount.ToString();
    }

    public void OnPointerDownSkill()
    {
        m_Player.OnPointerDownSkill();
    }
    public void OnPointerUpSkill()
    {
        m_Player.OnPointerUpSkill();
    }
}
