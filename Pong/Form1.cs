using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        //Global variables
        Rectangle player1 = new Rectangle(10, 10, 10, 60);
        Rectangle player2 = new Rectangle(10, 325, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);
        Rectangle wall = new Rectangle(580, 0, 10, 400);

        int playerturn = 1;


        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 5;
        int ballXSpeed = 3;
        int ballYSpeed = 3;


        bool wPressed = false;
        bool sPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;  
            }

        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            resultsLabel.Text = "";
            instructionsLabel.Text = "";
            
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }
            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;
            }
            if(player1.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                ballXSpeed += 1;
                if (playerturn == 1)
                {
                    playerturn = 2;
                }

                else if (playerturn == 2)
                {
                    player2Score++;
                    ballXSpeed = 3;
                    ballYSpeed = 3;
                    ball.X = 295;
                    ball.Y = 195;
                    player1.Y = 10;
                    player1.X = 10;
                    player2.Y = 325;
                    player2.X = 10;
                }
            }
            else if (wall.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
            }
            else if (player2.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                if (playerturn == 2)
                {
                    playerturn = 1;
                }

                else if (playerturn == 1)
                {
                    player1Score++;

                    ballXSpeed = 3;
                    ballYSpeed = 3;
                    ball.X = 295;
                    ball.Y = 195;
                    player1.Y = 10;
                    player1.X = 10;
                    player2.Y = 325;
                    player2.X = 10;
                }
            }

            //check if a player missed the ball and if true add 1 to score of other player 
            if (ball.X < 0)
            {
                if (playerturn == 1)
                {
                    player2Score++;
                }

                if (playerturn == 2)
                {
                    player1Score++;
                }
                ballXSpeed = 3;
                ballYSpeed = 3;
                ball.X = 295;
                ball.Y = 195;
                player1.Y = 10;
                player1.X = 10;
                player2.Y = 325;
                player2.X = 10;
            }
            if (ballXSpeed == 11)
            {
                ballXSpeed -= 1;
            }

            if (ballYSpeed == 10)
            {
                ballYSpeed -= 1;
            }

            if (playerturn == 0)
            {
                playerturn = 1;
            }

            if (playerturn == 3)
            {
                playerturn = 1;
                
                if (playerturn == 1)
                {
                    player2Score++;
                }

                if (playerturn == 2)
                {
                    player1Score++;

                }

                ballXSpeed = 3;
                ballYSpeed = 3;
                ball.X = 295;
                ball.Y = 195;
                player1.Y = 10;
                player1.X = 10;
                player2.Y = 325;
                player2.X = 10;

            }
            if (player1Score == 3)
            {
                gameTimer.Stop();
                resultsLabel.Text = "Player 1 Wins!";
                instructionsLabel.Text = "Click the screen to play again.";
            }

            if (player2Score == 3)
            {
                gameTimer.Stop();
                resultsLabel.Text = "Player 2 Wins!";
                instructionsLabel.Text = "Click the screen to play again.";
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            p1ScoreLabel.Text = $"P1 Score: {player1Score}";
            p2ScoreLabel.Text = $"P2 Score: {player2Score}";
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillEllipse(whiteBrush, ball);
            e.Graphics.FillRectangle(orangeBrush, wall);

            if (playerturn == 1)
            {
                e.Graphics.FillRectangle(yellowBrush, player1);
            }

            if (playerturn == 2)
            {
                e.Graphics.FillRectangle(yellowBrush, player2);
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            gameTimer.Start();

            ballXSpeed = 3;
            ballYSpeed = 3;
            player1Score = 0;
            player2Score = 0;
            playerturn = 1;
            ball.X = 295;
            ball.Y = 195;
            player1.Y = 10;
            player1.X = 10;
            player2.Y = 325;
            player2.X = 10;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            gameTimer.Stop();
            resultsLabel.Text = "Wall Ball";
            instructionsLabel.Text = "Click the screen to play!";
        }
    }
}
