using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    delegate void FallControlOfAllObjectsInTheScene(int levelOfNumber);
    FallControlOfAllObjectsInTheScene fallControlOfAllObjectsInTheScene;

    bool falling = false;
    void Start()
    {
        fallControlOfAllObjectsInTheScene += GameManager.Instance.theFallControlOfAllObjectsInTheScene;
    }

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, 2);
        Destroy(this, 2);
    }
    void FixedUpdate()
    {
        
        if (transform.position.y <= StaticDefinitions.FALLING_THRESHOLD && falling == false)
        {
            Handheld.Vibrate();
            
            if(gameObject.name.StartsWith(StaticDefinitions.ENEMY_NAME))
                GameManager.Instance.detectForEnemy = gameObject.transform.name;
            else
                fallControlOfAllObjectsInTheScene(GameManager.Instance.numberOfLevel);   

            falling = true;
            DestroyObjectDelayed();
            
        }
    }

    
}
