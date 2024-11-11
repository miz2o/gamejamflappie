using System.Collections;
using UnityEngine;

public class HunterUnit : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3[] path;
    private int targetIndex;

    [Header("Script references")]
    [SerializeField] private HunterWeaponManager hunterWeaponManager;
    private LineOfSight lineOfSight;

    [Header("Scriptable Objects")]
    [SerializeField] private AIStatistics aiStatistics;
    [SerializeField] private WeaponStatistics weaponStatistics;

    private float thisSpeed;
    private int thisAmmunition;

    [Header("Animation")]
    [SerializeField] private Animator hunterAnimator;

    [Header("Audio")]
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource ShootAudio;

    private void Awake()
    {
        target = GameObject.Find("PlayerHolder")?.transform;
        lineOfSight = GetComponent<LineOfSight>();

        thisAmmunition = weaponStatistics.amunition;
        thisSpeed = aiStatistics.movementSpeed;
        hunterAnimator.SetTrigger("Idle");
    }

    private void Start()
    {
        UpdatePath();
    }

    void Update()
    {
        if (target == null) return;

        if (lineOfSight.playerIsViseble)
        {
            LookAtPlayer();
            if (Vector3.Distance(transform.position, target.position) > 0f)
            {
                UpdatePath();
                hunterAnimator.SetTrigger("Walk");
            }
        }
        else
        {
            StopCoroutine("FollowPath");
            hunterAnimator.SetTrigger("Idle");
        }

        if (lineOfSight.playerIsViseble && hunterWeaponManager.canShoot && thisAmmunition > 0)
        {
            ShootAtPlayer();
        }
        else if (lineOfSight.playerIsViseble && hunterWeaponManager.canShoot && thisAmmunition <= 0)
        {
            StartCoroutine(hunterWeaponManager.ReloadProces());
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && lineOfSight.playerIsViseble)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            if (!walkAudioSource.isPlaying) walkAudioSource.Play();
        }
    }

    IEnumerator FollowPath()
    {
        if (path == null) yield break;
        if (targetIndex < path.Length)
        {
            Vector3 currentWaypoint = path[targetIndex];

            while (true)
            {
                if (this == null || target == null) yield break;

                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length) yield break;
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, thisSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }



    private void LookAtPlayer()
    {
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, aiStatistics.rotationSpeed * Time.deltaTime);
        }
    }

    private void ShootAtPlayer()
    {
        try
        {
            hunterWeaponManager.Shoot();
            hunterAnimator.SetTrigger("Shoot");

            if (thisAmmunition < 1)
            {
                hunterWeaponManager.canShoot = false;
                StartCoroutine(hunterWeaponManager.ReloadProces());
            }

            if (!ShootAudio.isPlaying) ShootAudio.Play();
        }
        catch (MissingReferenceException e)
        {
            Debug.LogWarning("HunterUnit references were destroyed: " + e.Message);
        }
    }

    public void UpdatePath()
    {
        if (target != null)
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
