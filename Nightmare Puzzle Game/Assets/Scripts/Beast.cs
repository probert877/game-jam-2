using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Beast : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;


    // array of monster objects in scene

    bool _hasDied;
    private Animator myAnimator;

    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();

    }


    void Awake (){
        
    }

    void Update()
    {

    }

    // I want the Monster to die

    // Animation Event - once the Beast Boy Death animation is done, then the Particle Effect can go off!
    // We can do a lot with this :D
    public void triggerKillObject(string s){
        Debug.Log(s + "kill event received");
        _particleSystem.Play();
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision){
        // check if the collision is by nibbler

        // we are looking at the collision, and it gives us a gameObject
        // we will get the nibbler component. If there IS a nibbler component, then that will be returned. If not, this will return null

        if (ShouldDieFromCollision(collision))
        {
            // StartCoroutine(Die());
            myAnimator.SetTrigger("isDead");
            _particleSystem.Play();

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
        // Set up Animation Controller

        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        // forces particle system to play when we die
        _particleSystem.Play();
        // wait a second, then set the Monster to disappear
        yield return new WaitForSeconds(2);

        
        
        
        // This way makes the Monster just disappear
        gameObject.SetActive(false);
    }
}
