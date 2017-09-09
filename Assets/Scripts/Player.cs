using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum Weapon { SwordAndShield, Greatsword, Spear, FistWeapons }

public class Player : MonoBehaviour 
{
	public static Weapon weapon;

	public Image powerDisplay;
	public Image healthDisplay;


	private Image lipFillImage;

	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float maxPower;

	private float currentHealth;
	private float currentPower;

	public float Power
	{
		get { return currentPower; }
	}

	public float Health
	{
		get { return currentHealth; }
	}

	private void Start()
	{
		lipFillImage = healthDisplay.transform.parent.GetChild(0).GetComponent<Image>();
		currentHealth = maxHealth;
		currentPower = 0f;
	}

	public void TakeDamage (float amount)
	{
		currentHealth -= amount;
		float targetFillAmount = currentHealth / maxHealth;

		DOTween.To(() => healthDisplay.fillAmount, x => healthDisplay.fillAmount = x, targetFillAmount, 0.3f);
		DOTween.To(() => lipFillImage.fillAmount, x => lipFillImage.fillAmount = x, targetFillAmount, 1f);
	}

	public void ConsumeMana(float amount)
	{
		currentPower -= amount;
		powerDisplay.fillAmount = currentPower / maxPower;
		powerDisplay.transform.parent.GetChild(0).GetComponent<Image>().fillAmount = powerDisplay.fillAmount;
	}
}
