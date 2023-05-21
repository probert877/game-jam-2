using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nibbler : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 2;

    Vector2 _startPosition;
    // cache our component references so it is more performant, and makes for higher code legibility
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;

    public LevelController levelController;
    private LivesManager livesManager;

    void Awake(){
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // set the startPosition variable... 2 ways w/ transform and GetComponent<RigidBody2D>
        _startPosition = _rigidbody2D.position;
        // Stopping our character from falling. Sit still, then drag it around, then launch it.
        // Code will access the 2D Rigid Body physics controller
        // isKinematic - still simulated, but it is controlled by the player
        _rigidbody2D.isKinematic = true;
        livesManager = FindObjectOfType<LivesManager>();
        


    
    }

    // click and hold nibbler, change its colors

    void OnMouseDown(){
        // GetComponent<SpriteRenderer>().color = new Color(1,0,1);
       _spriteRenderer.color = Color.red;
    }

    void OnMouseUp() {
        

        // set the current position
        Vector2 currentPosition = _rigidbody2D.position;
        // subtract start from current
        Vector2 direction = _startPosition - currentPosition;
        // get total magnitude of the vector
        direction.Normalize();
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);
        
        _spriteRenderer.color = Color.white;

        // once nibbler is launched, decreased the amount of lives left
        Debug.Log("beginning counter" + livesManager.livesCounter);
        livesManager.livesCounter--;
        Debug.Log("ending counter" + livesManager.livesCounter);

    }

    void OnMouseDrag() {
        // move our bird to wherever our player's mouse is
        // set mousePosition variable to the mouse position input
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // gives 2 dimensional vector and clamps the position, no Z position
       
        Vector2 desiredPosition = mousePosition;
        
        // gives us distance back in meters from Start Position to Desired Position
        float distance = Vector2.Distance(desiredPosition, _startPosition);

        if (distance > _maxDragDistance){
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            //change desired position to a point in that direction at the maximum distance
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }

        // check if the desired position x value is greater than the start position x value... 
        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;


        _rigidbody2D.position = desiredPosition;
        

    }

    // Update is called once per frame
    // Any code we want to run every frame... like 100 frames a second (will be called 100 times a second).
    // Handling input, watching to see if we shot at something, or collisions, etc.
    void Update()
    {
        
    }
    
    
    // we want to reset nibbler on collision
    
    void OnCollisionEnter2D(Collision2D collision){
        StartCoroutine(ResetAfterDelay());
        


    }

    IEnumerator ResetAfterDelay() {
               
        yield return new WaitForSeconds(3);
        // take nibbler, reset their position to the _startPosition and become kinematic
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        // stops his movement completely
        _rigidbody2D.velocity = Vector2.zero;
        
    }
}
