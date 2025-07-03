using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Gingerbread_Dash
{
    class Highscore
    {
        string pathForFile;
        string readScore;
        int highScore;
        public Highscore()
        {
            pathForFile = @"..\..\..\..\..\Score Data.dat";

            if (!File.Exists(pathForFile))
            {
                WriteFile(pathForFile, "0");
            }

            highScore = 0;
            readScore = "" + 0;
        }

        public void WriteFile(string path, string text)
        {
            using (StreamWriter myFileOut = new StreamWriter(path, false))
            {
                myFileOut.WriteLine(text);
            }

        }

        public void SetHighScore(int score)
        {
            int currScore = 0;

            using (StreamReader streamReader = new StreamReader(pathForFile))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    currScore = Convert.ToInt32(line);
                }
            }

            if (score >= currScore)
            {
                highScore = score;
                WriteFile(pathForFile, "" + highScore);
            }
        }

        public string GetHighscore()
        {
            using (StreamReader streamReader = new StreamReader(pathForFile))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    readScore = line;
                }
            }
            return readScore;
        }

    }
}
