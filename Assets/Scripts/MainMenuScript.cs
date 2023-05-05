using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private Transform levelSelectPanel;

    [SerializeField] private List<Button> levelSelectButton;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex");
        //levelSelectPanel = GameObject.Find("LevelsPanel").transform;
        //levelSelectPanel.parent.gameObject.SetActive(false);

        for (int i = 0; i < levelSelectPanel.childCount; i++)
        {
            Button btn = levelSelectPanel.GetChild(i).GetComponent<Button>();
            btn.interactable = i <= currentLevelIndex;

            int index = i;

            btn.onClick.AddListener(() => LoadLevel(index));
            
            levelSelectButton.Add(btn);
        }        
    }

    private void Start()
    {
        AdmobBanner.Instance.RequestBanner();
    }

    void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("loadedLevel" , level);
        GameManager.Instance.rewardCount = 0;
        SceneManager.LoadScene(1);
    }
}
