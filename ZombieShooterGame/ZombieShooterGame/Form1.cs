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

        }

        private void gameEngine(object sender, EventArgs e)
        {

        }

        private void spawnAmmo()
        {

        }

        private void fire(string direct)
        {

        }

        private void createEnemies()
        {

        }
    }
}