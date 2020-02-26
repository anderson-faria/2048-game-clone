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
    private int playsLeft = 0;
    private int emptySpaces = 16;
    private bool gameOver = false;
    private bool block2048 = false;

    public TextMeshProUGUI[] line0 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] line1 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] line2 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] line3 = new TextMeshProUGUI[4];

    public TextMeshProUGUI winText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverScreen;

    void Start()
    {
        GenerateBlock();
        GenerateBlock();
        ChangeMatrix();
    }

    void Update()
    {
        if(gameOver == true)
        {
            StopGame();
        }

        if (emptySpaces == 0)
        {
            if (playsLeft == 0) // O JOGO TERMINA QUANDO AINDA DÁ PARA MOVER BLOCOS
            {
                gameOver = true;
            }
        }
        else if(block2048 == true)
        {
            gameOver = true;
        }

        if (gameOver == false)
        {
            SearchEmptySpaces();

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
                JoinBlocksLeft();
                MoveLeft();
                GenerateBlock();
                ChangeMatrix();
                CanJoinBlocks();
                Debug.Log(playsLeft);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
                JoinBlocksRight();
                MoveRight();
                GenerateBlock();
                ChangeMatrix();
                CanJoinBlocks();
                Debug.Log(playsLeft);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                MoveUp();
                JoinBlocksUp();
                MoveUp();
                GenerateBlock();
                ChangeMatrix();
                CanJoinBlocks();
                Debug.Log(playsLeft);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                MoveDown();
                JoinBlocksDown();
                MoveDown();
                GenerateBlock();
                ChangeMatrix();
                CanJoinBlocks();
                Debug.Log(playsLeft);
            }
            
        }
    }

    private void ResetMatrix()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                matrix[i, j] = 0;
            }
        }
    }

    public void NewGame()
    {
        emptySpaces = 16;
        ResetMatrix();
        block2048 = false;
        gameOver = false;
        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        GenerateBlock();
        GenerateBlock();
        ChangeMatrix();
    }

    private void StopGame()
    {
        if (block2048 == true)
        {
            winText.gameObject.SetActive(true);
        }
        else
        {
            gameOverText.gameObject.SetActive(true);
        }

        gameOverScreen.gameObject.SetActive(true);
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

    private void CanJoinBlocks()
    {
        playsLeft = 0;

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if(j < 3)
                {
                    if(matrix[i, j + 1] == matrix[i, j])
                    {
                        playsLeft++;
                    }
                }

                if (j > 0)
                {
                    if (matrix[i, j - 1] == matrix[i, j])
                    {
                        playsLeft++;
                    }
                }

                if (i > 0)
                {
                    if (matrix[i - 1, j] == matrix[i, j])
                    {
                        playsLeft++;
                    }
                }

                if (i < 3)
                {
                    if (matrix[i + 1, j] == matrix[i, j])
                    {
                        playsLeft++;
                    }
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
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (matrix[j, i] == 0 && j != 3)
                {
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (matrix[k, i] != 0)
                        {
                            matrix[j, i] = matrix[k, i];
                            matrix[k, i] = 0;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void JoinBlocksUp()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (matrix[j, i] != 0 && j != 3)
                {
                    if (matrix[j, i] == matrix[j + 1, i])
                    {
                        matrix[j, i] *= 2;

                        ExecuteJoinAnimation(j, i);

                        matrix[j + 1, i] = 0;
                        break;
                    }
                }
            }
        }
    }

    private void MoveDown()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                if (matrix[j, i] == 0 && j != 0)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (matrix[k, i] != 0)
                        {
                            matrix[j, i] = matrix[k, i];
                            matrix[k, i] = 0;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void JoinBlocksDown()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                if (matrix[j, i] != 0 && j != 0)
                {
                    if (matrix[j, i] == matrix[j - 1, i])
                    {
                        matrix[j, i] *= 2;

                        ExecuteJoinAnimation(j, i);

                        matrix[j - 1, i] = 0;
                        break;
                    }
                }
            }
        }
    }
}
