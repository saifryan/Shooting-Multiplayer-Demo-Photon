using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
	public ItemInfoScript itemInfo;
	public GameObject itemGameObject;

	public abstract void Use();
}
