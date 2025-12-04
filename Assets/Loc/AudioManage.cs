using UnityEngine;

public class AudioManage : MonoBehaviour
{
    [SerializeField]
    public AudioSource
    audioSource_Ak,
    audioSource_scene,
    audioSource_Ak_run,
    audioSource_AK_jump,
    audioSource_canchien_slice,
    audioSource_invi,
    audioSource_canchien_run,
    audioSource_canchien_jump,
    audioSource_tank_move,
    audioSource_tank_shoot,
    audioSource_tank_explode,
    audioSource_heli_move,
    audioSource_heli_shoot,
    audioSource_heli_explode,
    AudioSource_gunner_idle_shoot,
    
    AudioSource_gunner_move,
    AudioSource_gunner_move_shoot,
    
    audioSource_boss2_run,
    audioSource_boss2_callReinforce,
    audioSource_boss1_teleport,
    audioSource_boss1_callReinforce,
    audioSource_boss1_shoot;



    public void Start()
    {
        audioSource_scene.Play(); 
        
    }
    public void StopScene_audio()
    {
        audioSource_scene.Stop();
    }
    //linh bo binh
    public void StartAudio_Ak_shot()
    {
        audioSource_Ak_run.Stop();
        audioSource_AK_jump.Stop();
        audioSource_Ak.Play();

    }

    public void stopAudio_AK()
    {
        audioSource_Ak.Stop();
    }

    public void startAudioSource_Run()
    {
        audioSource_Ak.Stop();
        audioSource_AK_jump.Stop();
        audioSource_Ak_run.Play();
    }
    public void stopAudioSource_Run()
    {
        audioSource_Ak_run.Stop();
    }
    public void startAudioSource_jump()
    {
        audioSource_Ak.Stop();
        audioSource_Ak_run.Stop();
        audioSource_AK_jump.Play();
    }
    public void stopAudioSource_jump()
    {
        audioSource_AK_jump.Stop();
    }

    //linh can chien
    public void StartAudio_canchien_clice()
    {
        audioSource_canchien_run.Stop();
        audioSource_invi.Stop();
        audioSource_canchien_jump.Stop();
        audioSource_canchien_slice.Play();

    }

    public void stopAudio_canchien_slice()
    {
        audioSource_canchien_slice.Stop();
    }

    public void startAudioSource_canchien_Run()
    {
        audioSource_canchien_slice.Stop();
        audioSource_invi.Stop();
        audioSource_canchien_jump.Stop();
        audioSource_canchien_run.Play();
    }
    public void stopAudioSource_canchien_Run()
    {
        audioSource_canchien_run.Stop();
    }
    public void startAudioSource_canchien_jump()
    {
        audioSource_canchien_slice.Stop();
        audioSource_invi.Stop();
        audioSource_canchien_run.Stop();
        audioSource_canchien_jump.Play();
    }
    public void stopAudioSource_canchien_jump()
    {
        audioSource_canchien_jump.Stop();
    }
    public void startAudioSource_canchien_invi()
    {
        audioSource_canchien_slice.Stop();
        audioSource_canchien_run.Stop();
        audioSource_canchien_jump.Stop();
        audioSource_invi.Play();
    }
    public void stopAudioSource_canchien_invi()
    {
        audioSource_invi.Stop();
    }

    // XE TĂNG
    //-------------------------------------
    public void startAudioSource_tank_Move()
    {
        audioSource_tank_shoot.Stop();
        audioSource_tank_explode.Stop();
        audioSource_tank_move.Play();
    }

    public void stopAudioSource_tank_Move()
    {
        audioSource_tank_move.Stop();
    }

    public void startAudioSource_tank_Shoot()
    {
        audioSource_tank_move.Stop();
        audioSource_tank_explode.Stop();
        audioSource_tank_shoot.Play();
    }

    public void stopAudioSource_tank_Shoot()
    {
        audioSource_tank_shoot.Stop();
    }

    public void startAudioSource_tank_Explode()
    {
        audioSource_tank_move.Stop();
        audioSource_tank_shoot.Stop();
        audioSource_tank_explode.Play();
    }

    public void stopAudioSource_tank_Explode()
    {
        audioSource_tank_explode.Stop();
    }

    //-------------------------------------
    // MÁY BAY TRỰC THĂNG
    //-------------------------------------
    public void startAudioSource_heli_Move()
    {
        audioSource_heli_shoot.Stop();
        audioSource_heli_explode.Stop();
        audioSource_heli_move.Play();
    }

    public void stopAudioSource_heli_Move()
    {
        audioSource_heli_move.Stop();
    }

    public void startAudioSource_heli_Shoot()
    {
        audioSource_heli_move.Stop();
        audioSource_heli_explode.Stop();
        audioSource_heli_shoot.Play();
    }

    public void stopAudioSource_heli_Shoot()
    {
        audioSource_heli_shoot.Stop();
    }

    public void startAudioSource_heli_Explode()
    {
        audioSource_heli_move.Stop();
        audioSource_heli_shoot.Stop();
        audioSource_heli_explode.Play();
    }

    public void stopAudioSource_heli_Explode()
    {
        audioSource_heli_explode.Stop();
    }
    //-------------------------------------
    // LÍNH SÚNG MÁY ĐỨNG YÊN
    //-------------------------------------
    public void StartAudio_gunnerIdle_Shoot()
    {
       
        AudioSource_gunner_idle_shoot.Play();
    }

    public void StopAudio_gunnerIdle_Shoot()
    {
        AudioSource_gunner_idle_shoot.Stop();
    }
    //-------------------------------------
    // LÍNH DI CHUYỂN & BẮN
    //-------------------------------------
    public void StartAudio_gunnerMove_Move()
    {
        AudioSource_gunner_move_shoot.Stop();
       
        AudioSource_gunner_move.Play();
    }

    public void StopAudio_gunnerMove_Move()
    {
        AudioSource_gunner_move.Stop();
    }

    public void StartAudio_gunnerMove_Shoot()
    {
        AudioSource_gunner_move.Stop();
      
        AudioSource_gunner_move_shoot.Play();
    }

    public void StopAudio_gunnerMove_Shoot()
    {
        AudioSource_gunner_move_shoot.Stop();
    }
    //-------------------------------------
    // BOSS 2: chạy, gọi viện binh
    //-------------------------------------
    public void startAudioSource_boss1_Run()
    {
        audioSource_boss2_callReinforce.Stop();
        audioSource_boss2_run.Play();
    }

    public void stopAudioSource_boss1_Run()
    {
        audioSource_boss2_run.Stop();
    }

    public void startAudioSource_boss1_CallReinforce()
    {
        audioSource_boss2_run.Stop();
        audioSource_boss1_callReinforce.Play();
    }

    public void stopAudioSource_boss1_CallReinforce()
    {
        audioSource_boss1_callReinforce.Stop();
    }
    //-------------------------------------
    // BOSS 1: dịch chuyển, gọi viện binh, bắn
    //-------------------------------------
    public void startAudioSource_boss2_Teleport()
    {
        audioSource_boss1_callReinforce.Stop();
        audioSource_boss1_shoot.Stop();
        audioSource_boss1_teleport.Play();
    }

    public void stopAudioSource_boss2_Teleport()
    {
        audioSource_boss1_teleport.Stop();
    }

    public void startAudioSource_boss2_CallReinforce()
    {
        audioSource_boss1_teleport.Stop();
        audioSource_boss1_shoot.Stop();
        audioSource_boss1_callReinforce.Play();
    }

    public void stopAudioSource_boss2_CallReinforce()
    {
        audioSource_boss2_callReinforce.Stop();
    }

    public void startAudioSource_boss2_Shoot()
    {
        audioSource_boss1_teleport.Stop();
        audioSource_boss1_callReinforce.Stop();
        audioSource_boss1_shoot.Play();
    }

    public void stopAudioSource_boss2_Shoot()
    {
        audioSource_boss1_shoot.Stop();
    }

}
