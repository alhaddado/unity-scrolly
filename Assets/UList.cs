using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public abstract class UIList<TCell> : MonoBehaviour where TCell : Component
{
	public GridLayoutGroup grid;
	
	protected List<TCell> cells = new List<TCell>(10);
	private List<TCell> pooledCells = new List<TCell>(10);
	
	[SerializeField]
	private TCell cellPrefab;
	
	public abstract int NumberOfCells();
	public abstract void UpdateCell(int index, TCell cell);
	
	public void Refresh()
	{
		int numCells = cells.Count;
		int numCellsRequired = NumberOfCells();
		
		// do we need to add?
		if (numCells < numCellsRequired)
		{
			int numCellsToAdd = numCellsRequired - numCells;
			
			for (int x = 0; x < numCellsToAdd; x++)
			{
				AddCell();
			}
			
		}
		// or remove?
		else if (numCellsRequired < numCells)
		{
			int numCellsToRem = numCells - numCellsRequired;
			
			for (int x = 0; x < numCellsToRem; x++)
				PushPooledCell(0);
		}
		
		ReloadCells();
			
	}
		
	#region Internals
	private void AddCell()
	{
		TCell cell;
		
		// if we have any unused cells, use one
		if (pooledCells.Count > 0)
		{
			cell = PopPooledCell();
			
		}
		else
		{
			// create a prefab instance
			cell = GameObject.Instantiate (cellPrefab) as TCell;
			
			// remove (Clone) part from the name
			cell.name = cell.name.Replace("(Clone)", "");
			
			// set parent
			cell.transform.SetParent(grid.transform);			
		}
		
		int order = cells.Count;
		cell.name = string.Format("Cell{0}", order);
		
		cell.transform.localScale = Vector3.one;
		
		// add it to the list
		cells.Add(cell);
	}
	
	public void RemoveAllCells()
	{
		// disable all games and put them in unused array
		while (cells.Count > 0)
		{
			PushPooledCell(0);
		}
	}
	
	public void ClearAllCells()
	{
		
		cells.Clear();
		pooledCells.Clear();
		
		Transform gridT = grid.transform;
		
		if (grid != null)
			for (int i = 0; i < gridT.childCount; i++)
			{
				Destroy(grid.transform.GetChild(i).gameObject);
			}
		
	}
	
	private void ReloadCells()
	{
		for (int x = 0; x < cells.Count; x++)
		{
			UpdateCell(x, cells[x]);
		}
	}
	
	private void PushPooledCell(int cellIndex)
	{
		// deactivate cell
		cells[cellIndex].gameObject.SetActive(false);
		
		// add it to the pool
		pooledCells.Add(cells[cellIndex]);
		
		// remove it from the used cells
		cells.RemoveAt(cellIndex);
	}
	
	private TCell PopPooledCell()
	{
		// activate the pooled cell
		pooledCells[0].gameObject.SetActive(true);
		
		// add it to our cell list
		cells.Add(pooledCells[0]);
		
		// remove it from the pool list
		pooledCells.RemoveAt(0);
		
		return cells[cells.Count-1];
	}
	#endregion
}

