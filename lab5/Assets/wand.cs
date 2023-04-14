using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPC_script_namespace;

public class wand : MonoBehaviour
{

    private FPC_script _input;
    [SerializeField]
    private GameObject projectile_prefab;
    [SerializeField]
    private GameObject projectile_point;
    [SerializeField]
    private float projectile_speed = 600;

    [SerializeField]
    public GameObject active_effect;
    [SerializeField]
    public GameObject passive_effect;

    // Start is called before the first frame update
    void Start()
    {
        active_effect.SetActive(false);
        _input = transform.root.GetComponent<FPC_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.is_shooting){
            Shoot();
            _input.is_shooting = false;
        }
    }

    void Shoot(){
        passive_effect.SetActive(false);
        active_effect.SetActive(true);
        Debug.Log("SHOOTING");
        GameObject projectile = Instantiate(projectile_prefab, projectile_point.transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile_point.transform.forward * projectile_speed);
        active_effect.SetActive(false);
        passive_effect.SetActive(true);
        // Destroy(projectile, 1);
    }
}
