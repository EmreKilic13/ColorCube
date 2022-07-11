using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticDefinitions 
{

    public enum Levels
    {
        FIRST_LEVEL = 1,
        SECOND_LEVEL = 2,
        THIRD_LEVEL = 3
    };
    /* This section related to Sensor configuration.*/
    public const int SENSOR_FREQUENCY                                   = 5000;
    public const float SENSOR_RAY_DISTANCE                              = 0.65f;
    public const string OBSTACLE_LAYER_NAME                             = "Obstacle";

    public const int SCORE_COEFFICIENT                                  = 100;

    /* This game includes 3 levels. */
    public const int TOTAL_LEVEL_OF_NUMBERS                             = 3;

    public const int FL_NUMBER_OF_OBJECT_IN_THE_FIRST_SCENE             = 127;
    public const int FL_NUMBER_OF_OBJECT_IN_THE_SCENE                   = 135;
    public const int SL_NUMBER_OF_OBJECT_IN_THE_FIRST_SCENE             = 19;
    public const int SL_NUMBER_OF_OBJECT_IN_THE_SCENE                   = 357;
    public const int TL_NUMBER_OF_OBJECT_IN_THE_FIRST_SCENE             = 109;
    public const int TL_NUMBER_OF_OBJECT_IN_THE_SCENE                   = 305;

    public const float FL_THE_REDUCING_SCALE_FOR_OPEN_THE_GATE          = -0.25F;
    public const float FL_THE_REDUCING_YPOSITION_FOR_OPEN_THE_GATE      = 0.25f;

    public const float BEGIN_POSITION_OF_SECOND_GROUND                  = 8f;

    public const float SPEED_OF_CAMERA                                  = 0.25f;
    public const float SPEED_OF_HOLE                                    = 0.2f;

    public const float BEGIN_XPOS_OF_HOLE                               = -1.5F;
    public const float FALLING_THRESHOLD                                = -1f;
    public const float TIMER_FOR_OPEN_THE_GATE                          = 0.1f;

    public const float TIMER_FOR_NEXT_LEVEL_SCENE                       = 3.0f;

    public const float FG_BACK_BORDER                                   = -1.85f;
    public const float FG_TOP_BORDER                                    =  1.7f;
    public const float FG_RIGHT_BORDER                                  = -1.85f;
    public const float FG_LEFT_BORDER                                   = 1.85f;
    public const float SG_BACK_BORDER                                   = 8.2f;
    public const float SG_TOP_BORDER                                    = 11.7f;
    public const float SG_RIGHT_BORDER                                  = -1.7f;
    public const float SG_LEFT_BORDER                                   = 1.7f;

    /* This section for shake animation of camera.*/
    public const float SHAKE_DURATION                                   = 1f;
    public const float SHAKE_AMOUNT                                     = 0.7f;
    public const float SHAKE_DECREASE_FACTOR                            = 1f;

    /***************************************************/

    public const string ENEMY_NAME                                      = "Enemy";

    /* This section for Player Prefs. */
    public const string NEXT_LEVEL_NAME                                 = "NextLevelName";
    public const string NUMBER_OF_NEXT_LEVEL                            = "NumberOfNextLevel";
    public const string NUMBER_OF_CURRENT_LEVEL                         = "NumberOfCurrentLevel";
    public const string LEVEL_NAME_TO_RETURN                            = "LevelNameToReturn";
    public const string CURRENT_LEVEL_NAME                              = "CurrentLevelName";
    public const string SCORE                                           = "Score";

    /* This section for Scene names. */
    public const string NEXT_LEVEL_SCENE                                = "NextLevelTransitionScene";
    public const string GAME_OVER_SCENE                                 = "GameOverScene";



}
