using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void OnClickBreakBreakButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickTurnTurnButton()
    {
        SceneManager.LoadScene(2);
    }
}
