using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action ShootInput;
    public static Action ReloadInput;

    [SerializeField] private KeyCode reloadKey;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            ReloadInput?.Invoke();
        }
    }
}
