using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticDefinitions;


/*
 * This class has singleton pattern.
 */ 
public class GameManager : MonoBehaviour
{
    public GameObject sphere;
    public GameObject parentSphere;

    public bool openGateFlag;
    public int numberOfFallingObject;
    public string detectForEnemy;
    public bool isCameraMoveToSecondGround = false;
    public Vector3 currentPosOfCamera;
    public GameObject forGate;
    GameObject tmpObjectForSphere;

    // Shaking configurations of camera
    public Transform cameraTransform;
    float shakeDuration = SHAKE_DURATION;
    float shakeAmount = SHAKE_AMOUNT;
    float decreaseFactor = SHAKE_DECREASE_FACTOR;

    public int score = 0;
    public int numberOfLevel = 0;
    
    Dictionary<StaticDefinitions.Levels, KeyValuePair<int,int> > mapOfLevelName;


    float timerForOpenTheGate = TIMER_FOR_OPEN_THE_GATE;

    public static GameManager Instance { get; private set; }


    void Start()
    {
        currentPosOfCamera = cameraTransform.localPosition;
    }


    private void Awake()
    {
        mapOfLevelName = new Dictionary<StaticDefinitions.Levels, KeyValuePair<int, int>>();
        mapOfLevelName.Add(StaticDefinitions.Levels.FIRST_LEVEL, new KeyValuePair<int, int>(
            StaticDefinitions.FL_NUMBER_OF_OBJECT_IN_THE_FIRST_SCENE,
            StaticDefinitions.FL_NUMBER_OF_OBJECT_IN_THE_SCENE));


        mapOfLevelName.Add(StaticDefinitions.Levels.SECOND_LEVEL, new KeyValuePair<int, int>(
           StaticDefinitions.SL_NUMBER_OF_OBJECT_IN_THE_FIRST_SCENE,
           StaticDefinitions.SL_NUMBER_OF_OBJECT_IN_THE_SCENE));

        mapOfLevelName.Add(StaticDefinitions.Levels.THIRD_LEVEL, new KeyValuePair<int, int>(
           StaticDefinitions.TL_NUMBER_OF_OBJECT_IN_THE_FIRST_SCENE, 
           StaticDefinitions.TL_NUMBER_OF_OBJECT_IN_THE_SCENE));


        /* This function(createSphereForTinyGround) use for auto generate sphere for the TinyGround.
         */
        createSphereForTinyGround();


        findNumberOfLevel();
        calculateOfGameScore();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void FixedUpdate()
    {

        
        if (openGateFlag == true)
        {

            timerForOpenTheGate -= Time.deltaTime;
            if (timerForOpenTheGate <= Time.deltaTime)
            {
                openGate();
                timerForOpenTheGate = 0.1f;
            }

        }

        

        /* if The EnemyObject has fallen, this level will fail.
         */
        if(detectForEnemy.StartsWith(ENEMY_NAME)) 
        {
            if(shakeDuration > 0)
            {
                cameraTransform.localPosition = currentPosOfCamera + Random.insideUnitSphere * shakeAmount;
                Handheld.Vibrate();
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                cameraTransform.localPosition = currentPosOfCamera;
                PlayerPrefs.SetString(LEVEL_NAME_TO_RETURN, SceneManager.GetActiveScene().name);

                /* if this condition is true, is not passed to the next level. 
                 * So Game can just restart with current level. 
                 */
                SceneManager.LoadScene(StaticDefinitions.GAME_OVER_SCENE);
                

            }
            
        }
        
        
        /* I used the one Collider for all of scene, so if sphere has rigidbody component at the begining, 
         * they are affected by the movement of mesh collider. therefore i need this part for this reason.
         */
        if (openGateFlag) 
        {
            for (int childIndexOfParentSphere = 0; childIndexOfParentSphere < parentSphere.transform.childCount; childIndexOfParentSphere++)
            {
                if(parentSphere.transform.GetChild(childIndexOfParentSphere).gameObject.GetComponent<Rigidbody>() == null)
                    parentSphere.transform.GetChild(childIndexOfParentSphere).gameObject.AddComponent<Rigidbody>();
                    
            }
        }

    }

    public void getInnerObject(GameObject go)
    {

        if (go.transform.childCount == 0)
        {
            if (go.GetComponent<Rigidbody>() == null)
                go.AddComponent<Rigidbody>();
        }
        else
        {
            for (int childIndex = 0; childIndex < go.transform.childCount; childIndex++)
            {
                if (go.transform.GetChild(childIndex).gameObject.GetComponent<Rigidbody>() == null)
                {
                    go.transform.GetChild(childIndex).gameObject.AddComponent<Rigidbody>();
                }

            }
        }


    }


   /* This Function(fallControlOfAllObjectsInTheScene) has two purposes.
    * Firstly, checks whether the objects have fallen in the first scene (This makes for opening the gate).
    * Secondly, checks whether the all valid objects have fallen in the scene (if the valid objects have fallen, is passed the next level). 
    */
    public void theFallControlOfAllObjectsInTheScene(int levelNumber)
    {
        /* if this condition is true, is opened the gate in the firstGround. */
        if (numberOfFallingObject == mapOfLevelName[(StaticDefinitions.Levels)levelNumber].Key)
            openGateFlag = true;

        /* if this condition is true, is passed the next level. */
        if (numberOfFallingObject == mapOfLevelName[(StaticDefinitions.Levels)levelNumber].Value)
            nextLevel();

        numberOfFallingObject++;
    }

    private void findNumberOfLevel()
    {
        string digitCharacter = "";

        string currentLevelName = SceneManager.GetActiveScene().name;

        /* The purpose here is to find the level number in the string. */
        foreach (char c in currentLevelName)
        {
            if (char.IsDigit(c))
            {
                digitCharacter = c.ToString();
                currentLevelName = currentLevelName.Replace(c.ToString(), string.Empty);
            }

        }

        int.TryParse(digitCharacter, out numberOfLevel);
        PlayerPrefs.SetInt(StaticDefinitions.NUMBER_OF_CURRENT_LEVEL, numberOfLevel);
    }

    private void calculateOfGameScore()
    {
        /* I increase the score +100 point every level. */
        score = PlayerPrefs.GetInt(StaticDefinitions.NUMBER_OF_CURRENT_LEVEL) * StaticDefinitions.SCORE_COEFFICIENT;
        
        PlayerPrefs.SetInt(StaticDefinitions.SCORE, score);
    }

    private void nextLevel()
    {
        

        string nextLevelName = "Level_";

        /* My Game architect has three level now. If add new scene in the game, 
         * will be enough to change this macro that it is defined in the StaticDefinitions. 
         */
        if (numberOfLevel != TOTAL_LEVEL_OF_NUMBERS)
            numberOfLevel++;

        nextLevelName = nextLevelName + numberOfLevel.ToString();

        PlayerPrefs.SetString(StaticDefinitions.NEXT_LEVEL_NAME, nextLevelName);

        SceneManager.LoadScene(StaticDefinitions.NEXT_LEVEL_SCENE);
        
    }

    private void openGate()
    {
        if (forGate.transform.localScale.y >= 0)
        {
            forGate.transform.localScale += new Vector3(0, FL_THE_REDUCING_SCALE_FOR_OPEN_THE_GATE);
            forGate.transform.position   -= new Vector3(0, FL_THE_REDUCING_YPOSITION_FOR_OPEN_THE_GATE);
        }

    }

    private void createSphereForTinyGround()
    {
        for(float i = 2.75f; i <= 7; i = i + 0.2f )
        {
            for(float j = -0.7f; j <= 0.5f; j = j + 0.2f )
            {
                tmpObjectForSphere = Instantiate(sphere, new Vector3(i, 0.07f, j), Quaternion.identity);
                tmpObjectForSphere.transform.parent = parentSphere.transform;
                
            }
            
        }
    }
   

}
