using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Matrix : MonoBehaviour
{
    private int[,] matrix = new int[4, 4] {
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 }
    };

    private int posX = 0;
    private int posY = 0;
    private int value = 2;
    private int emptySpaces = 16;

    public TextMeshProUGUI[] line0 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] line1 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] line2 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] line3 = new TextMeshProUGUI[4];

    void Start()
    {
        GenerateBlock();
        GenerateBlock();
        ChangeMatrix();
    }

    void Update()
    {
        SearchEmptySpaces();

        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateBlock();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
            JoinBlocksLeft();
            MoveLeft();
            GenerateBlock();
            ChangeMatrix();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
            JoinBlocksRight();
            MoveRight();
            GenerateBlock();
            ChangeMatrix();
        } 
    }

    private void ChangeMatrix()
    {
        for (int i = 0; i < 4; i++)
        {
            if (matrix[0, i] == 0)
            {
                line0[i].transform.GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                line0[i].transform.GetComponent<TextMeshProUGUI>().text = matrix[0, i].ToString();
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (matrix[1, i] == 0)
            {
                line1[i].transform.GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                line1[i].transform.GetComponent<TextMeshProUGUI>().text = matrix[1, i].ToString();
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (matrix[2, i] == 0)
            {
                line2[i].transform.GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                line2[i].transform.GetComponent<TextMeshProUGUI>().text = matrix[2, i].ToString();
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (matrix[3, i] == 0)
            {
                line3[i].transform.GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                line3[i].transform.GetComponent<TextMeshProUGUI>().text = matrix[3, i].ToString();
            }
        }
    }

    private void PrintMatrix()
    {
        Debug.Log("\n" +
            matrix[0, 0] + " " + matrix[0, 1] + " " + matrix[0, 2] + " " + matrix[0, 3] + "\n" +
            matrix[1, 0] + " " + matrix[1, 1] + " " + matrix[1, 2] + " " + matrix[1, 3] + "\n" +
            matrix[2, 0] + " " + matrix[2, 1] + " " + matrix[2, 2] + " " + matrix[2, 3] + "\n" +
            matrix[3, 0] + " " + matrix[3, 1] + " " + matrix[3, 2] + " " + matrix[3, 3] + "\n"
        );
    }

    private void GenerateBlock()
    {
        if(emptySpaces > 0)
        {
            bool okay = false;
            posX = Random.Range(0, 4);
            posY = Random.Range(0, 4);

            while (!okay) // IMPEDIR QUE GERE 4 E 4 !!!!!!!
            {
                if (matrix[posY, posX] == 0)
                {
                    if(emptySpaces == 15 && value == 4)
                    {
                        value = 2;
                    }
                    else
                    {
                        if (Random.Range(1, 28) % 2 == 0)
                        {
                            value = 2;
                        }
                        else
                        {
                            value = 4;
                        }
                    }
                    matrix[posY, posX] = value;
                    okay = true;
                }
                else
                {
                    posX = Random.Range(0, 4);
                    posY = Random.Range(0, 4);
                }
            }
        }
        
    }

    private void SearchEmptySpaces()
    {
        emptySpaces = 0;

        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(matrix[i, j] == 0)
                {
                    emptySpaces++;
                }
            }
        }
    }

    private void ExecuteJoinAnimation(int i, int j)
    {
        int childIndex = 0;
        Animation anim;

        switch (i)
        {
            case 0:
                childIndex = 0 + j;
                break;

            case 1:
                childIndex = 4 + j;
                break;

            case 2:
                childIndex = 8 + j;
                break;

            case 3:
                childIndex = 12 + j;
                break;
        }

        anim = this.gameObject.transform.GetChild(childIndex).transform.GetComponent<Animation>();
        anim.Play("JoinBlock");
    }

    private void MoveLeft()
    {
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(matrix[i, j] == 0 && j != 3)
                {
                    for (int k = j + 1; k < 4; k++)
                    {
                        if(matrix[i, k] != 0)
                        {
                            matrix[i, j] = matrix[i, k];
                            matrix[i, k] = 0;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void JoinBlocksLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(matrix[i, j] != 0 && j != 3)
                {
                    if (matrix[i, j] == matrix[i, j + 1])
                    {
                        matrix[i, j] *= 2;

                        ExecuteJoinAnimation(i, j);

                        matrix[i, j + 1] = 0;
                        break;
                    }
                }
            }
        }
    }

    private void MoveRight()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                if (matrix[i, j] == 0 && j != 0)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (matrix[i, k] != 0)
                        {
                            matrix[i, j] = matrix[i, k];
                            matrix[i, k] = 0;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void JoinBlocksRight()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                if (matrix[i, j] != 0 && j != 0)
                {
                    if (matrix[i, j] == matrix[i, j - 1])
                    {
                        matrix[i, j] *= 2;

                        ExecuteJoinAnimation(i, j);

                        matrix[i, j - 1] = 0;
                        break;
                    }
                }
            }
        }
    }

    private void MoveUp()
    {
        
    }

    private void JoinBlocksUp()
    {
        
    }
}
