using UnityEngine;

public class PlayerRoot : MonoBehaviour
{
    [SerializeField] private Transform weaponSetRoot;

    public Transform WeaponSetRoot => weaponSetRoot;
}
