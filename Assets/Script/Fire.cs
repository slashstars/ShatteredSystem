using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    public GameObject projectile;
    public float FireForce = 10;
    private string spaceGunName;

    // Use this for initialization
    void Start()
    {
        spaceGunName = transform.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (IsClickPositionAValidTarget(Input.mousePosition))
                CreateProjectileAndFire(Input.mousePosition, FireForce);
        }
    }

    private bool IsClickPositionAValidTarget(Vector3 clickPosition)
    {
        var ray = Camera.main.ScreenPointToRay(clickPosition);
        RaycastHit hit;
        var isClickOnObject = Physics.Raycast(ray, out hit, 100);

        if (!isClickOnObject || (isClickOnObject && hit.transform.name != spaceGunName))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Creates the projectile and activates its fire functionality.
    /// </summary>
    /// <param name="target">The target for projectile as non-world point.</param>
    /// <param name="force">Force of projectile launch.</param>
    private void CreateProjectileAndFire(Vector3 target, float force)
    {
        Vector3 curScreenPoint = Camera.main.ScreenToWorldPoint(target);
        var directionOfFire = (curScreenPoint - transform.position).normalized;

        var currentProjectile = Instantiate(projectile);
        currentProjectile.transform.position = transform.transform.position;
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * FireForce, ForceMode2D.Impulse);
    }
}
