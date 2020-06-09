using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Summary
 * A class that allows the player to recolor tiles they step over and interact with
 */ 
public class Recolorer : MonoBehaviour
{
    public LayerMask RecolorableLayers;

    //Keeps track of which tile the player is currently above
    protected GameObject _currentTileOn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateListener());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckTile(collision.gameObject))
        {
            GameObject foundTile = collision.transform.parent.gameObject;
            Debug.Log("Tile found. Name: " + foundTile.name);
            //If it's a tile, it should have a parent that has a child with the SpriteRenderer
            _currentTileOn = foundTile;
        }
    }
   
    /*
     * Checks if the passed GameObject is a tile. 
     * True if yes
     * False otherwise
     */
    protected virtual bool CheckTile(GameObject obj)
    {
        //Might have differect conditions to check for a tile later
        //Depends on it having the correct placement of the model in the hierarchy and being on the building layer for now
        Transform objParent = obj.transform.parent;
        bool hasModel = objParent != null && objParent.GetComponentInChildren<SpriteRenderer>() != null;

        bool inRecolorLayers = RecolorableLayers.value == (RecolorableLayers.value | (1 << objParent.gameObject.layer));
        Debug.Log("Checked object. inRecolorLayers: " + inRecolorLayers + ", hasModel: " + hasModel);
        return inRecolorLayers && hasModel;
    }

    protected virtual void RecolorTile()
    {
        //Could do something mor especific with new color later
        //For now, it assigns a random color
        Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        SpriteRenderer childSpriteRend = _currentTileOn.GetComponentInChildren<SpriteRenderer>();
        if (childSpriteRend != null)
            childSpriteRend.color = randomColor;
    }

    /*
     * Creates a listener for the "Interact" axis that will recolor _currentTileOn if pressed
     */
    protected virtual IEnumerator CreateListener()
    {
        bool running = true;
        while (running)
        {
            yield return null;
            if (_currentTileOn != null && Input.GetKeyDown(KeyCode.R))
            {
                RecolorTile();
            }
        }
    }
}
