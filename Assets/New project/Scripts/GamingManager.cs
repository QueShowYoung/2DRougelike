using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamingManager : MonoBehaviour
{
    private static GamingManager _instance;
    private bool sleepStep=true;

    public Text foodText;
    public Text deadText;
    public  static GamingManager Instance
    {
        get
        {
            return _instance;
        }
      
    }
    
    
    public int level = 1;
    public int food = 10;
    public List<EnemyController> enemyList = new List<EnemyController>(); 

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
        initGame();
        
    }

    void initGame()
    {
        foodText = GameObject.Find("FoodText").GetComponent<Text>();
        updateFoodText();
        deadText = GameObject.Find("DeadText").GetComponent<Text>();
        deadText.enabled = false;
    }
    
    public void foodAdd(int count)
    {
        food += count;
        updateFoodText();
    }
    public void foodReduce(int count)
    {
        food -= count;
        updateFoodText();
    }
    void updateFoodText()
    {
        foodText.text = "Food:" + food;
    }

    public void OnPlayerMove()
    {
        if (sleepStep)
        {
            sleepStep = false;
        }
        else
        {
            foreach (var EnemyController in enemyList)
            {
                EnemyController.Move();
            }
            sleepStep = true;
        }
    }
    
}
