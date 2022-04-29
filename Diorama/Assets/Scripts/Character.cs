using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]GameObject prompt;


    public void Speak()
    {
        Debug.Log("logging");
    }
   void OnTriggerEnter2D(Collider2D collider)
   {
    //    if(collider.GetComponentInParent<Player>())
       {
           prompt.SetActive(true);
       }
   }
   void OnTriggerExit2D(Collider2D collider)
   {
    //    if(collider.GetComponentInParent<Player>())
       {
           prompt.SetActive(false);
       }
   }
}
