using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollListTest : UIList<ScrollCellTest> {

	public InputField numCellsInput;

	#region implemented abstract members of UIList

	public override int NumberOfCells ()
	{
		int numCells = 0;
		int.TryParse(numCellsInput.text, out numCells);
		return numCells;
	}

	public override void UpdateCell (int index, ScrollCellTest cell)
	{
		cell.cellLabel.text = "Cell: " + index.ToString();
	}

	#endregion

	public void OnCreateList()
	{
		int cells = 0;

		int.TryParse(numCellsInput.text, out cells);

		if (cells > 0)
		{
			// Delete the old list
			ClearAllCells();
			
			Refresh();

		}

		
	}
}
