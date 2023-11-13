using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    Vector3 direction = new Vector3(0.0f, -1.0f, 1.0f);
    void Update()
    {
        transform.Translate(direction * 5.0f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
