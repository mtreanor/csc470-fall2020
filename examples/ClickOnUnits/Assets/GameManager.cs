using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public UnitScript currentSelection;

	public GameObject namePanel;
	public Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// This function takes a Unit's UnitScript, makes it selected, and deselects any other units that were selected.
	// If null is passed in, it will just deselect everything.
	// This function also populates the nameText UI element with the currently selected unit's name, and also ensures
	// that the namePanel UI element is only active is a unit is selected.
	public void SelectUnit(UnitScript toSelect)
	{
		// Get an array of all GameObjects that have the tag "Unit".
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		// Loop through all units and make sure they are not selected.
		for (int i = 0; i < units.Length; i++) {
			UnitScript unitScript = units[i].GetComponent<UnitScript>();
			unitScript.selected = false;
			unitScript.UpdateVisuals();
		}
		
		
		if (toSelect != null) {
			// If there is a selected, mark it as selected and update the nameText UI element.
			toSelect.selected = true;
			nameText.text = toSelect.name;
			namePanel.SetActive(true);
			toSelect.UpdateVisuals();
		} else {
			// If we get in here, it means that the toSelect parameter was null, and that means that we 
			// should deactivate the namePanel.
			namePanel.SetActive(false);
		}
	}
}
