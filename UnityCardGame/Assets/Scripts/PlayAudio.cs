using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource aud;        // 音效來源：喇叭
    public AudioClip soundGetCard;  // 發牌

    public void event_Deck()
    {
        aud.PlayOneShot(soundGetCard);
    }
}
