using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicUI : MonoBehaviour
{
    //go to scene by index
    public void GoToScene(int sceneIndex) { SceneManager.LoadScene(sceneIndex); }

}
