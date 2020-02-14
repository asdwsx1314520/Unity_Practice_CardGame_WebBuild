using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region KID
    [Header("卡片陣列")]
    public MeshFilter[] cards;
    [Header("發牌按鈕")]
    public Button btnGetCard;

    public Animator ani_deck;
    public MeshFilter m_pc, m_player;
    
    private int player, pc;     // 玩家、電腦卡片編號

    //發牌
    public GameObject settlement;
    public Text text_settlement;

    //換牌
    public GameObject changView;

    private void Start()
    {
        aud = GetComponent<AudioSource>();

        player = shuffle();
        pc = shuffle();
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    /// <returns></returns>
    int shuffle()
    {
        int r = Random.Range(0, cards.Length);

        return r;
    }

    /// <summary>
    /// 遊戲開始
    /// </summary>
    public void PlayerGetCard()
    {
        btnGetCard.interactable = false;
        changeCards();
        ani_deck.SetTrigger("licensing");

        Invoke("openChangeView", 2.0f);
    }

    /// <summary>
    /// 更換卡片顯示
    /// </summary>
    private void changeCards()
    {
        m_player.sharedMesh = cards[player].sharedMesh;
        m_pc.sharedMesh = cards[pc].sharedMesh;
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="pos">卡片座標</param>
    /// <returns>取得的卡片編號</returns>
    //private int GetCard(Vector3 pos)
    //{
    //    aud.PlayOneShot(soundGetCard);

    //    int r = Random.Range(0, cards.Length);

    //    Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

    //    return r + 1;
    //}
    #endregion

    #region 練習區域
    [Header("音效區域")]
    public AudioClip soundGetCard;  // 發牌
    public AudioClip soundWin;      // 獲勝
    public AudioClip soundLose;     // 失敗
    public AudioClip soundTie;      // 平手

    private AudioSource aud;        // 音效來源：喇叭


    /// <summary>
    /// 勝負顯示：使用玩家與電腦取得卡片判斷獲勝、平手或失敗
    /// 玩家卡片編號：player
    /// 電腦卡片編號：pc
    /// 顯示結算畫面
    /// </summary>
    private void GameWinner()
    {
        settlement.SetActive(true);

        if (player > pc)
        {
            text_settlement.text = "獲勝";
            aud.PlayOneShot(soundWin);
        }
        else if(player < pc)
        {
            text_settlement.text = "輸了";
            aud.PlayOneShot(soundLose);
        }
        else
        {
            text_settlement.text = "平手";
            aud.PlayOneShot(soundTie);
        }
    }

    /// <summary>
    /// 開啟更換的畫面
    /// </summary>
    public void openChangeView()
    {
        changView.SetActive(true);
    }

    /// <summary>
    /// 重新開始
    /// </summary>
    public void again()
    {
        SceneManager.LoadScene("練習場景");
    }

    /// <summary>
    /// 實際進行換牌
    /// </summary>
    public void changeValue()
    {
        int proxy;

        proxy = player;
        player = pc;
        pc = proxy;
    }

    /// <summary>
    /// 換牌
    /// </summary>
    public void yesChange()
    {
        changView.SetActive(false);
        changeValue();
        changeCards();
        ani_deck.SetTrigger("showdown");
        Invoke("GameWinner", 2);
    }

    /// <summary>
    /// 不換牌
    /// </summary>
    public void noChange()
    {
        changView.SetActive(false);
        ani_deck.SetTrigger("showdown");
        Invoke("GameWinner", 2);
    }

    
    #endregion
}
