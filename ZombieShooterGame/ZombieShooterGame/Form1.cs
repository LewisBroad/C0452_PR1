using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieShooterGame
{
    public partial class Form1 : Form
    {
        bool moveLeft, moveRight, moveUp, moveDown, gameOver;
        string facing = "up";
        int characterHealth = 100;
        int speed = 15;
        int ammo = 10;
        int enemySpeed = 5;
        Random randNum = new Random();
        int kills;
        List<PictureBox> enemyList = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver == true)
            {
                return;
            }

            if (e.KeyCode == Keys.A)
            {
                moveLeft = true;
                facing = "left";
                character.Image = Properties.Resource1.player_Left;
            }

            if (e.KeyCode == Keys.D)
            {
                moveRight = true;
                facing = "right";
                character.Image = Properties.Resource1.player_Right;
            }

            if (e.KeyCode == Keys.W)
            {
                moveUp = true;
                facing = "up";
                character.Image = Properties.Resource1.player_Back;
            }

            if (e.KeyCode == Keys.S)
            {
                moveDown = true;
                facing = "down";
                character.Image = Properties.Resource1.player_Front;
            }

            if(e.KeyCode == Keys.Escape)
            {
                timer1.Stop();
            }

            if (e.KeyCode == Keys.Tab)
            {
                timer1.Start();
            }

        }
 
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                moveLeft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                moveRight = false;
            }

            if (e.KeyCode == Keys.W)
            {
                moveUp = false;
            }

            if (e.KeyCode == Keys.S)
            {
                moveDown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                fireBullet(facing);

                if (ammo < 1)
                {
                    spawnAmmo();
                }
            }

            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }


        }



        private void gameEngine(object sender, EventArgs e)
        {
            if (characterHealth > 1)
            {
                progressBar1.Value = characterHealth;
            }
            else
            {
                character.Image = Properties.Resource1.Tombstone;
                character.BringToFront();

                timer1.Stop();
                gameOver = true;
            }

            label1.Text = "Ammo: " + ammo;
            label2.Text = "Kills: " + kills;

            if (moveLeft && character.Left > 0)
            {
                character.Left -= speed;
            }

            if (moveRight && character.Right + character.Width < 2010)
            {
                character.Left += speed;
            }

            if (moveUp && character.Top > 60)
            {
                character.Top -= speed;
            }

            if (moveDown && character.Top + character.Height < 1420)
            {
                character.Top += speed;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(character.Bounds))
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "enemy")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(character.Bounds) && characterHealth > 1)
                    {
                        ((PictureBox)x).BringToFront();
                        characterHealth -= 1;
                    }

                    if (((PictureBox)x).Left > character.Left)
                    {
                        ((PictureBox)x).Left -= enemySpeed;
                    }
                    
                    if (((PictureBox)x).Top > character.Top)
                    {
                        ((PictureBox)x).Top -= enemySpeed;
                    }

                    if (((PictureBox)x).Left < character.Left)
                    {
                        ((PictureBox)x).Left += enemySpeed;
                    }

                    if (((PictureBox)x).Top < character.Top)
                    {
                        ((PictureBox)x).Top += enemySpeed;
                    }
                }


                foreach(Control j in this.Controls)
                {

                    if ((j is PictureBox && (string)j.Tag == "bullet") && x is PictureBox && (string)x.Tag == "enemy")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            kills++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            enemyList.Remove(((PictureBox)x));
                            createEnemies();
                        }
                    }
                }
            }
        }

        private void spawnAmmo()
        {

            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resource1.Ammo;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(60, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            character.BringToFront();
        }

        private void fireBullet(string direction)
        {
            Bullet fire = new Bullet();

            fire.direction = direction;
            fire.bulletLeft = character.Left + (character.Width / 2);
            fire.bulletTop = character.Top + (character.Height / 2);
            fire.gunShot(this);
        }

        private void createEnemies()
        {
            PictureBox enemy = new PictureBox();

            enemy.Tag = "enemy";
            enemy.Image = Properties.Resource1.Enemy;
            enemy.Left = randNum.Next(10, this.ClientSize.Width - enemy.Width);
            enemy.Top = randNum.Next(0, this.ClientSize.Height - enemy.Height);
            enemy.SizeMode = PictureBoxSizeMode.AutoSize;
            enemyList.Add(enemy);
            this.Controls.Add(enemy);
            //character.BringToFront();
        }

        private void RestartGame()
        {
            character.Image = Properties.Resource1.player_Front;

            foreach (PictureBox i in enemyList)
            {
                this.Controls.Remove(i);
            }

            enemyList.Clear();

            for (int i = 0; i < 3; i++)
            {
                createEnemies();
            }

            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            gameOver = false;

            characterHealth = 100;
            kills = 0;
            ammo = 10;

            timer1.Start();
        }
    }
}