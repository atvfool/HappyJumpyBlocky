using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button btnStartGame;

    public Rigidbody2D playerBlock;
    public Rigidbody2D pinkyBlock;

    // Start is called before the first frame update
    void Start()
    {
        btnStartGame.onClick.AddListener(btnStartGame_OnClick);
    }

    private void FixedUpdate()
    {
        if(playerBlock != null && pinkyBlock != null)
        {
            playerBlock.rotation -= 2f;
            pinkyBlock.rotation += 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void btnStartGame_OnClick()
    {
        SceneManager.LoadScene("TheFirstLevel");
    }
}
