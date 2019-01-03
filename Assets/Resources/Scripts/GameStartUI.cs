using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour {

    public Button beginBtn;
    public GameUI game;

    void Start()
    {
        beginBtn.onClick.AddListener(() => { game.gameObject.SetActive(true); gameObject.SetActive(false); });
    }

}
