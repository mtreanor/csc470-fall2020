using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public Dropdown dropdown;

	public static GameManager instance;

	public enum MODES { FADING, GAMEPLAY, TEXTBOX };
	public MODES mode;

	public TMP_Text textboxText;
	public GameObject textbox;
	public string[] textEntries;

	public Image fadeImage;
	float fadeRate = 0.9f;
	Coroutine fadeCoroutine;

	public int score = 0;

	private void Awake()
	{
		// The Singleton pattern.
		if (instance != null && instance != this) {
			// Enforce that there is only one GameManager.
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}

		score = PlayerPrefs.GetInt("score");
	}

	// Start is called before the first frame update
	void Start()
	{
		mode = MODES.FADING;
		fadeCoroutine = StartCoroutine(fadeIn());
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.I)) {
			StopCoroutine(fadeCoroutine);
			fadeCoroutine = StartCoroutine(fadeIn());
		}

		if (Input.GetKeyDown(KeyCode.O)) {
			StopCoroutine(fadeCoroutine);
			fadeCoroutine = StartCoroutine(fadeOut());
		}
	}


	IEnumerator fadeIn()
	{
		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
		while (fadeImage.color.a >= 0) {
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - fadeRate * Time.deltaTime);
			yield return null;
		}

		StartCoroutine(introTextSequence());
	}

	IEnumerator fadeOut()
	{
		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
		while (fadeImage.color.a < 1) {
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + fadeRate * Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator introTextSequence()
	{
		mode = MODES.TEXTBOX;

		textbox.SetActive(true);
		textboxText.text = textEntries[0];
		int count = 0;
		while (count < textEntries.Length) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				count++;
				if (count < textEntries.Length) {
					textboxText.text = textEntries[count];
				}
			}
			yield return null;
		}
		textbox.SetActive(false);

		mode = MODES.GAMEPLAY;
	}
}
