using UnityEngine;

// Detects when a given target is visible to this object. A target is
// visible when it's both in range and in front of the target. Both the
// range and the angle of visibility are configurable.
public class SentryGunController : MonoBehaviour
{

    // The object we're looking for.
    Transform lockedTarget = null;

    [SerializeField] GameObject rotatingPart;
    [SerializeField] GameObject muzzle;

    // If the object is more than this distance away, we can't see it.
    [SerializeField] float range = 50f;

    // Rotation speed
    [SerializeField] float rotationSpeed = 100f;

    // Damage
    [SerializeField] float damage = 0f;

    // Rate of fire (in RPM)
    [SerializeField] float fireRate = 0f;

    // Impact force
    [SerializeField] float impactForce = 0f;

    // Total ammunition count
    [SerializeField] int maxAmmo = 250;

    // Current ammo count
    int ammoCount;

    // The angle of our arc of visibility.
    [Range(0f, 360f)]
    public float angle = 360f;

    // A property that other classes can access to determine if we can
    // currently see our target.
    public bool targetIsVisible { get; private set; }

    // Indicates whether the sentry gun is locked on a target or not
    // If so, engage the target until it is neutralized
    bool isLocked = false;

    // Time between each round discharged
    float nextTimeToFire = 0f;

    // Awake is called as the script instance is loaded (before Start).
    void Awake()
    {
        ammoCount = maxAmmo;
    }

    // Check to see if we can see the target every frame.
    void Update()
    {
        // If ammo runs out, sentry gun auto self-destructs
        if (ammoCount <= 0)
        {
            Destroy(gameObject);
        }
        // If there is no locked target, find a target to lock onto
        if (!isLocked) 
        {
            isLocked = SearchForTargets();
        }
        // Else, attempt to track and engage currently locked on target
        else 
        {
            if (lockedTarget != null)
            {
                isLocked = EngageTarget();
            }
            else
            {
                isLocked = false;
                nextTimeToFire = 0f;
            }
        }
    }

    // Attack the target currently locked onto.
    bool EngageTarget()
    {
        // Check if the target is visible
        if (!CheckVisibility(lockedTarget)) {
            return false;
        }

        // Track target
        Vector3 directionToTarget = Vector3.ProjectOnPlane(lockedTarget.position - transform.position, Vector3.up);

        float angleToTarget = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);

        Quaternion rotation = Quaternion.Euler(0f, angleToTarget, 0f);
        rotatingPart.transform.rotation = Quaternion.Slerp(rotatingPart.transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        // Engage target
        if ((ammoCount > 0) && (Time.time >= nextTimeToFire))
        {
            nextTimeToFire = Time.time + 60f / fireRate;
            Shoot();
        }

        return true;
    }

    // // Discharge a round towards a desired direction.
    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        ammoCount--;
    }

    // Returns true if a target is found and locked on, otherwise returns false.
    bool SearchForTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject target in targets)
        {
            if (CheckVisibility(target.transform))
            {
                lockedTarget = target.transform;
                return true;
            }
        }
        return false;
    }

    // Returns true if a straight line can be drawn between this object
    // and the target. The target must be within range, and within the
    // visible arc.
    public bool CheckVisibility(Transform target)
    {
        // Compute the direction to the target
        var directionToTarget = target.position - transform.position;

        // Calculate the number of degrees from the forward direction.
        var degreesToTarget =
            Vector3.Angle(transform.forward, directionToTarget);

        // The target is within the arc if it's within half of the
        // specified angle. If it's not within the arc, it's not visible.
        var withinArc = degreesToTarget < (angle / 2);

        if (withinArc == false)
        {
            return false;
        }

        // Compute the distance to the point
        var distanceToTarget = directionToTarget.magnitude;

        // Our ray should go as far as the target is or the maximum
        // distance, whichever is shorter
        var rayDistance = Mathf.Min(range, distanceToTarget);

        // Create a ray that fires out from our position to the target
        var ray = new Ray(transform.position, directionToTarget);

        // Store information about what was hit in this variable
        RaycastHit hit;

        // Records info about whether the target is in range and not
        // occluded
        var canSee = false;

        // Fire the raycast. Did it hit anything?
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // Did the ray hit our target?
            if (hit.collider.transform == target)
            {
                // Then we can see it (that is, the ray didn't hit an
                // obstacle in between us and the target)
                canSee = true;
            }

            // Visualize the ray.
            Debug.DrawLine(transform.position, hit.point);

        }
        else
        {
            // The ray didn't hit anything. This means that it reached
            // the maximum distance and stopped, which means we didn't
            // hit our target. It must be out of range.

            // Visualize the ray.
            Debug.DrawRay(transform.position,
                          directionToTarget.normalized * rayDistance);
        }

        // Is it visible?
        return canSee;

    }
}