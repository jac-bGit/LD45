using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScr : MonoBehaviour
{
    //basics stats 
    [SerializeField] protected int maxHp, hp, maxStamina, stamina;
    //movement
    [SerializeField] protected int speed, combatMovementSpeed;

    //components
    protected Rigidbody2D rb;

    //basic setup common for every character
    public virtual void CharacterSetup()
    {
        //get components
        rb = GetComponent<Rigidbody2D>();
    }

    #region GET_SET
    //getters
    public int getMaxHp() { return maxHp; }
    public int getHp() { return hp; }
    public int getMaxStamina() { return maxStamina; }
    public int getStamina() { return stamina; }

    #endregion

    public virtual void TakeDamage(int dmg)
    {
        
    }
}
