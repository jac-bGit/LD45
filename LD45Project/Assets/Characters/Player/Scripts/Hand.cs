using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //position
    [SerializeField] private float positionLenght;
    private int handHeldPosition;
    public float attackRadius;
    private bool isAttacking;

    //components
    private Animator anim;

    //references
    public Transform handTransform;
    private Fighter fighter;
    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        //get references
        anim = GetComponent<Animator>();

        //get references
        fighter = GetComponentInParent<Fighter>();
        weapon = fighter.weapon;

        //spawn weapon to hand
        if(weapon != null)
        {
            GameObject newWeapon = Instantiate(weapon.weaponPrefab, handTransform.position, handTransform.rotation, handTransform);
        }

        //setup
        attackRadius = weapon.lenght;
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHandPosition();
        anim.SetBool("isAttacking", isAttacking);
        if (Input.GetMouseButtonUp(0) && !isAttacking)
            Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(handTransform.position, attackRadius);
    }

    //positioning hand
    void SetHandPosition()
    {
        //going down with hand
        if (!isAttacking)
        {
            if (handHeldPosition > 0)
            {
                if (Input.GetKeyDown(KeyCode.S))
                    handHeldPosition--;
            }
            //going up with hand
            if (handHeldPosition < 2)
            {
                if (Input.GetKeyDown(KeyCode.W))
                    handHeldPosition++;
            }
        }

        //set hand position
        switch (handHeldPosition)
        {
            case 0:
                transform.localPosition = new Vector2(transform.localPosition.x, -positionLenght);
                break;
            case 1:
                transform.localPosition = new Vector2(transform.localPosition.x, 0);
                break;
            case 2:
                transform.localPosition = new Vector2(transform.localPosition.x, positionLenght);
                break;
        }
    }

    //attack
    void Attack()
    {
        isAttacking = true;
    }

    //deal damage to the target fighter
    public void HitTarget()
    {
        //check collider 
        Collider2D[] colls =  Physics2D.OverlapCircleAll(handTransform.position, attackRadius);
        bool targetHitted = false;
        foreach(Collider2D col in colls)
        {
            if (col.gameObject == fighter.opponent.gameObject)
                targetHitted = true;
        }

        //check hit with target
        if(targetHitted)
        {
            int finalDmg = fighter.attackStrenght + weapon.dmg;
            fighter.opponent.TakeDamage(finalDmg, handHeldPosition);
        }
        else
        {
            Debug.Log("not hit");
        }

        //deal damage to target

        //end animation
        isAttacking = false;
    }

}
