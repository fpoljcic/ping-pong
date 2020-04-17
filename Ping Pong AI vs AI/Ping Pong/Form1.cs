using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;


namespace Ping_Pong
{
    public partial class Form1 : Form
    {
        int br = 0;
        int direction = 1;
        int sway = 5;
        int r = 0;
        int speed1 = 5;
        int speed2 = 5;
        double speedball = 5;
        double spb = 0;
        bool keyup = false;
        bool keydown = false;
        bool keyright = false;
        bool keyleft = false;
        bool keyw = false;
        bool keys = false;
        bool keya = false;
        bool keyd = false;
        bool p = false;
        int star = 0;
        string cheat="";
        //AI
        bool gotovo = false;
        int ball_left;
        int ball_top;
        int ball_bottom;
        int ball_right;
        int pom_r;
        int ball_location_x;
        int ball_location_y;
        Random s = new Random();
        SoundPlayer m = new SoundPlayer(Application.StartupPath + "\\TheFatRat - Time Lapse.wav");
        public Form1()
        {
            InitializeComponent();
            do
            {
                r = s.Next(sway * (-1), sway);
                if (r == 0)
                    r = s.Next(sway * (-1), sway);
            } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
            
            
            spb = speedball;
            player1.Left = ClientRectangle.Left + 10;
            player2.Left = ClientRectangle.Right - 20;
            player1.Top = (ClientSize.Height - player1.Height) / 2;
            player2.Top = (ClientSize.Height - player2.Height) / 2;
            ball.Location = new Point(25, ((ClientSize.Height - ball.Height) / 2));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if ((e.KeyCode == Keys.Up)&&(!pauseToolStripMenuItem.Checked))
                keyup = true;
            if ((e.KeyCode == Keys.Down)&& (!pauseToolStripMenuItem.Checked))
                keydown = true;
            if ((e.KeyCode == Keys.Left)&& (!pauseToolStripMenuItem.Checked))
                keyleft = true;
            if ((e.KeyCode == Keys.Right)&& (!pauseToolStripMenuItem.Checked))
                keyright = true;
            if ((e.KeyCode == Keys.W)&& (!pauseToolStripMenuItem.Checked))
                keyw = true;
            if ((e.KeyCode == Keys.S)&& (!pauseToolStripMenuItem.Checked))
                keys = true;
            if ((e.KeyCode == Keys.A)&& (!pauseToolStripMenuItem.Checked))
                keya = true;
            if ((e.KeyCode == Keys.D)&& (!pauseToolStripMenuItem.Checked))
                keyd = true;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((keyup)&&(player2.Top>ClientRectangle.Top))
            player2.Top = player2.Top - speed2;
            if ((keyup) && (!timerball.Enabled) && (ball.Top > ClientRectangle.Top+3)&&(!p) && (star == 1))
                ball.Top = ball.Top - speed2;
           
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if ((keydown) && (player2.Bottom < ClientRectangle.Bottom))
                player2.Top = player2.Top + speed2;
            if ((keydown) && (!timerball.Enabled)  && (ball.Bottom < ClientRectangle.Bottom-3)&&(!p) && (star == 1))
                ball.Top = ball.Top + speed2;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if ((keyw)&& (player1.Top > ClientRectangle.Top))
            player1.Top = player1.Top - speed1;
            if ((keyw) && (!timerball.Enabled)&&(ball.Top > ClientRectangle.Top+3)&&(!p) && (star == 0))
                ball.Top = ball.Top - speed1;



        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if ((keys)&& (player1.Bottom < ClientRectangle.Bottom))
            player1.Top = player1.Top + speed1;
            if ((keys) && (!timerball.Enabled) && (ball.Bottom< ClientRectangle.Bottom-3)&&(!p) && (star == 0))
                ball.Top = ball.Top + speed1;


        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                keyup = false;
            if (e.KeyCode == Keys.Down)
                keydown = false;
            if (e.KeyCode == Keys.Left)
                keyleft = false;
            if (e.KeyCode == Keys.Right)
                keyright = false;
            if (e.KeyCode == Keys.W)
                keyw = false;
            if (e.KeyCode == Keys.S)
                keys = false;
            if (e.KeyCode == Keys.A)
                keya = false;
            if (e.KeyCode == Keys.D)
                keyd = false;
        }

      

        private void timerball_Tick(object sender, EventArgs e)
        {
            speedlabel.Text = spb.ToString();
           
            if (direction == 1)
            {
                
                ball.Left = ball.Left + Convert.ToInt32 (spb);
                ball.Top = ball.Top + r;
                if (ball.Top <= ClientRectangle.Top)
              
                    r = r * (-1);
             
                if (ball.Bottom >= ClientRectangle.Bottom)
               
                    r = r * (-1);
               
                if ((ball.Top >= player2.Top) && (ball.Bottom <= player2.Bottom) && (ball.Location.X+Convert.ToInt32(spb)+10 > player2.Location.X)&&((ball.Location.X < player2.Location.X+10)))
                {
                    direction = -1;
                    if ((r < sway) && (r > sway*(-1)))
                        r = r + s.Next(-1, 1);
                    else if (r <= sway*(-1))
                        r = r + s.Next(0, 1);
                    else if (r >= sway)
                        r = r - s.Next(0, 1);
                    spb = spb + 0.25;
                }
                if (ball.Right >= ClientRectangle.Right)
                {
                    timerball.Stop();
                    Thread.Sleep(1000);
                    p = false;
                    gotovo = false;
                    spb = speedball;
                    score1.Text = (Convert.ToInt32(score1.Text) + 1).ToString();
                    
                    player1.Left = ClientRectangle.Left + 10;
                    player2.Left = ClientRectangle.Right - 20;
                    player1.Top = (ClientSize.Height - player1.Height) / 2;
                    player2.Top = (ClientSize.Height - player2.Height) / 2;
                    ball.Location = new Point(25, ((ClientSize.Height - ball.Height) / 2));
                    star = 0;
                    do
                    {
                        r = s.Next(sway * (-1), sway);
                        if (r == 0)
                            r = s.Next(sway * (-1), sway);
                    } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
                }
                

            }
            
                if (direction == -1)
            {
                 ball.Left = ball.Left - Convert.ToInt32 (spb);
                ball.Top = ball.Top + r;
                if (ball.Top <= ClientRectangle.Top)
              
                    r = r * (-1);
          
                if (ball.Bottom >= ClientRectangle.Bottom)
             
                    r = r * (-1);
         

                if ((ball.Top >= player1.Top) && (ball.Bottom <= player1.Bottom) && (ball.Location.X - Convert.ToInt32(spb)-10 < player1.Location.X) && ((ball.Location.X > player1.Location.X - 10)))
                {
                    direction = 1;
                    if ((r < sway) && (r > sway * (-1)))
                        r = r + s.Next(-1, 1);
                    else if (r <= sway * (-1))
                        r = r + s.Next(0, 1);
                    else if (r >= sway)
                        r = r - s.Next(0, 1);
                    spb = spb + 0.25;

                }
                if (ball.Left <= ClientRectangle.Left)
                {
                    timerball.Stop();
                    Thread.Sleep(1000);
                    p = false;
                    gotovo = false;
                    spb = speedball;
                    score2.Text = (Convert.ToInt32(score2.Text) + 1).ToString();
                    
                    player1.Left = ClientRectangle.Left + 10;
                    player2.Left = ClientRectangle.Right - 20;
                    player1.Top = (ClientSize.Height - player1.Height) / 2;
                    player2.Top = (ClientSize.Height - player2.Height) / 2;
                    ball.Location = new Point(ClientRectangle.Width - 40, ((ClientSize.Height - ball.Height) / 2));
                    star = 1;
                    do
                    {
                        r = s.Next(sway * (-1), sway);
                        if (r == 0)
                            r = s.Next(sway * (-1), sway);
                    } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
                }
             
            }
        
            

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                timerball.Start();
                pauseToolStripMenuItem.Checked = false;
            }
            if (e.KeyChar.ToString().ToUpper() == Keys.P.ToString())
            {
                if (timerball.Enabled)
                {
                    pauseToolStripMenuItem.Checked = true;
                    timerball.Stop();
                }
                else
                {
                    pauseToolStripMenuItem.Checked = false;
                    timerball.Start();
                }
            }
            if (e.KeyChar.ToString().ToUpper() == Keys.M.ToString())
            {
                
                if (pictureBox1.ImageLocation == Application.StartupPath + "\\soundon.png")
                {
                    pictureBox1.ImageLocation = Application.StartupPath + "\\soundoff.png";
                    m.Stop();
                    musicToolStripMenuItem.Checked = false;
                }
                else
                {
                    pictureBox1.ImageLocation = Application.StartupPath + "\\soundon.png";
                    m.PlayLooping();
                    musicToolStripMenuItem.Checked = true;
                }

                    music.Start();
              
            }
            if (e.KeyChar.ToString().ToUpper() == Keys.R.ToString())
            {
                score1.Text = "0";
                score2.Text = "0";
            }
            if ((e.KeyChar.ToString().ToUpper() == "L") || (e.KeyChar.ToString().ToUpper() == "1") || (e.KeyChar.ToString().ToUpper() == "2"))
                cheat = cheat + e.KeyChar.ToString().ToString().ToUpper();
            else
                cheat = "";

            if (cheat == "LLL1")
               score1.Text = (Convert.ToInt32(score1.Text) + 2).ToString();
            if (cheat == "LLL2")
                score2.Text = (Convert.ToInt32(score2.Text) + 2).ToString();
            if ((cheat == "1") || (cheat == "2") || (cheat == "L1") || (cheat == "L2") || (cheat == "1L") || (cheat == "2L") || (cheat == "LL1") || (cheat == "LL2") || (cheat == "1LL") || (cheat == "2LL") || (cheat == "L1L") || (cheat == "L2L") || (cheat == "1LLL") || (cheat == "2LLL") || (cheat == "L1LL") || (cheat == "L2LL") || (cheat == "LL1L") || (cheat == "LL2L") || (cheat == "LLLL"))
                cheat = "";
            if (cheat.Length>=4)
                cheat = "";
           
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            net.Left = (ClientSize.Width - net.Width) / 2;
            score1.Left = ((ClientSize.Width - score1.Width) / 2)-20;
            score2.Left = ((ClientSize.Width - score2.Width) / 2)+20;
            player1.Left = ClientRectangle.Left + 10;
            player2.Left = ClientRectangle.Right - 20;
            pictureBox1.Left = ((ClientSize.Width - pictureBox1.Width) / 2) + 70;


        }

        private void music_Tick(object sender, EventArgs e)
        {
            pictureBox1.Show();
            br++;
            if (br >= 100)
            {
                music.Stop();
                pictureBox1.Hide();
                br = 0;
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ball.ImageLocation = Application.StartupPath + "\\ball.png";
        
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ball.ImageLocation = Application.StartupPath + "\\ballblue.png";
          
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ball.ImageLocation = Application.StartupPath + "\\ballgreen.png";
           
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ball.ImageLocation = Application.StartupPath + "\\ballred.png";
           
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            ball.ImageLocation = Application.StartupPath + "\\ballyellow.png";
      
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            player2.ImageLocation = Application.StartupPath + "\\player.png";
            score2.ForeColor = Color.Black;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            player2.ImageLocation = Application.StartupPath + "\\playerblue.png";
            score2.ForeColor = Color.Blue;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            player2.ImageLocation = Application.StartupPath + "\\playergreen.png";
            score2.ForeColor = Color.Green;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            player2.ImageLocation = Application.StartupPath + "\\playerred.png";
            score2.ForeColor = Color.Red;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            player2.ImageLocation = Application.StartupPath + "\\playeryellow.png";
            score2.ForeColor = Color.Yellow;
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player1.ImageLocation = Application.StartupPath + "\\player.png";
            score1.ForeColor = Color.Black;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player1.ImageLocation = Application.StartupPath + "\\playerblue.png";
            score1.ForeColor = Color.Blue;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player1.ImageLocation = Application.StartupPath + "\\playergreen.png";
            score1.ForeColor = Color.Green;
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player1.ImageLocation = Application.StartupPath + "\\playerred.png";
            score1.ForeColor = Color.Red;
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player1.ImageLocation = Application.StartupPath + "\\playeryellow.png";
            score1.ForeColor = Color.Yellow;
        }

        private void blackToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            score1.ForeColor = Color.Black;
            
        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            score1.ForeColor = Color.Blue;
        
        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            score1.ForeColor = Color.Green;
           
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            score1.ForeColor = Color.Red;
          
        }

        private void yellowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            score1.ForeColor = Color.Yellow;
          
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            score2.ForeColor = Color.Black;
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            score2.ForeColor = Color.Blue;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            score2.ForeColor = Color.Green;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            score2.ForeColor = Color.Red;
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            score2.ForeColor = Color.Yellow;
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            net.ImageLocation = Application.StartupPath + "\\strip.png";
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            net.ImageLocation = Application.StartupPath + "\\stripblue.png";
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            net.ImageLocation = Application.StartupPath + "\\stripgreen.png";
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            net.ImageLocation = Application.StartupPath + "\\stripred.png";
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            net.ImageLocation = Application.StartupPath + "\\stripyellow.png";
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.Black;
                
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.Blue;
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.Green;

        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.Red;
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.Yellow;
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.BackColor = Color.White;
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (pictureBox1.ImageLocation == Application.StartupPath + "\\soundon.png")
            {
                musicToolStripMenuItem.Checked = false;
                pictureBox1.ImageLocation = Application.StartupPath + "\\soundoff.png";
                m.Stop();

            }
            else
            {
                musicToolStripMenuItem.Checked = true;
                pictureBox1.ImageLocation = Application.StartupPath + "\\soundon.png";
                m.PlayLooping();

            }

            music.Start();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            score1.Text = "0";
            score2.Text = "0";
            timerball.Stop();
            p = false;
            spb = speedball;
            player1.Left = ClientRectangle.Left + 10;
            player2.Left = ClientRectangle.Right - 20;
            player1.Top = (ClientSize.Height - player1.Height) / 2;
            player2.Top = (ClientSize.Height - player2.Height) / 2;
            ball.Location = new Point(25, ((ClientSize.Height - ball.Height) / 2));
            star = 0;
            do
            {
                r = s.Next(sway * (-1), sway);
                if (r == 0)
                    r = s.Next(sway * (-1), sway);
            } while ((r < sway - 2) && (r > (sway * (-1) + 2)));

        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerball.Enabled)
            {
                p = true;
                pauseToolStripMenuItem.Checked = true;
                timerball.Stop();
            }
            else
            {
                p = false;
                pauseToolStripMenuItem.Checked = false;
                timerball.Start();
            }
            
        }

     

       

        private void lowSwayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sway = 3;
            do
            {
                r = s.Next(sway * (-1), sway);
                if (r == 0)
                    r = s.Next(sway * (-1), sway);
            } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
            lowSwayToolStripMenuItem.Checked = true;
            mediumSwayToolStripMenuItem.Checked = false;
            highSwayToolStripMenuItem.Checked = false;
            crazySwayToolStripMenuItem.Checked = false;
        }

        private void mediumSwayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sway = 5;
            do
            {
                r = s.Next(sway * (-1), sway);
                if (r == 0)
                    r = s.Next(sway * (-1), sway);
            } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
            lowSwayToolStripMenuItem.Checked = false;
            mediumSwayToolStripMenuItem.Checked = true;
            highSwayToolStripMenuItem.Checked = false;
            crazySwayToolStripMenuItem.Checked = false;
        }

        private void highSwayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sway = 7;
            do
            {
                r = s.Next(sway * (-1), sway);
                if (r == 0)
                    r = s.Next(sway * (-1), sway);
            } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
            lowSwayToolStripMenuItem.Checked = false;
            mediumSwayToolStripMenuItem.Checked = false;
            highSwayToolStripMenuItem.Checked = true;
            crazySwayToolStripMenuItem.Checked = false;
        }

        private void crazySwayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sway = 10;
            do
            {
                r = s.Next(sway * (-1), sway);
                if (r == 0)
                    r = s.Next(sway * (-1), sway);
            } while ((r < sway - 2) && (r > (sway * (-1) + 2)));
            lowSwayToolStripMenuItem.Checked = false;
            mediumSwayToolStripMenuItem.Checked = false;
            highSwayToolStripMenuItem.Checked = false;
            crazySwayToolStripMenuItem.Checked = true;
            
        }

        private void lowSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speedball = 3;
            spb = speedball;
            lowSpeedToolStripMenuItem.Checked = true;
            mediumSpeedToolStripMenuItem.Checked = false;
            highSpeedToolStripMenuItem.Checked = false;
            crazySpeedToolStripMenuItem.Checked = false;
        }

        private void mediumSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speedball = 5;
            spb = speedball;
            lowSpeedToolStripMenuItem.Checked = false;
            mediumSpeedToolStripMenuItem.Checked = true;
            highSpeedToolStripMenuItem.Checked = false;
            crazySpeedToolStripMenuItem.Checked = false;
        }

        private void highSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speedball = 7;
            spb = speedball;
            lowSpeedToolStripMenuItem.Checked = false;
            mediumSpeedToolStripMenuItem.Checked = false;
            highSpeedToolStripMenuItem.Checked = true;
            crazySpeedToolStripMenuItem.Checked = false;
        }

        private void crazySpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speedball = 10;
            spb = speedball;
            lowSpeedToolStripMenuItem.Checked = false;
            mediumSpeedToolStripMenuItem.Checked = false;
            highSpeedToolStripMenuItem.Checked = false;
            crazySpeedToolStripMenuItem.Checked = true;
        }

        private void smallPadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size x = new Size(player1.Width,45);
            player1.Size = x;
            player2.Size = x;
            smallPadToolStripMenuItem.Checked = true;
            mediumPadToolStripMenuItem.Checked = false;
            largePadToolStripMenuItem.Checked = false;
            if ((!timerball.Enabled) && (!pauseToolStripMenuItem.Checked))
                ball.Location = new Point(25, (player1.Location.Y + ((player1.Height - ball.Height) / 2)));

        }

        private void mediumPadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size x = new Size(player1.Width, 90);
            player1.Size = x;
            player2.Size = x;
            smallPadToolStripMenuItem.Checked = false;
            mediumPadToolStripMenuItem.Checked = true;
            largePadToolStripMenuItem.Checked = false;
            if ((!timerball.Enabled) && (!pauseToolStripMenuItem.Checked))
                ball.Location = new Point(25, (player1.Location.Y + ((player1.Height - ball.Height) / 2)));
        }

        private void largePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size x = new Size(player1.Width, 135);
            player1.Size = x;
            player2.Size = x;
            smallPadToolStripMenuItem.Checked = false;
            mediumPadToolStripMenuItem.Checked = false;
            largePadToolStripMenuItem.Checked = true;
            if ((!timerball.Enabled) && (!pauseToolStripMenuItem.Checked))
                ball.Location = new Point(25, (player1.Location.Y + ((player1.Height - ball.Height)/2)));
        }

        private void lowSpeedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            speed1 = 3;
            speed2 = 3;
            lowSpeedToolStripMenuItem1.Checked = true;
            mediumSpeedToolStripMenuItem1.Checked = false;
            highSpeedToolStripMenuItem1.Checked = false;
            crazyPadSpeedToolStripMenuItem.Checked = false;
        }

        private void mediumSpeedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            speed1 = 5;
            speed2 = 5;
            lowSpeedToolStripMenuItem1.Checked = false;
            mediumSpeedToolStripMenuItem1.Checked = true;
            highSpeedToolStripMenuItem1.Checked = false;
            crazyPadSpeedToolStripMenuItem.Checked = false;
        }

        private void highSpeedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            speed1 = 8;
            speed2 = 8;
            lowSpeedToolStripMenuItem1.Checked = false;
            mediumSpeedToolStripMenuItem1.Checked = false;
            highSpeedToolStripMenuItem1.Checked = true;
            crazyPadSpeedToolStripMenuItem.Checked = false;
        }

        private void crazyPadSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed1 = 11;
            speed2 = 11;
            lowSpeedToolStripMenuItem1.Checked = false;
            mediumSpeedToolStripMenuItem1.Checked = false;
            highSpeedToolStripMenuItem1.Checked = false;
            crazyPadSpeedToolStripMenuItem.Checked = true;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            
            if (!timerball.Enabled)
            {
                
                if ((keyleft) && (player2.Left - 140 > (ClientRectangle.Width) / 2))
                    player2.Left = player2.Left - speed2;
                if ((keyleft) && (!timerball.Enabled) && (ball.Location.X - 120 > ((ClientRectangle.Width) / 2)) && (!p) && (star == 1))
                    ball.Left = ball.Left - speed2;
            }
            else 
            {
                if ((keyleft) && (player2.Left - 40 > (ClientRectangle.Width) / 2))
                    player2.Left = player2.Left - speed2;
                if ((keyleft) && (!timerball.Enabled) && (ball.Location.X - 20 > ((ClientRectangle.Width) / 2)) && (!p) && (star == 1))
                    ball.Left = ball.Left - speed2;
            }
            
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            if ((keyright) && (player2.Left < (ClientRectangle.Width -25)))
                player2.Left = player2.Left + speed2;
            if ((keyright) && (!timerball.Enabled) && (ball.Right+30 < ClientRectangle.Right) && (!p) && (star == 1))
                ball.Left = ball.Left + speed2;
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            if ((keya) && (player1.Left-15 > ClientRectangle.Left))
                player1.Left = player1.Left - speed1;
            if ((keya) && (!timerball.Enabled) && (ball.Location.X - 30 > ClientRectangle.Left) && (!p) && (star == 0))
                ball.Left = ball.Left - speed1;
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            if (!timerball.Enabled)
            {
                
                if ((keyd) && (player1.Left + 150 < (ClientRectangle.Width / 2)))
                    player1.Left = player1.Left + speed1;
                if ((keyd) && (!timerball.Enabled) && (ball.Location.X + 135 < (ClientRectangle.Width / 2)) && (!p) && (star == 0))
                    ball.Left = ball.Left + speed1;
            }
            else
            {
                if ((keyd) && (player1.Left + 50 < (ClientRectangle.Width / 2)))
                    player1.Left = player1.Left + speed1;
                if ((keyd) && (!timerball.Enabled) && (ball.Location.X + 35 < (ClientRectangle.Width / 2)) && (!p) && (star == 0))
                    ball.Left = ball.Left + speed1;
            }
        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            //AI 3.0 - player3
            //gotovo - jel prvi put da provjerava?
            if (timerball.Enabled)
            {
                if (direction == 1)
                {
                    if (!gotovo)
                    {
                        ball_location_x = ball.Location.X;
                        ball_location_y = ball.Location.Y;
                        ball_left = ball.Left;
                        ball_top = ball.Top;
                        ball_bottom = ball.Bottom;
                        ball_right = ball.Right;
                        pom_r = r;
                        do
                        {
                            ball_left = ball_left + Convert.ToInt32(spb);
                            ball_right = ball_right + Convert.ToInt32(spb);
                            ball_location_x += Convert.ToInt32(spb);
                            ball_top = ball_top + pom_r;
                            ball_bottom = ball_bottom + pom_r;
                            ball_location_y += pom_r;
                            if (ball_top <= ClientRectangle.Top)
                                pom_r = pom_r * (-1);
                            if (ball_bottom >= ClientRectangle.Bottom)
                                pom_r = pom_r * (-1);
                        } while (ball_right + 25 < ClientRectangle.Right);
                        gotovo = true;
                    }
                    if (ball_location_y - player2.Location.Y != (player2.Height - ball.Height) / 2 && ball_location_y < player2.Location.Y + (player2.Height - ball.Height) / 2 && player2.Top > ClientRectangle.Top)
                        player2.Top -= speed2;
                    if (ball_location_y - player2.Location.Y != (player2.Height - ball.Height) / 2 && ball_location_y > player2.Location.Y + (player2.Height - ball.Height) / 2 && player2.Bottom < ClientRectangle.Bottom)
                        player2.Top += speed2;
                }
                if (direction == -1)
                {
                    gotovo = false;
                    if (player2.Location.Y > (ActiveForm.Height / 2) - player2.Height)
                        player2.Top -= speed2;
                    if (player2.Location.Y < (ActiveForm.Height / 2) - player2.Height)
                        player2.Top += speed2;
                }
            }
            //AI 1.0 - player1
            if (timerball.Enabled) {
                if (ball.Location.Y - player1.Location.Y != (player1.Height - ball.Height)/2 && ball.Location.Y < player1.Location.Y + (player1.Height - ball.Height) / 2 && player1.Top > ClientRectangle.Top)
                    player1.Top -= speed1;
                if (ball.Location.Y - player1.Location.Y != (player1.Height - ball.Height) / 2 && ball.Location.Y > player1.Location.Y + (player1.Height - ball.Height) / 2 && player1.Bottom < ClientRectangle.Bottom)
                    player1.Top += speed1;
                // AI 2.0 - player1
                double startpos = Math.Pow(player1.Location.X - ball.Location.X, 2) - Math.Pow(player1.Location.Y - ball.Location.Y, 2);
                if (direction == 1 || ball.Location.X - 10 < player1.Location.X) {
                    if (player1.Left - 15 > ClientRectangle.Left)
                        player1.Left = player1.Left - speed1;
                } else if (startpos > 50) {
                    if (player1.Left + 50 < (ClientRectangle.Width / 2))
                        player1.Left += speed1;
                }
                
                //AI 1.0 - player2
                /*
                if (ball.Location.Y - player2.Location.Y != (player2.Height - ball.Height) / 2 && ball.Location.Y < player2.Location.Y + (player2.Height - ball.Height) / 2 && player2.Top > ClientRectangle.Top)
                    player2.Top -= speed2;
                if (ball.Location.Y - player2.Location.Y != (player2.Height - ball.Height) / 2 && ball.Location.Y > player2.Location.Y + (player2.Height - ball.Height) / 2 && player2.Bottom < ClientRectangle.Bottom)
                    player2.Top += speed2;
                // AI 2.0 - player2
                double startpos2 = Math.Pow(player2.Location.X - ball.Location.X, 2) - Math.Pow(player2.Location.Y - ball.Location.Y, 2);
                if (direction == -1 || ball.Location.X + 10 > player2.Location.X)
                {
                    if (player2.Left < (ClientRectangle.Width - 25))
                        player2.Left = player2.Left + speed1;
                }
                else if (startpos2 > 50)
                {
                    if (player2.Left - 40 > (ClientRectangle.Width) / 2)
                        player2.Left -= speed1;
                }
                */
            }
        }
    }
}
