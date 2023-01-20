using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZombieShooterGame
{
    public partial class Form1 : Form
    {
        bool moveLeft, moveRight, moveUp, moveDown;
        bool gameOver = false;
        string facing = "up";
        double characterHealth = 100;
        int speed = 15;
        int ammo = 10;
        int enemySpeed = 4;
        Random randNum = new Random();
        int kills = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
                facing = "left";
                character.Image = Properties.Resource1.player_Left;
            }

            if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
                facing = "right";
                character.Image = Properties.Resource1.player_Right;
            }

            if (e.KeyCode==Keys.Up)
            {
                moveUp = true;
                facing = "up";
                character.Image = Properties.Resource1.player_Back;
            }

            if (e.KeyCode == Keys.Down)
            {
                moveDown = true;
                facing = "down";
                character.Image = Properties.Resource1.player_Front;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                moveUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                moveDown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                fire(facing);

                if (ammo < 1)
                {
                    spawnAmmo();
                }
            }
        }

        private void gameEngine(object sender, EventArgs e)
        {
            if (characterHealth > 1)
            {
                progressBar1.Value = Convert.ToInt32(characterHealth);
            }
            else
            {
                character.Image = Properties.Resource1.Tombstone;
                timer1.Stop();
                gameOver = true;
            }

            label1.Text = "Ammo: " + ammo;
            label2.Text = "Kills: " + kills;

            if (characterHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red;
            }

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

            if (moveDown && character.Top + character.Height > 1420)
            {
                character.Top += speed;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "ammo")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(character.Bounds))
                    {
                        this.Controls.Remove(((PictureBox)x));

                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                if (x is PictureBox && x.Tag == "bullet")
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 2010 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 1420)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }

                if (x is PictureBox && x.Tag == "enemy")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(character.Bounds))
                    {
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
                        ((PictureBox)x).Left += enemySpeed);
                    }

                    if (((PictureBox)x).Top < character.Top)
                    {
                        ((PictureBox)x).Top += enemySpeed;
                    }
                }


                foreach(Control j in this.Controls)
                {

                    if ((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "enemy"))
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            kills++;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                        }
                    }
                }
            }
        }

        private void spawnAmmo()
        {

            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resource1.Ammo;
            ammo.Left = randNum.Next(10, 2010);
            ammo.Top = randNum.Next(50, 1420);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            character.BringToFront();
        }

        private void fire(string direct)
        {

        }

        private void createEnemies()
        {

        }
    }
}