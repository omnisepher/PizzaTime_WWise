using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBase : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    public bool fed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("Done", false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.givingPizza && !fed)
            {
                fed = true;
                GameManager.fedPeople++;
                anim.SetBool("Done", true);
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
