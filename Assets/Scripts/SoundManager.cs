using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public AudioSource ShootingChannel;

    public AudioSource reloadingSoundUzi;
    public AudioSource reloadingSoundAK47;
    public AudioSource emptyMagazineAK47;
    public AudioClip UziShot;
    public AudioClip AK47Shot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.AK47:
                ShootingChannel.PlayOneShot(AK47Shot);
                break;

            case WeaponModel.Uzi:
                ShootingChannel.PlayOneShot(UziShot);
                break;
        }

    }


    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.AK47:
                reloadingSoundAK47.Play();
                break;

            case WeaponModel.Uzi:
                reloadingSoundUzi.Play();

                break;
        }
    }
}
