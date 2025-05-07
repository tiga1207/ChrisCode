using System.Collections;
using System.Collections.Generic;
using Scripts.Manager;
using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip slashSound;
    [SerializeField] private AudioClip deadSound;
    public void ShootingSound()
    {
        AudioManager.Instance.PlaySFX(shotSound);
    }
    public void HurtSound()
    {
        AudioManager.Instance.PlaySFX(hurtSound);
    }
    public void SlashSound()
    {
        AudioManager.Instance.PlaySFX(slashSound);
    }
    public void DeadSound()
    {
        AudioManager.Instance.PlaySFX(deadSound);
    }
}
