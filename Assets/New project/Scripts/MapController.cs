using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject[] OutWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] EnemyArray;
    public GameObject exitPrefab;

    public int rows = 10;
    public int cols = 10;
    public int minWall = 2;
    public int maxWall = 8;
 
    private Transform mapHolder;
    private List<Vector2> positionList=new List<Vector2>();

    // Start is called before the first frame update
    
    void Start()
    {
        CreatMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatMap()
    {
        mapHolder = new GameObject("Map").transform;
        for (int x = 0; x < rows; x++) 
        {
            for (int y = 0; y < cols; y++)
            {
                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1)
                {
                    int wallIndex = Random.Range(0,OutWallArray.Length);
                    GameObject go1=GameObject.Instantiate(OutWallArray[wallIndex], new Vector3(x, y, 0), Quaternion.identity)as GameObject;
                    go1.transform.SetParent(mapHolder);
                }
                else
                {
                    int floorIndex = Random.Range(0,floorArray.Length);
                    GameObject go2 = GameObject.Instantiate(floorArray[floorIndex], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go2.transform.SetParent(mapHolder);
                }
                
            }
        }

        for (int x = 2;x < rows-2; x++)
        {
            for (int y = 2; y < cols - 2; y++)
            {
                positionList.Add(new Vector2(x, y));
            }
           
        }
        int wallCount = Random.Range(minWall, maxWall+1);
        for (int i = 0; i < wallCount; i++)
        {
            int positionIndex = Random.Range(0, positionList.Count);
            Vector2 pos = positionList[positionIndex];
            positionList.RemoveAt(positionIndex);
            int wallIndex = Random.Range(0, wallArray.Length);
            GameObject go3 = GameObject.Instantiate(wallArray[wallIndex], pos, Quaternion.identity) as GameObject;
            go3.transform.SetParent(mapHolder);
        }

        int foodCount = Random.Range(2,GamingManager.Instance.level*2 );
        for (int i = 0; i < foodCount; i++)
        {
            int positionIndex = Random.Range(0, positionList.Count);
            Vector2 pos = positionList[positionIndex];
            positionList.RemoveAt(positionIndex);
            int foodIndex = Random.Range(0, foodArray.Length);
            GameObject go4 = GameObject.Instantiate(foodArray[foodIndex], pos, Quaternion.identity) as GameObject;
            go4.transform.SetParent(mapHolder);
        }

        int enemyCount = GamingManager.Instance.level/2+1;
        for (int i = 0; i < enemyCount; i++)
        {
            int positionIndex = Random.Range(0, positionList.Count);
            Vector2 pos = positionList[positionIndex];
            positionList.RemoveAt(positionIndex);
            int enemyIndex = Random.Range(0, EnemyArray.Length);
            GameObject go5 = GameObject.Instantiate(EnemyArray[enemyIndex], pos, Quaternion.identity) as GameObject;
            go5.transform.SetParent(mapHolder);
        }
        GameObject go6 = GameObject.Instantiate(exitPrefab, new Vector2(rows-2,cols-2), Quaternion.identity) as GameObject;
        go6.transform.SetParent(mapHolder);
    }
}
