using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollListTest : MonoBehaviour {

	public GameObject cellPrefab;

	public GridLayoutGroup grid;

	public InputField numCellsInput;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCreateList()
	{
		int cells = 0;

		int.TryParse(numCellsInput.value, out cells);

		if (cells > 0)
		{
			// Delete the old list

			for (int x = 0; x < grid.transform.childCount;  x++)
			{
				GameObject.Destroy(grid.transform.GetChild(x).gameObject);
			}
			

			// Make the new list

			for (int i = 0; i < cells; i++)
			{
				GameObject cell = (GameObject) GameObject.Instantiate(cellPrefab);
				cell.transform.SetParent(grid.transform);
			}
		}

		
	}
}
