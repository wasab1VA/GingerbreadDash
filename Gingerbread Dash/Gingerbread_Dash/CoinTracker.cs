using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gingerbread_Dash
{
    class CoinTracker
    {
        string pathForFile;
        string readScore;


        public bool isCollected = false;
        int score;

        public CoinTracker()
        {
            pathForFile = @"..\..\..\..\..\Coin Data.dat";
            score = 1000;

            if (!File.Exists(pathForFile))
            {
                WriteFile("" + score);
            }

            readScore = "";
        }

        public void WriteFile(string text)
        {

            using (StreamWriter myFileOut = new StreamWriter(pathForFile, true))
            {
                myFileOut.WriteLine(text);
            }

        }

        public string GetCoinscore()
        {
            int baseScore = 1000;
            int total = baseScore;
            using (StreamReader streamReader = new StreamReader(pathForFile))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (int.TryParse(line, out int value))
                        total += value;
                }
            }
            readScore = "" + total;
            return readScore;
        }

        public void AddCoins(int c)
        {
            score += c;
            WriteFile("" + score);
        }
    }
}
