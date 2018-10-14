﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout  {

	[System.Serializable]
	public struct rowData{
		public bool[] row;
	}

	public rowData[] rows = new rowData[16]; //Grid of 7x7
}
