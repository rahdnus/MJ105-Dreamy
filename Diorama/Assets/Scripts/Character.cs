using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    [SerializeField]GameObject prompt;
    [SerializeField]StoryManager storyManager;
    public void Start()
    {
    }
    public void Speak()
    {
        storyManager.Begin(name);
        prompt.SetActive(false);
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
