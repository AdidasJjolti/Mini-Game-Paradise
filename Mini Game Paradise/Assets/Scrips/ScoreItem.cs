using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _highScores;
    [SerializeField] TextMeshProUGUI[] _highScoreHeads;
    [SerializeField] Image[] _newIcons;

    void Awake()
    {
        var texts = FindObjectsOfType<TextMeshProUGUI>(true);
        Image[] images = FindObjectsOfType<Image>(true);

        // SerializeField로 세팅한 배열 크기가 5가 아닌 경우에 대비한 방어 코드
        if (_highScores.Length < 5)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "Score" && text.transform.parent.transform.name == "Record")
                            {
                                _highScores[0] = text;
                            }

                            if (text.transform.name == "Head" && text.transform.parent.transform.name == "Record")
                            {
                                _highScoreHeads[0] = text;
                            }
                        }

                        foreach (var image in images)
                        {
                            if (image.transform.name == "New" && image.transform.parent.transform.name == "Record")
                            {
                                _newIcons[0] = image;
                            }
                        }
                        break;
                    case 1:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "Score" && text.transform.parent.transform.name == "Record (1)")
                            {
                                _highScores[1] = text;
                            }

                            if (text.transform.name == "Head" && text.transform.parent.transform.name == "Record (1)")
                            {
                                _highScoreHeads[1] = text;
                            }
                        }

                        foreach (var image in images)
                        {
                            if (image.transform.name == "New" && image.transform.parent.transform.name == "Record (1)")
                            {
                                _newIcons[1] = image;
                            }
                        }
                        break;
                    case 2:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "Score" && text.transform.parent.transform.name == "Record (2)")
                            {
                                _highScores[2] = text;
                            }

                            if (text.transform.name == "Head" && text.transform.parent.transform.name == "Record (2)")
                            {
                                _highScoreHeads[2] = text;
                            }
                        }

                        foreach (var image in images)
                        {
                            if (image.transform.name == "New" && image.transform.parent.transform.name == "Record (2)")
                            {
                                _newIcons[2] = image;
                            }
                        }
                        break;
                    case 3:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "Score" && text.transform.parent.transform.name == "Record (3)")
                            {
                                _highScores[3] = text;
                            }

                            if (text.transform.name == "Head" && text.transform.parent.transform.name == "Record (3)")
                            {
                                _highScoreHeads[3] = text;
                            }
                        }

                        foreach (var image in images)
                        {
                            if (image.transform.name == "New" && image.transform.parent.transform.name == "Record (3)")
                            {
                                _newIcons[3] = image;
                            }
                        }
                        break;
                    case 4:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "Score" && text.transform.parent.transform.name == "Record (4)")
                            {
                                _highScores[4] = text;
                            }

                            if (text.transform.name == "Head" && text.transform.parent.transform.name == "Record (4)")
                            {
                                _highScoreHeads[4] = text;
                            }
                        }

                        foreach (var image in images)
                        {
                            if (image.transform.name == "New" && image.transform.parent.transform.name == "Record (4)")
                            {
                                _newIcons[4] = image;
                            }
                        }
                        break;
                }
            }
        }
    }

    public void SwitchOnNewIcon(int i)
    {
        if(i < 0)
        {
            i = 0;
        }
        _newIcons[i].gameObject.SetActive(true);
    }

    public void SwitchOffNewIcon(int i)
    {
        _newIcons[i].gameObject.SetActive(false);
    }

    public void SetHighScores(int[] record)
    {
        _highScores[record[0]].text = string.Format("{0:#,###}", record[1]);
    }

    public void SetRankings(int[] record)
    {
        _highScoreHeads[record[0]].text = record[1].ToString() + ".";
    }
}
