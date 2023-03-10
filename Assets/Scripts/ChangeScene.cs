using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Update(){
        ChangeSceneOnTaskCompletion();
    }
    public void ChangeSceneOnTaskCompletion()
    {
        bool coinExists = false;
        foreach (Transform trans in gameObject.transform) {
            GameObject obj = trans.gameObject;
            if (obj.tag == "Coins") {
                coinExists = true;
            }
        }
        if (!coinExists){
            SceneManager.LoadScene("GameOver");
        }
    }
}
