using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject explosion_effect;

    void OnCollisionEnter(){
    var expl = Instantiate(explosion_effect, transform.position, Quaternion.identity);
     Destroy(gameObject); // destroy the projectile
     Destroy(expl, 3); // delete the explosion after 3 seconds
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
