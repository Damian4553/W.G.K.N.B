using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private GunData gunData;

    [SerializeField] private Transform muzzle;


    private float _timeSinceLastShot;


    private void Start()
    {
        PlayerShoot.ShootInput += Shoot;
        PlayerShoot.ReloadInput += StartReloading;
    }


    private bool CanShoot() => !gunData.reloading && _timeSinceLastShot > 1f / (gunData.fireRate / 60f);


    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                // if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                // {
                //     Debug.Log(hitInfo.transform.name);
                // }
                Camera playerCamera = Player.GetCamera();
                Transform playerCameraTransform = playerCamera.transform;

                Ray fpsCameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

                Vector3 targetPoint;

                if (Physics.Raycast(fpsCameraRay, out var hit))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = fpsCameraRay.GetPoint(gunData.maxDistance);
                }

                var position = playerCameraTransform.position;
                Vector3 directionWithoutSpread = targetPoint - position;

                float x = Random.Range(-gunData.spread, gunData.spread);
                float y = Random.Range(-gunData.spread, gunData.spread);

            Vector3 directionWithSpread = playerCameraTransform.forward + new Vector3(x, y, 0f);
                
                GameObject currentBullet = Instantiate(gunData.bullet, muzzle.position, Quaternion.identity);

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * gunData.shootForce, ForceMode.Impulse);

                gunData.currentAmmo--;
                _timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    public void StartReloading()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magazineSize;

        gunData.reloading = false;
    }

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);
    }

    private void OnGunShot()
    {
    }
}