using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Base : ScriptableObject
public class TileGeneration : MonoBehaviour
{
    //public TileObject[] TileObjectList;
    //public List<TileObject> tileObjectList = new List<TileObject>();
    //public Vector3      TilePosition;
    //List<GameObject> tileObjectList = new List<GameObject>();
    List<TileObject> tileObjectList = new List<TileObject>();
    

    public int scalar = 10;
    public float disolver = 0.5f;
    //public GameObject exampleObject; 


    //public GameObject[] SubObjects;
    //public Hashtable


    //void Start() {
    //public List<TileSubObject> tileSubObjectList = new List<TileSubObject>();
    //TileSubObject.add
    //}

    //void start()

    void Start() {
        //
        for (int i = 0; i < 10; i++){
            for (int j = 0; j < 10; j++){
                GameObject cube             = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
                //GameObject cube = Instantiate(exampleObject);

                /*cube.transform.localScale   = new Vector3(1.0f, Random.Range(1f,2f), 1.0f);
                cube.GetComponent<Renderer>().material.SetColor(
                    "_Color",
                    new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))
                );*/

                /*
                //TileObject tObj = new TileObject(cube, new Vector3(i * scalar, 0f, j * scalar), null);
                GameObject tObj = new GameObject("Tile");
                //tObj.AddComponent(cube) as GameObject;
                cube.transform.parent = tObj.gameObject.transform;
                tObj.transform.position = new Vector3(i, 0f, j);
                */

                TileObject tObj = new TileObject(this, cube, new Vector3(i, 0f, j), null);

                           
                tileObjectList.Add(tObj);
            }
        }

        //
    }
}
