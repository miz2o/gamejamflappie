using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


/// this script 
public class WeaponManager : MonoBehaviour
{
    public WeaponStatistics weaponStatistics;
    public Transform weaponTransform;

    private BunnyInput bunnyInput;
    private InputAction shoot;


    private int startAmunition;
    private int weaponAmmunition;

    public bool canShoot = true;
//    public Animator animator;


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
         
        if(context.performed && weaponStatistics.amunition >0 && canShoot == true)
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

        //animator.SetTrigger("shoot");
        weaponAmmunition--;


        if(weaponAmmunition < 1)
        {
            StartCoroutine(ReloadProces());
        }


        canShoot = false;


        StartCoroutine(FireCooldDown());
    }


    public IEnumerator ReloadProces()
    {
        //animator.SetTrigger("reload");
        yield return new WaitForSeconds(weaponStatistics.realodTime);


        OnReload();
    }

    private IEnumerator FireCooldDown()
    {
        yield return new WaitForSeconds(1 / weaponStatistics.fireRate);

        canShoot = true;
    }
}
