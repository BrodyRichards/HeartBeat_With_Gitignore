using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEmoControl : MonoBehaviour
{
    public Sprite happy; //1
    public Sprite sad; //2
    public Sprite surprised; //3
    public Sprite mad; //4
    public Sprite grovin; //5
    public Sprite hurt; //6

    public static string ReactEmo;
    public static bool NoEmoAtm;
    
    private SpriteRenderer sr;


    private void Awake()
    {
        ReactEmo = "";
        NoEmoAtm = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ReactEmo !="" && NoEmoAtm)
        {

            if (ReactEmo != "Held Rabbit" && ReactEmo != "Happy Song")
            {
                NoEmoAtm = false;
                Invoke("ResetEmo", 1.0f);
            }
            else if (ReactEmo == "Happy Song" || ReactEmo == "Sad Song")
            {

                if (!MusicKidBT.mcIsAffected || (ReactEmo == "Happy Song" && MusicKidBT.currentMood == 1)
                || (ReactEmo == "Sad Song" && MusicKidBT.currentMood == 0))
                {
                    ResetEmo();
                }
            }
            else if (ReactEmo=="Held Rabbit")
            {

                //Debug.LogError("Being carried is " + RabbitJump.beingCarried);
                if (!RabbitJump.beingCarried)
                {
                    ResetEmo();
                }
            }


        }

        EmoSwitcher(ReactEmo);

    }

    void EmoSwitcher(string s)
    {
        switch (s)
        {
            case "Played catch":
                sr.sprite = happy;
                break;
            case "Sad Song":
                sr.sprite = sad;
                break;
            case "Held Rabbit":
                sr.sprite = surprised;
                break;
            case "Hit by ball":
                sr.sprite = mad;
                break;
            case "Happy Song":
                sr.sprite = grovin;
                break;
            case "Bit by rabbit":
                sr.sprite = hurt;
                break;
            default:
                sr.sprite = null;
                break;

        }
    }

    private void ResetEmo()
    {
        ReactEmo = "";
        NoEmoAtm = true;
    }
}
