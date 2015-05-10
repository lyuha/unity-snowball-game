using UnityEngine;
using System.Collections;

public class DamageableBox : MonoBehaviour, IDamageable {
	public bool TakeDamage(IDamage damage) {
		Debug.Log("DamageableBox Hit");
		Destroy(this.gameObject);

		return true;
	}
}
