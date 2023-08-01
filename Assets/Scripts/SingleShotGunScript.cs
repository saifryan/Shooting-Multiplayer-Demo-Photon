using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGunScript : GunScript
{
    [SerializeField] Camera cam;
    public override void Use()
    {
        Debug.Log("Gun Name : " + itemInfo.ItemName);
        Shoot();
    }

    void Shoot()
    {
        Debug.Log("Shooting");

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<Idamageable>()?.TakeDamage(((GunInfoScript)itemInfo).Damage);
        }
    }
}
