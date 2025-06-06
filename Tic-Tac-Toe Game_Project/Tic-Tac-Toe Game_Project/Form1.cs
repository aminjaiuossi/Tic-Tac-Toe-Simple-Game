using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game_Project.Properties;

namespace Tic_Tac_Toe_Game_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enPlayer
        {
            Player1,
            Player2
        }

        enPlayer PlayTurn = enPlayer.Player1;

        enum enWinner
        {
            Player1,
            Player2,
            GameInProgress,
            Draw
        }

        struct stGameStatus
        {
            public short PlayRound;
            public enWinner Winner;
            public bool GameOver;
        }

        stGameStatus GameStatus;
        
        void EndGame()
        {
            lblTurnName.Text = "GameOver";
            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;

            }

            
        }

        public bool CheckValues(Button btn1 , Button btn2 , Button btn3)
        {
            if (btn1.Tag.ToString()!="?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                
            }
            GameStatus.GameOver = false;
            return false;
        }

        void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button7, button5, button3))
                return;
            
        }

        void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (PlayTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        lblTurnName.Text = "Player2";
                        btn.Tag = "X";
                        PlayTurn = enPlayer.Player2;
                        GameStatus.PlayRound++;
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        lblTurnName.Text = "Player1";
                        btn.Tag = "O";
                        PlayTurn = enPlayer.Player1;
                        GameStatus.PlayRound++;
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (GameStatus.PlayRound==9)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }
            
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);
            Pen WhitePen = new Pen(White);
            WhitePen.Width = 15;
            WhitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            WhitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(WhitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(WhitePen, 400, 460, 1050, 460);

            e.Graphics.DrawLine(WhitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(WhitePen, 840, 140, 840, 620);
        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }


        void ResetButton(Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
        }

        void RestartGame()
        {
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            lblTurnName.Text = "Player1";
            lblWinner.Text = "In Progress";
            PlayTurn = enPlayer.Player1;
            GameStatus.GameOver = false;
            GameStatus.PlayRound = 0;
            GameStatus.Winner = enWinner.GameInProgress;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
