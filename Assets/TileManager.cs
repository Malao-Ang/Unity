using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] titlePrefs;
    public float zSpawn = 0;
    public float titleLength = 30;
    public int numberOfTile = 3;
    // Start is called before the first frame update
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();
    void Start()
    {
        for(int i=0;i<numberOfTile;i++){
            if(i==0){
                SpawnTile(0);
            }
            else{
                SpawnTile(Random.Range(0,titlePrefs.Length));
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - 35> zSpawn-(numberOfTile * titleLength)){
            SpawnTile(Random.Range(0,titlePrefs.Length));
            DeleteTitle();
        }
    }
    public void SpawnTile(int titleIndex){
        GameObject go =  Instantiate(titlePrefs[titleIndex],transform.forward * zSpawn,transform.rotation);
        activeTiles.Add(go);
        zSpawn+= titleLength;
    }
    private void DeleteTitle(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
