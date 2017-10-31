using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] allScores = new int[11][];
            //obtain the frame and roll scores from the user.
            for (int frame = 1; frame <= 10; frame++)
            {
                Console.Clear();
                allScores[frame] = new int[3];
                Console.WriteLine("What is your first roll for frame {0}", frame);
                int rollOne = Convert.ToInt32(Console.ReadLine());
                allScores[frame][0] = rollOne;
                if (frame == 10 || rollOne != 10)
                {
                    Console.WriteLine("What is your second roll for frame {0}", frame);
                    int rollTwo = Convert.ToInt32(Console.ReadLine());
                    allScores[frame][1] = rollTwo;
                }
                if (frame == 10)
                {
                    //check if first roll is a strike
                    if (rollOne == 10 || rollOne + allScores[10][1] == 10)
                    {
                        Console.WriteLine("What is your third roll for frame 10");
                        int rollThree = Convert.ToInt32(Console.ReadLine());
                        allScores[frame][2] = rollThree;
                    }
                    Console.ReadKey();
                }
                Console.Write("Total Game Score: ");
                Console.WriteLine(CalculateScore(allScores));
                Console.WriteLine("Hit Any Key");
                Console.ReadKey();
            }
        }
        public static int CalculateScore(int[][] allScore)
        {
            int totalGameScore = 0;
            for (int frame = 1; frame <= 8; frame++)
            {
                int frameScore = 0;
                //condition for null reference exception
                if (allScore[frame] != null)
                {
                    //Not a strike and not a spare.
                    if (RequestValue(allScore, frame, 0) + RequestValue(allScore, frame, 1) != 10 && frame != 10)
                    {
                        frameScore = RequestValue(allScore, frame, 0) + RequestValue(allScore, frame, 1);

                    }
                    //Strike only
                    if (RequestValue(allScore, frame, 0) == 10 && frame != 10)
                    {
                        frameScore = RequestValue(allScore, frame, 0) + RequestValue(allScore, frame, 1) + RequestValue(allScore, frame + 1, 0);
                        //Two Strikes
                        if (RequestValue(allScore, frame + 1, 0) == 10)
                        {
                            frameScore = RequestValue(allScore, frame, 0) + RequestValue(allScore, frame + 1, 0) + RequestValue(allScore, frame + 2, 0);
                        }
                    }
                    //Spare only
                    if (RequestValue(allScore, frame, 0) != 10 && RequestValue(allScore, frame, 0) + RequestValue(allScore, frame, 1) == 10 && frame != 10)
                    {
                        frameScore = RequestValue(allScore, frame, 0) + RequestValue(allScore, frame, 1) + RequestValue(allScore, frame + 1, 0);
                    }
                    totalGameScore = totalGameScore + frameScore;
                }
            }
            totalGameScore += FrameNineBonus(allScore, 10);
            totalGameScore += FrameTen(allScore, 10);
            return totalGameScore;
        }
        public static int RequestValue(int[][] allScore, int requestedFrame, int roll)
        {
            int requestedValue = 0;
            if (requestedFrame < allScore.Count() && allScore[requestedFrame] != null && roll < allScore[requestedFrame].Count())
            {
                requestedValue = allScore[requestedFrame][roll];
            }
            return requestedValue;
        }
        public static int FrameTen(int[][] allScore, int frameTen)
        {
            int frameTenScore = 0;
            
                frameTenScore = RequestValue(allScore, frameTen, 0) + RequestValue(allScore, frameTen, 1) + RequestValue(allScore, frameTen, 2);
            
            return frameTenScore;
        }
        public static int FrameNineBonus(int[][] allScore, int frameTen)
        {
            int nineBonus = 0;
            if(RequestValue(allScore, frameTen - 1, 0) + RequestValue(allScore, frameTen - 1, 1) != 10)
            {
                nineBonus = RequestValue(allScore, frameTen - 1, 0) + RequestValue(allScore, frameTen - 1, 1);
            }
            if (RequestValue(allScore, frameTen - 1, 0) == 10 && RequestValue(allScore, frameTen, 0) == 10 && RequestValue(allScore, frameTen, 1) == 10)
            {
                nineBonus = RequestValue(allScore, frameTen - 1, 0) + RequestValue(allScore, frameTen, 0) + RequestValue(allScore, frameTen, 1);
            }
            return nineBonus;
        }
    }
}