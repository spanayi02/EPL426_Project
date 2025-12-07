using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int HP = 200;
    public void TakeDamage(int damageAmount)
    {
        

        if (HP <= 0)
        {
            print("Player Dead");
        }
        else
        {
            print("player hit");
        }
        }
    
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {

            TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
        }
    }


}
