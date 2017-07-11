using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class TileObject : MonoBehaviour {
public class TileObject{
    TileGeneration  parent;
    GameObject      tileObject;
    Vector3         position;
    Hashtable       subObjects;

    /*void Start() {
        Debug.Log(position);
        tileObject.transform.position = position;
    }*/

    public TileObject(TileGeneration parent, GameObject newTileObject, Vector3 newPosition, Hashtable newSubObjects) {
        tileObject  = newTileObject;

        position    = newPosition;

        if (newSubObjects != null) {
            subObjects = newSubObjects;
        }
        //subObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(0, 0, 0)); //test

        //Debug.Log(position);
        tileObject.transform.position = position;

        Vector3 rand = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        tileObject.transform.localScale = new Vector3(1.0f, 1 + rand.y, 1.0f);
        tileObject.GetComponent<Renderer>().material.SetColor(
            "_Color",
            new Color(rand.x, rand.y, rand.z, (parent.disolver < rand.y)?1f:0f )
        );
        /*if (rand.y < 0.5) {
            GameObject.Destroy(this.tileObject,0f);
        }*/
    }

    public void TileSubObjectLoop(Hashtable h) {
        foreach (DictionaryEntry entry in h)
        {
            Debug.Log(entry.Key + "" + entry.Value);
        }
        /*var h = { "fruit":"apple", "veggie":"squash"};
        //    for (item in hash)
        //      Debug.Log(item.Key + ":" + item.Value);
        for (item in h.Values){
            Debug.Log(item);
        }*/
    }
}
