using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    const byte num = 52;
    public GameObject cardItem;
    public Transform cardParent;
	void Start () {
        StartCoroutine(ShowAnimCard());
	}

    void InitUI()
    {

    }
    IEnumerator ShowAnimCard()
    {
        Sprite tx = Resources.Load<Sprite>("Texture/f002");
        for (byte i = 0; i < num; i++)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            GameObject item = Instantiate(cardItem, cardParent);
            item.name = i.ToString();
            item.GetComponent<Image>().sprite = tx;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = new Vector3(i + 1, 0);
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
