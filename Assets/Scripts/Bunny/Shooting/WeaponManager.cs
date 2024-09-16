using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public WeaponStatistics weaponStatistics;


    private BunnyInput bunnyInput;
    private InputAction shoot;


    [SerializeField] private GameObject weapon;
    [SerializeField] private float bulletSpeed;


    private int startAmunition;
    private bool canShoot = true;


    private void Awake()
    {
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

    private void Start()
    {
        startAmunition = weaponStatistics.amunition;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed && weaponStatistics.amunition >0 && canShoot == true)
        {
            Shoot();
        }
    }


    public void OnReload()
    {
        weaponStatistics.amunition = startAmunition;
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(weaponStatistics.bulletProjectile, weapon.transform.position, weapon.transform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = transform.right * bulletSpeed;


        weaponStatistics.amunition --;


        if(weaponStatistics.amunition == 0)
        {
            StartCoroutine(ReloadProces());
        }


        canShoot = false;


        StartCoroutine(FireCooldDown());
    }


    private IEnumerator ReloadProces()
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
