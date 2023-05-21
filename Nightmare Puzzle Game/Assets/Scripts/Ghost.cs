using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Ghost : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied;

    // I want the Monster to die

    void OnCollisionEnter2D(Collision2D collision){
    // check if the collision is by nibbler

    // we are looking at the collision, and it gives us a gameObject
    // we will get the nibbler component. If there IS a nibbler component, then that will be returned. If not, this will return null

    if (ShouldDieFromCollision(collision))
    {
        StartCoroutine(Die());
    }   
    // takes current gameObject and sets them not Active (disappears from Gamer view)
        

   }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        // if we have died, we should not die from a collision again (multiple deaths)
        if (_hasDied)
            return false;

        Nibbler nibbler = collision.gameObject.GetComponent<Nibbler>();
        // if nibbler collided with the Monster, then return true - should die
        // if the collision is not by nibbler, then the Monster doesn't die - return false
        if (nibbler != null)
            return true;

        // array of gameObjects that contact from collision. Usually just need the first one
        // if the contact is from above (-.5 on the y axis), then we want to do something with it

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }

    IEnumerator Die()
    {   
        _hasDied = true;
        // show the dead sprite variable when the monster Dies
        // forces particle system to play when we die
        _particleSystem.Play();
        // wait a second, then set the Monster to disappear
        yield return new WaitForSeconds(3);

              
        
        // This way makes the Monster just disappear
        gameObject.SetActive(false);
    }
}
