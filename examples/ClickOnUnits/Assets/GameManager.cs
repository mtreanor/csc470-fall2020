using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public UnitScript selectedUnit;

	public GameObject namePanel;
	public Text nameText;
	public MeterScript healthMeter;
	public MeterScript charismaMeter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	}
    
    public void GoButtonClicked()
	{
		if (selectedUnit != null) {
			selectedUnit.StartFollowingPath();
		}
	}

	// This function takes a Unit's UnitScript, makes it selected, and deselects any other units that were selected.
	// If null is passed in, it will just deselect everything.
	// This function also populates the nameText UI element with the currently selected unit's name, and also ensures
	// that the namePanel UI element is only active is a unit is selected.
	public void SelectUnit(UnitScript toSelect)
	{
		selectedUnit = toSelect;
	
		// Get an array of all GameObjects that have the tag "Unit".
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		// Loop through all units and make sure they are not selected.
		for (int i = 0; i < units.Length; i++) {
			UnitScript unitScript = units[i].GetComponent<UnitScript>();
			unitScript.selected = false;
			unitScript.UpdateVisuals();
		}
		
		
		if (toSelect != null) {
			// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
			selectedUnit.selected = true;

			UpdateUI(selectedUnit);
			
			selectedUnit.UpdateVisuals();
		} else {
			// If we get in here, it means that the toSelect parameter was null, and that means that we 
			// should deactivate the namePanel.
			namePanel.SetActive(false);
		}
	}
	
	public void UpdateUI(UnitScript unit)
	{
		healthMeter.SetMeter(unit.health / 100f);
		charismaMeter.SetMeter(unit.charisma / 18f);
		nameText.text = unit.unitName;
		namePanel.SetActive(true);
	}
}
