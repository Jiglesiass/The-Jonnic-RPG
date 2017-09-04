using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum Weapon { SwordAndShield, Greatsword, Spear, FistWeapons }

public class Player : MonoBehaviour 
{
	public Image manaDisplay;
	public Image healthDisplay;

	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float maxMana;

	private float currentHealth;
	private float currentMana;

	public float Mana
	{
		get { return currentMana; }
	}

	private float manaRegenPerSec = 5f;

	private void Start()
	{
		currentHealth = maxHealth;
		currentMana = maxMana;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			ConsumeMana(10f);
		}

		ManaRegen();
	}

	public void TakeDamage (float amount)
	{
		currentHealth -= amount;
		healthDisplay.fillAmount = currentHealth / maxHealth;
		healthDisplay.transform.parent.GetChild(0).GetComponent<Image>().fillAmount = healthDisplay.fillAmount;
	}

	public void ConsumeMana(float amount)
	{
		currentMana -= amount;
		manaDisplay.fillAmount = currentMana / maxMana;
		manaDisplay.transform.parent.GetChild(0).GetComponent<Image>().fillAmount = manaDisplay.fillAmount;
	}

	private void ManaRegen()
	{
		if (currentMana < maxMana)
		{
			currentMana += manaRegenPerSec * Time.deltaTime;
			manaDisplay.fillAmount = currentMana / maxMana;
			manaDisplay.transform.parent.GetChild(0).GetComponent<Image>().fillAmount = manaDisplay.fillAmount;
		}
	}
}
