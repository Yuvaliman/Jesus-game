using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Bow;
    private bool isSwordActive = true;

    // Start is called before the first frame update
    void Start()
    {
        Sword.SetActive(true);
        Bow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSwordActive = !isSwordActive;
            Sword.SetActive(isSwordActive);
            Bow.SetActive(!isSwordActive);
        }
    }

    public bool IsSwordActive()
    {
        return isSwordActive;
    }
}
