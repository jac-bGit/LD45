using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Custom/Werapons", order = 1)]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int dmg;
    public int speed;
    public int lenght;
    public int defence;
    public GameObject weaponPrefab;
}
