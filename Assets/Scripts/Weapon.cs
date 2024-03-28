using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    public Weapon[] weapons; 
    public Text ammoText;
    public Text reloadingText;
    public GameObject projectilePrefab;
    protected string weaponName;
    protected int fireRate;
    protected bool IsAutomatic;
    protected int magazineCapacity;
    protected int accuracy;
    protected int damage;
    protected int reloadTime;
    protected float nextFireTime;
    protected float inaccuracy;

    protected int currentAmmo;
    protected bool isReloading;

    // Constructor
    public Weapon(string weaponName, int fireRate, bool IsAutomatic, int magazineCapacity, int accuracy, int damage, int reloadTime, float inaccuracy)
    {
        this.weaponName = weaponName;
        this.fireRate = fireRate;
        this.IsAutomatic = IsAutomatic;
        this.magazineCapacity = magazineCapacity;
        this.accuracy = accuracy;
        this.damage = damage;
        this.reloadTime = reloadTime;
        this.inaccuracy = inaccuracy;
    }

    protected virtual void Start()
    {
        nextFireTime = 0f;
        currentAmmo = magazineCapacity;
        reloadingText.gameObject.SetActive(false);
        isReloading = false;
        ammoText.text = currentAmmo.ToString();
    }

    public void Shoot(){
        if(!isReloading){
            if (currentAmmo > 0){
                if (Time.time >= nextFireTime)
                {
                    float hitChance = Random.Range(0f, 100f);
                    GameObject projectile;
                    if (hitChance <= accuracy){
                        Debug.Log("Gun Fired! Accurate.");
                        projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    }
                    else{
                        Debug.Log("Gun Fired! Inaccurate.");
                        projectile = Instantiate(projectilePrefab, transform.position, randomiseRotation(transform.rotation));
                    }
                    Projectile projectileScript = projectile.GetComponent<Projectile>();
                    if (projectileScript != null)
                    {
                        projectileScript.SetProjectileProperties(damage);
                    }
                    nextFireTime = Time.time + 1f / fireRate;
                    currentAmmo--;
                    ammoText.text = currentAmmo.ToString();
                }
            }
            else
            {
                Debug.Log("Magazine is empty!");
                Reload();
            }
        }
    }

    public void Reload(){
        if(!isReloading){
            isReloading = true;
            Debug.Log("Reloading...");
            reloadingText.gameObject.SetActive(true);
            StartCoroutine(ReloadCoroutine());
            
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineCapacity;
        ammoText.text = currentAmmo.ToString();
        reloadingText.gameObject.SetActive(false);
        isReloading = false;
    }

    private Quaternion randomiseRotation(Quaternion transformRotation){
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-inaccuracy, inaccuracy), 0);
        return transformRotation*randomRotation;
    }

    public bool isAutomatic(){return IsAutomatic;}
    public int getCurrentAmmo(){return currentAmmo;}
    public string getName(){return weaponName;}
    
}
