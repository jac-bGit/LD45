using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : CharacterScr
{
    //equipment
    public Weapon weapon;
    public int[] hitZonesArmor = new int[3];

    //references
    protected Hand hand;

    //combat
    public int attackStrenght;
    protected bool inCombat;
    public Fighter opponent;
    public float attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CharacterSetup()
    {
        base.CharacterSetup();
        //get references
        hand = GetComponentInChildren<Hand>();

        //set hit zones
        for(int i = 0; i < hitZonesArmor.Length; i++)
        {
            if (hitZonesArmor[i] < 0)
                hitZonesArmor[i] = 0;
        }
    }

    public void TakeDamage(int dmg, int hitZoneId)
    {
        //check zone 
        Debug.Log("hit zone number: " + hitZoneId);
        //calculate dmg
        int finalDmg = dmg - hitZonesArmor[hitZoneId];
        //substract from stamina
        if (((float)stamina - (float)finalDmg) / (float)maxStamina > 0.5f)
        {
            stamina -= dmg;
        }
        //if dmg to stamina will drop under 50% - suubract from hp
        else
        {
            int dmgToHp = dmg;
            //if is not allready under 50%
            if ((float)stamina / (float)maxStamina > 0.5f)
            {
                dmgToHp = (int)((float)dmg - ((float)stamina - (float)maxStamina * 0.5f));
                Debug.Log("dmgToHp between: " + dmgToHp);
            }

            if (((float)stamina - (float)finalDmg) / ((float)maxStamina * 0.5f) > 0)
                dmgToHp = (int)((float)dmgToHp - ((float)dmgToHp * (((float)stamina - (float)finalDmg) / ((float)maxStamina * 0.5f))));


            Debug.Log("stam: " + (((float)stamina - (float)finalDmg) / ((float)maxStamina * 0.5f)));
            Debug.Log("dmgToHp: " + dmgToHp);
            hp -= dmgToHp;
            stamina -= dmg;
        }
    }
}
