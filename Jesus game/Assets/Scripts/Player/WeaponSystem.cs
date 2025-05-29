using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Bow;
    public Transform WeaponHolder; // The transform that holds the weapon (positioned at character's hand/body)

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        Sword.SetActive(true);
        Bow.SetActive(false);
    }

    void Update()
    {
        HandleWeaponSwitch();
        RotateWeaponToMouse();
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sword.SetActive(true);
            Bow.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Sword.SetActive(false);
            Bow.SetActive(true);
        }
    }

    void RotateWeaponToMouse()
    {
        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 direction = mouseWorldPosition - WeaponHolder.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        WeaponHolder.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
