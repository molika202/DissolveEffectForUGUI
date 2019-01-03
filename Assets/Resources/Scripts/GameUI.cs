using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Coffee.UIExtensions;
public class GameUI : MonoBehaviour {

    const byte num = 52;
    public GameObject cardItem;
    public Transform cardParent,showCardParent;
    public Text jushu, fenshu,cishu;
    public GameObject endUI,maskUI;
    public Button startBtn, endGameBtn;
    int jushuNum=1, fenshuNum=0,cishuNum=2;
    RandomHelper randomHelp = new RandomHelper();
	void Start () {
        cardItem.SetActive(false);
        SetTxtMsg();
        endGameBtn.onClick.AddListener(() => { Application.Quit(); });
        startBtn.onClick.AddListener(() => { 
            cishuNum = 2;
            clickNum = 0;
            jushuNum += 1;
            SetTxtMsg();
            StartCoroutine(DestroyUI()); 
        });
        StartCoroutine(ShowAnimCard());
	}
    void SetTxtMsg()
    {
        jushu.text = DissolveRandom.JUSHU + jushuNum;
        fenshu.text = DissolveRandom.FENSHU + fenshuNum;
        cishu.text = DissolveRandom.CISHU + cishuNum;
    }
    IEnumerator DestroyUI()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        endUI.SetActive(false);
        maskUI.SetActive(true);
        int count = showCardParent.childCount;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSecondsRealtime(0.15f);
            Destroy(showCardParent.GetChild(0).gameObject);
        }
        int allCard = cardParent.childCount;
        for (int i = 0; i < allCard; i++)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            Destroy(cardParent.GetChild(cardParent.childCount-1).gameObject);
        }
        maskUI.SetActive(false);
        StartCoroutine(ShowAnimCard());
    }
    IEnumerator ShowAnimCard()
    {
        maskUI.SetActive(true);
        Sprite tx = Resources.Load<Sprite>("Texture/f002");
        for (byte i = 0; i < num; i++)
        {
            GameObject item = Instantiate(cardItem, cardParent);
            item.name = i.ToString();
            item.GetComponent<Image>().sprite = tx;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = Vector3.one;
            yield return new WaitForSecondsRealtime(0.02f);
            item.transform.localPosition = new Vector3((i + 1)/2, -i/2);
            item.SetActive(true);
        }
        Transform trans = cardParent.GetChild(cardParent.childCount-1);
        Vector3 transV3 = trans.localPosition;
        Transform oneCard = Instantiate(trans,cardParent);
        int rNum=DissolveRandom.INSTANCE.GetTypeRanDom(0 , num+1);//数字 
        int[] dataList = new int[5];//会出现相同元素在此数组中
        dataList[0]=rNum;
        for (int i = 1; i < 5; i++)
        {
            int tNum=DissolveRandom.INSTANCE.GetTypeRanDom(0 , num+1);
            dataList[i] = tNum != rNum ? tNum : rNum;
        }
        randomHelp.GetRandomArray(dataList);
        //oneCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/"+rNum);//加载对应的图片
        oneCard.name = rNum.ToString();
        oneCard.localPosition = transV3;
        oneCard.localRotation = Quaternion.identity;
        oneCard.localScale = Vector3.one;
        yield return new WaitForSecondsRealtime(0.05f);
        oneCard.DOLocalMoveY(-(transV3.y + 500), 0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < dataList.Length; i++)
        {
            //GameObject cellData = Instantiate(cardItem,showCardParent);
            GameObject cellData = Instantiate(cardItem,showCardParent.parent);
            cellData.name = dataList[i].ToString();
            cellData.transform.localPosition = transV3;
            cellData.transform.localRotation = Quaternion.identity;
            cellData.transform.localScale = Vector3.one;
            cellData.transform.SetAsFirstSibling();
            cellData.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/" + "f002" /*dataList[i]*/);//加载对应的图片
            yield return new WaitForSecondsRealtime(0.25f);
            cellData.SetActive(true);
            cellData.transform.DOLocalMoveY(showCardParent.localPosition.y+100, 0.3f).OnComplete(() => { cellData.transform.SetParent(showCardParent); cellData.transform.localScale = Vector3.one; });
            cellData.AddComponent<Button>().onClick.AddListener(() => {
                cishuNum -= 1;

                clickNum++;
                int tNum;
                int.TryParse(cellData.name, out tNum);
                Debug.LogError(tNum + " : " + rNum);
                if (tNum == rNum && clickNum == 1)
                {
                    fenshuNum += 100;
                    endUI.SetActive(true);
                }
                else if (tNum == rNum)
                {
                    fenshuNum += 50;
                    endUI.SetActive(true);
                }
                SetTxtMsg();
                if (cishuNum == 0)
                {
                    endUI.SetActive(true);
                    return;
                }
            });
        }
        yield return new WaitForSecondsRealtime(0.9f);
        maskUI.SetActive(false);
    }
    int clickNum = 0;//用于判断是第几次选中正确的目标从而增加对应的积分 
}
