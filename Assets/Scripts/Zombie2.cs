using UnityEngine;

public class Zombie2 : MonoBehaviour
{
    public ZombieHand zombieHand;
    public int zombieDamage;

    private void Start()
    {
        zombieHand.damage=zombieDamage;
    }
}
