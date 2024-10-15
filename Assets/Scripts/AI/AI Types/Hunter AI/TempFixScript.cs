using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempFixScript : MonoBehaviour 
{ 
    public WeaponStatistics weaponStatistics;
public Transform weaponTransform;

private BunnyInput bunnyInput;
private InputAction shoot;


private int startAmunition;
private int weaponAmmunition;

public bool canShoot = true;


/// This function is called when the script instance is being loaded
private void Awake()
{
    // Create a new instance of the BunnyInput class.
    bunnyInput = new BunnyInput();
}


private void OnEnable()
{
    shoot = bunnyInput.Bunny.Shoot;


    shoot.Enable();
}


private void OnDisable()
{
    shoot.Disable();
}


/// Start is called 
private void Start()
{
    startAmunition = weaponStatistics.amunition;

    weaponAmmunition = weaponStatistics.amunition;
}


/// Shoot is called whenever the input action is called.
public void OnShoot(InputAction.CallbackContext context)
{

    if (context.performed && weaponStatistics.amunition > 0 && canShoot == true)
    {
        Shoot();
    }
}


public void OnReload()
{
    weaponAmmunition = startAmunition;
}


public void Shoot()
{
    GameObject bullet = Instantiate(weaponStatistics.bulletProjectile, weaponTransform.position, weaponTransform.rotation);

    bullet.GetComponent<Rigidbody>().velocity = transform.right * weaponStatistics.bulletSpeed;


    weaponAmmunition--;


    if (weaponAmmunition < 1)
    {
        StartCoroutine(ReloadProces());
    }


    canShoot = false;


    StartCoroutine(FireCooldDown());
}


public IEnumerator ReloadProces()
{
    yield return new WaitForSeconds(weaponStatistics.realodTime);


    OnReload();
}

private IEnumerator FireCooldDown()
{
    yield return new WaitForSeconds(1 / weaponStatistics.fireRate);

    canShoot = true;
}
}
