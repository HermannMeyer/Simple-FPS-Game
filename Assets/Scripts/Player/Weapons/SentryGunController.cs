using UnityEngine;

// Detects when a given target is visible to this object. A target is
// visible when it's both in range and in front of the target. Both the
// range and the angle of visibility are configurable.
public class SentryGunController : MonoBehaviour
{

    // The object we're looking for.
    Transform lockedTarget = null;

    public GameObject rotatingPart;
    public GameObject muzzle;

    // If the object is more than this distance away, we can't see it.
    public float range = 50f;

    // Rotation speed
    public float rotationSpeed = 100f;

    // Damage
    public float damage = 0f;

    // Rate of fire (in RPM)
    public float fireRate = 0f;

    // Impact force
    public float impactForce = 0f;

    // Total ammunition count
    public int maxAmmo = 250;

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

    //
    float nextTimeToFire = 0f;

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
                print(lockedTarget.name);
                return true;
            }
        }
        return false;
    }

    // Returns true if this object can see the specified position.
    public bool CheckVisibilityToPoint(Vector3 worldPoint)
    {

        // Calculate the direction from our location to the point
        var directionToTarget = worldPoint - transform.position;

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

        // Figure out the distance to the target
        var distanceToTarget = directionToTarget.magnitude;

        // Take into account our maximum distance
        var rayDistance = Mathf.Min(range, distanceToTarget);

        // Create a new ray that goes from our current location, in the
        // specified direction
        var ray = new Ray(muzzle.transform.position, directionToTarget);

        // Stores information about anything we hit
        RaycastHit hit;

        // Perform the raycast. Did it hit anything?
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // We hit something.
            if (hit.collider.transform == lockedTarget)
            {
                // It was the target itself. We can see the target point.
                return true;
            }
            // It's something between us and the target. We cannot see
            // the target point.
            return false;
        }
        else
        {
            // There's an unobstructed line of sight between us and the
            // target point, so we can see it.
            return true;
        }
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