using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // [Tooltip("less than ")]
    public bool inConversation=false;
    [SerializeField] float moveSpeed=0.2f;
    CharacterController controller;
    LayerMask interactionMask;
    void Start()
    {
        controller=GetComponent<CharacterController>();
        interactionMask=LayerMask.GetMask("Interactable");
    }
    void Update()
    {
        if(!Input.anyKey)
            return;

        if(!inConversation)
            Move();
        
        Interact();

    }
    void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
             if(Physics.CheckSphere(transform.position,2f,interactionMask))
            {
               Collider[] colliders=Physics.OverlapSphere(transform.position,2.0f,interactionMask);
               float mindistance=3.0f;
               Collider interaction=null;
               foreach(Collider collider in colliders)
                {
                    if(collider.GetComponentInParent<Character>())
                    {
                        float distance=Vector3.Distance(collider.transform.position,transform.position);
                        if(distance<mindistance )
                        {
                            mindistance=distance;
                            interaction=collider;
                        }
                    }    
                }
                if(interaction)
                {
                    interaction.GetComponentInParent<Character>().Speak();
                    inConversation=true;
                }

            }
        }
    }
    void Move()
    {
        float x=Input.GetAxis("Horizontal");
        float y=Input.GetAxis("Vertical");
        controller.Move(new Vector3(x,y,0)*Time.deltaTime*moveSpeed);
    }
}
