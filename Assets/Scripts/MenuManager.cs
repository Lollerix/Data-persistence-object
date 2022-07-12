using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputName;

    private void Awake()
    {
        if (DataManager.Instance != null)
        {
            inputName.text = DataManager.Instance.playerName;
        }
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        DataManager.Instance.playerName = inputName.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

    }
}
