using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text zombieKilledNum;
    private int killedNum;
    
    
    // Start is called before the first frame update
    void Start()
    {
        killedNum = 0;
        killedNum = ZombieScript.score;
        zombieKilledNum.text = "Zombie Killed: " + killedNum;
    }

}
