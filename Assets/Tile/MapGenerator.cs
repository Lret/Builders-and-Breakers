using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public string tileType;
    public Vector3 tileRot;
    public GameObject[] subObjects;
    //public string tileState;
    //public float tileX;
    //public float tileY;

    // constructor
    public TileData(string tileType, Vector3 tileRot, GameObject[] subObjects) {
        this.tileType       = tileType;
        this.tileRot        = tileRot;
        this.subObjects     = subObjects;

    }
}

public class MapGenerator : MonoBehaviour {

    public Transform        tilePrefab;
    public Vector3          mapSize;
    /*
    public int mapSizeX;
    public int mapSizeY;
    public int mapSizeZ;
    */

    public Transform[,,]    mapLocator;

    public Transform mapHolder;

    //delegate void UpdateTiles(Transform[,,] mapLocation);
    //UpdateTiles updateTiles;

    // single deligate
    public delegate void TileFunc(Transform t);

    // event for update checking //other classes can subscribe to update
    // TileFunc updateTileFunc;
    public event TileFunc updateTileAction; // rescriction for all other classes to only subscribe or unsubscribe += / -=
    //public event Action<Transform> updateTileAction; // Action void return type with with/witout parameters
    //public event Func<Transform, int> updateTile      // function with custom return type and with/witout parameters

    int counterColor = 0;

    //float[,,] myarray = new float[10, 10,10];

    [Range(0, 1)]
    public float outlinePrecentage;
    private float outlinePrecentageCheck = 0;

    //public void GenerateMap(Vector3 mSize) {
    public Transform[,,] GenerateMap(Vector3 mSize){

        /*if (mSize.x <= 0 || mSize.y <= 0 || mSize.z <= 0) { return null; };
        mSize.x = (mSize.x <= 0) ? 1 : mSize.x;
        mSize.x = (mSize.y <= 0) ? 1 : mSize.y;
        mSize.x = (mSize.z <= 0) ? 1 : mSize.z;
        */

        //mapSize = new Vector3(mapSizeX, mapSizeY, mapSizeZ);

        Transform[,,] mapLocator = new Transform[(int)mapSize.x, (int)mapSize.y, (int)mapSize.z];

        
        string holderName = "Generated Map";
        if (transform.FindChild(holderName)){
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }
        //Transform mapHolder = new GameObject(holderName).transform;
        mapHolder = new GameObject(holderName).transform;
        //mapHolder.parent = transform;


        for (int x = 0; x < mSize.x; x++) {
            for (int y = 0; y < mSize.y; y++){
                for (int z = 0; z < mSize.z; z++){
                    Vector3 tilePos = new Vector3(-mapSize.x / 2 + x, 0.5f + y, -mapSize.z / 2 + z);
                    //Vector3     tilePos = new Vector3(-mapSize.x / 2 + x, -mapSize.y / 2 + y, -mapSize.z / 2 + z);
                    Transform   newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.up)) as Transform;
                    //Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;

                    

                    //newTile.localScale = new Vector3(1 - outlinePrecentage, 0.2f, 1 - outlinePrecentage);
                    newTile.localScale = new Vector3(1 - outlinePrecentage, 1 - outlinePrecentage, 1 - outlinePrecentage);
                    newTile.parent = mapHolder;
                    //newTile.gameObject.AddComponent<Rigidbody>(); // add this with a name in newtile

                    mapLocator[x, y, z] = newTile;
                    //mapLocator[(int) Mathf.Floor(x), (int) Mathf.Floor(y), (int)Mathf.Floor(y)] = newTile;
                }
            }
        }
        return mapLocator;
    }

    public void LogTiles(Transform[,,] mapLocation) {
        for (int x = 0; x < mapLocation.GetLength(0); x++)
        {
            for (int y = 0; y < mapLocation.GetLength(1); y++)
            {
                for (int z = 0; z < mapLocation.GetLength(2); z++)
                {
                    //Debug.Log(((x + z) / 2f) - (Mathf.Floor((x + z) / 2f)));
                    //decimal.Round(yourValue, 2, MidpointRounding.AwayFromZero); // decimalVar.ToString ("0.##"); // returns "0"  when decimalVar == 0
                    if ( ((x + y + z) / 2f) - (Mathf.Round( (x + y + z) / 2f) ) == 0) {
                        //Debug.Log("Destory:"+mapLocator[x, y, z]);
                        //DestroyImmediate(mapLocator[x, y, z].gameObject);
                        //mapLocator[x, y, z] = null;
                        mapLocation[x, y, z].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    }
                    //Debug.Log(mapLocation[x, y, z] + " at position: x: " + x + " y: " + y + " z: " + z);
                }
            }
        }
    }

    /*TODO make this a deligate
    public void ForEachDo(Transform[,,] mapLocation)
    {
        for (int x = 0; x < mapLocation.GetLength(0); x++)
        {
            for (int y = 0; y < mapLocation.GetLength(1); y++)
            {
                for (int z = 0; z < mapLocation.GetLength(2); z++)
                {

                    //return mapLocation[x, y, z];

                }
            }
        }
        //return null;
    }
    */// TODOEND

    //TODO make this a deligate
    public void ForEachDo(Transform[,,] mapLocation, TileFunc func)
    //public void ForEachDo(Transform[,,] mapLocation)
    {
        for (int x = 0; x < mapLocation.GetLength(0); x++)
        {
            for (int y = 0; y < mapLocation.GetLength(1); y++)
            {
                for (int z = 0; z < mapLocation.GetLength(2); z++)
                {
                    func(mapLocation[x, y, z]);
                    //return mapLocation[x, y, z];
                    //updateTileFunc(mapLocation[x, y, z]);

                }
            }
        }
        //return null;
    }
    // TODOEND

    public void moveUp(Transform t) {
        t.transform.Translate(Vector3.up * 1f);
    }

    // Use this for initialization
    void Start () {
        //GenerateMap(mapSize);


        mapLocator = GenerateMap(mapSize);
        LogTiles(mapLocator);

        /*updateTiles += ForEachDo;
        updateTiles(mapLocator);*/

       updateTileAction += moveUp;
        if (updateTileAction != null){
            //ForEachDo(mapLocator, updateTileFunc);
            ForEachDo(mapLocator, moveUp);
        }
        //ForEachDo(mapLocator);
    }
	
	// Update is called once per frame
	void Update () {

        if (mapSize != new Vector3(mapLocator.GetLength(0), mapLocator.GetLength(1), mapLocator.GetLength(2)) ) {
            //if (mapSizeX != mapLocator.GetLength(0) || mapSizeY != mapLocator.GetLength(1) || mapSizeZ != mapLocator.GetLength(2))){
            DestroyImmediate(mapHolder.gameObject);
            //if (mapLocator != null) { DestroyImmediate(mapLocator.Gam); };
            mapLocator = GenerateMap(mapSize);
        }

        if (outlinePrecentageCheck != outlinePrecentage) {
            ForEachDo(mapLocator, t =>  t.transform.localScale = new Vector3(1 - outlinePrecentage, 1 - outlinePrecentage, 1 - outlinePrecentage)  );
            outlinePrecentageCheck = outlinePrecentage;
        } 

        int preInt = 6;
        if (counterColor > preInt - 1){
            int x = (counterColor - preInt) % mapLocator.GetLength(0);
            int y = ((counterColor - preInt) / mapLocator.GetLength(0)) % mapLocator.GetLength(1);
            int z = ((counterColor - preInt) / (mapLocator.GetLength(0) * mapLocator.GetLength(1))) % mapLocator.GetLength(2);
            mapLocator[
                x,
                y,
                z
            ].GetComponent<Renderer>().material.SetColor("_Color",
                (((x + y + z) / 2f) - (Mathf.Round((x + y + z) / 2f)) == 0) ? new Color(0f,0f,0f,0.8f) : new Color(1f,1f,1f,0.8f)
            );
        }


        mapLocator[
            counterColor % mapLocator.GetLength(0),
            (counterColor / mapLocator.GetLength(0)) % mapLocator.GetLength(1),
            (counterColor / (mapLocator.GetLength(0) * mapLocator.GetLength(1))) % mapLocator.GetLength(2)
        ].GetComponent<Renderer>().material.SetColor("_Color", new Color(1f,0.92f,0.016f,0.8f));
        counterColor ++;
    }

    void updateTiles() {
        if (updateTileAction != null){
            ForEachDo(mapLocator, updateTileAction);
        }
    }
}
