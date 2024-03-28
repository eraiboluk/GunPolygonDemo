using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    public Weapon[] weapons; 
    public Text ammoText;
    public Text gunType;
    public int currentWeaponIndex = 0;
    private int SwitchIndex = 1;

    private void Start()
    {
        SwitchWeapon();
    }

    void Update()
    {
        if(weapons[currentWeaponIndex].isAutomatic()){
            if (Input.GetKey(KeyCode.S))
            {
                weapons[currentWeaponIndex].Shoot();
            }
        }
        else{
            if (Input.GetKeyDown(KeyCode.S))
            {
                weapons[currentWeaponIndex].Shoot();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapons[currentWeaponIndex].Reload();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        currentWeaponIndex += SwitchIndex;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        ammoText.text = weapons[currentWeaponIndex].getCurrentAmmo().ToString();
        gunType.text=weapons[currentWeaponIndex].getName();
        Debug.Log("Switched to "+weapons[currentWeaponIndex].getName());
        SwitchIndex *= -1;
    }
}
