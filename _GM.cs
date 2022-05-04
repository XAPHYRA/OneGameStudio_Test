using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _GM : MonoBehaviour
{
    Vector3 PrevPos = Vector3.zero, PosPos = Vector3.zero;
    public Transform[] Buts_0;

    float RotState;
    public Vector3 Sc_0, Sc_1, RSc_0, RSc_1, RSc_2;
    public Sprite[] But_Imgs_0;
    public Image CenterButt, FinalImg;
    public Image[] But_0, But_1, But_2;
    public GameObject[] Layers;
    int LayerId = 0, ButState = 0;
    public Color[] UiColor;
    public int[] ItemId;
    int SliderValue;
    bool ReadyToRotate = false;
    public Slider Slider;
    public Text SliderText, ItemText;
    public GameObject Section_1, Section_2, Back;
    public ParticleSystem Boom;

    private void Start()
    {
        Slider.maxValue = 100;      
    }

    public void EnableRot(bool RotPermission)
    {
        if (RotPermission == true)
        {
            ReadyToRotate = true;
        }
        else
        {
            ReadyToRotate = false;
        }
    }   

    public void CenterBut(int ButID)
    {
        if(LayerId == -1)
        {
            LayerId = 0;
            ButState = 0;
            Layers[0].transform.localScale = RSc_0;
            Layers[1].SetActive(false);
            Back.SetActive(false);

            foreach (Image x1 in But_0)
            {
                x1.color = UiColor[0];
            }
            return;
        }

        if (LayerId == 0)
        {            
            LayerId = 1;
            ButState = 4;            
            Layers[0].transform.localScale = RSc_1;
            Layers[1].transform.localScale = RSc_0;
            Layers[1].SetActive(true);
            Layers[2].SetActive(false);
            Section_1.SetActive(false);

            Back.SetActive(true);

            foreach (Image x1 in But_0)
            {
                x1.color = UiColor[1];
            }

            return;
        }
        if (LayerId == 1)
        {
            if (ButID == 0)
            {
                LayerId = 2;
                ButState = 8;
                Layers[0].transform.localScale = RSc_2;
                Layers[1].transform.localScale = RSc_1;
                Layers[2].SetActive(true);
                Section_1.SetActive(true);

                foreach (Image x2 in But_1)
                {
                    x2.color = UiColor[1];
                }
            }
            else
            {
                LayerId = -1;
                CenterBut(0);
            }
            return;
        }
        if (LayerId == 2)
        {
            if (ButID == 0)
            {
                Back.SetActive(false);
                Section_1.SetActive(false);
                Boom.Play();

                StartCoroutine(Result());
            }
            else
            {
                LayerId = 0;
                CenterBut(0);
                foreach (Image x2 in But_1)
                {
                    x2.color = UiColor[0];
                }
            }
        }
    }

    IEnumerator Result()
    {
        yield return new WaitForSeconds(1);
        Section_2.SetActive(true);
        ItemText.text = ItemId[0] + "-" + ItemId[1] + "-" + ItemId[2] + " X " + Slider.value;
        FinalImg.sprite = But_Imgs_0[ItemId[2] + ButState];
    }

    public void Thanks()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        SliderValue = (int)Slider.value;

        SliderText.text = SliderValue.ToString();

        RotState = Layers[LayerId].transform.localEulerAngles.z;

        if (Input.GetMouseButton(0) && ReadyToRotate == true)
        {
            PosPos = Input.mousePosition - PrevPos;
            Layers[LayerId].transform.Rotate(0, 0, -Vector3.Dot(PosPos, Camera.main.transform.right), Space.World);
        }
        PrevPos = Input.mousePosition;

        if (RotState < 90 && RotState >= 0)
        {
            Buts_0[0 + ButState].localScale = Sc_0;
            CenterButt.sprite = But_Imgs_0[0 + ButState];

            ItemId[LayerId] = 0;
        }
        else
        {
            Buts_0[0 + ButState].localScale = Sc_1;
        }

        if (RotState < 180 && RotState > 90)
        {
            Buts_0[1 + ButState].localScale = Sc_0;
            CenterButt.sprite = But_Imgs_0[1 + ButState];
            ItemId[LayerId] = 1;
        }
        else
        {
            Buts_0[1 + ButState].localScale = Sc_1;

        }
        if (RotState < 270 && RotState > 180)
        {
            Buts_0[2 + ButState].localScale = Sc_0;
            CenterButt.sprite = But_Imgs_0[2 + ButState];

            ItemId[LayerId] = 2;
        }
        else
        {
            Buts_0[2 + ButState].localScale = Sc_1;
        }
        if (RotState < 360 && RotState > 270)
        {
            Buts_0[3 + ButState].localScale = Sc_0;
            CenterButt.sprite = But_Imgs_0[3 + ButState];

            ItemId[LayerId] = 3;
        }
        else
        {
            Buts_0[3 + ButState].localScale = Sc_1;
        }
    }
}
