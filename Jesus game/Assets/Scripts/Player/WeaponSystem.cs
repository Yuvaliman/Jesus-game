using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Bow;

    // Start is called before the first frame update
    void Start()
    {
        Sword.SetActive(true);
        Bow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
}
