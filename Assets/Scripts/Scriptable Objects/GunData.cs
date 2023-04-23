using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
    public class GunData : ScriptableObject
    {
        [Header("Info")]
        public new string name;

        [Header("Shooting")]
        public float damage;
        public float maxDistance;
        public float shootForce;
        public float spread;
        public GameObject bullet;

        [Header("Reloading")]
        public int currentAmmo;
        public int magazineSize;
        public float fireRate;
        public float reloadTime;
        [HideInInspector]
        public bool reloading;

    }
}
