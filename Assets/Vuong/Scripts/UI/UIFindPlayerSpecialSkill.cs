using UnityEngine;
using UnityEngine.UI;
using PurrNet;
public class UIFindPlayerSpecialSkill : NetworkBehaviour
{
    private PlayerSpecialSkillBase m_Player;
   [SerializeField] private Text skill_Amount_Text;
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        Invoke(nameof(FinPlayer_Delay), 0);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
  
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
