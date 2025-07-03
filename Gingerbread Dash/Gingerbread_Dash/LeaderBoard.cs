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
    class LeaderBoard
    {
        int score;
        int[] leaderboard;
        string pathForFile;

        public LeaderBoard(int s)
        {
            pathForFile = @"..\..\..\..\..\LeaderBoard.dat";

            if (!File.Exists(pathForFile))
            {
                WriteFile(pathForFile, "" + 7);
            }

            score = s;
            leaderboard = new int[3];


            //Console.WriteLine(ReadFile(pathForFile));

        }

        public void WriteFile(string path, string text)
        {
            using (StreamWriter myFileOut = new StreamWriter(path, false))
            {
                myFileOut.WriteLine(text);
            }

        }

        public void EnterScore(string score)
        {
            WriteFile(pathForFile, score);
        }

        public string GetHighestScore()
        {
            string line = "";
            int currScore = 0;

            using (StreamReader streamReader = new StreamReader(pathForFile))
            {
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    if (Convert.ToInt32(line) > currScore)
                    {
                        currScore = Convert.ToInt32(line);
                    }
                    //Console.WriteLine("line:" + line);
                }
            }

            return Convert.ToString(currScore);

        }
    }
}
