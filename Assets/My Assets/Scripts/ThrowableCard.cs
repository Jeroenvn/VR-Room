using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCard : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform instantiatePosition;


    public void Throw(){

        GameObject instance = Instantiate(cardPrefab, instantiatePosition.position, transform.rotation);

        if (instance.TryGetComponent(out Rigidbody rigidbody)){
            
            if (rigidbody == null){

                throw new System.Exception("No rigidbody found");

            }

            float speed = 3f;
            rigidbody.velocity = transform.forward * speed;
        }

    }

}
