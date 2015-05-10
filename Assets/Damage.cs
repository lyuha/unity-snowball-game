using UnityEngine;
using System.Collections;

public interface IDamage {
	int GetDamageAmount();
	GameObject GetDamageSource();
}
