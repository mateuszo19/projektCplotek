namespace ConsoleApp6
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static ulong OpComparisonEQ;
        static int[] TestVector;
        const int NIter = 10;
        static bool IsPresent_LinearTim(int[] Vector, int Number)
        {
            for (int i = 0; i < Vector.Length; i++)
                if (Vector[i] == Number) return true;
            return false;
        }
        static bool IsPresent_LinearInstr(int[] Vector, int Number)
        {
            for (int i = 0; i < Vector.Length; i++)
            {
                OpComparisonEQ++;
                if (Vector[i] == Number) return true;
            }
            return false;
        }
        static bool IsPresent_BinaryTim(int[] Vector, int Number)
        {
            int Left = 0, Right = Vector.Length - 1, Middle;
            while (Left <= Right)
            {
                Middle = (Left + Right) / 2;
                if (Vector[Middle] == Number) return true;
                else if (Vector[Middle] > Number) Right = Middle - 1;
                else Left = Middle + 1;
            }
            return false;
        }
        static bool IsPresent_BinaryInstr(int[] Vector, int Number)
        {
            int Left = 0, Right = Vector.Length - 1, Middle;
            while (Left <= Right)
            {
                OpComparisonEQ++;
                Middle = (Left + Right) / 2;
                if (Vector[Middle] == Number) return true;
                else
                {
                    OpComparisonEQ++;
                    if (Vector[Middle] > Number) Right = Middle - 1;
                    else Left = Middle + 1;
                }
            }

            return false;
        }
        static void Main()
        {
            Console.WriteLine("Size\t|LMaxI\t|LMaxT\t|BMaxI\t|BMaxT\t|LAvgI\t|LAvgT\t|BAvgI\t|BAvgT");
            for (uint ArraySize = 429496729; ArraySize <= 4294967295; ArraySize += 429496729)
            {

                Console.Write(ArraySize + "\t");
                TestVector = new int[ArraySize];
                for (int i = 0; i < TestVector.Length; i++)
                    TestVector[i] = i;
                LinearMaxInstr(); // liniowe max instrumentacja
                LinearMaxTim(); // liniowe max czas
                BinaryMaxInstr(); // binarne max instrumentacja
                BinaryMaxTim(); // binarne max czas
                //LinearAvgInstr(); // liniowe średnia instrumentacja
                //LinearAvgTim();  // liniowe średnia czas
                //BinaryAvgInstr(); // binarne średnia instrumentacja
                //BinaryAvgTim(); // binarne średnia czas
                Console.Write("\n");
            }

        }
        static void LinearMaxInstr()
        {
            OpComparisonEQ = 0;
            bool Present = IsPresent_LinearInstr(TestVector, TestVector.Length);
            Console.Write("|" + OpComparisonEQ);
        }
        static void LinearMaxTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_LinearTim(TestVector, TestVector.Length);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t|" + ElapsedSeconds.ToString("F4"));
        }
        static void BinaryMaxInstr()
        {
            OpComparisonEQ = 0;
            bool Present = IsPresent_BinaryInstr(TestVector, TestVector.Length + 1);
            Console.Write("\t|" + OpComparisonEQ);
        }
        static void BinaryMaxTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_BinaryTim(TestVector, TestVector.Length + 1);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t|" + ElapsedSeconds.ToString("F4"));
        }
        /*static void LinearAvgInstr()
        {
            OpComparisonEQ = 0;
            bool Present;
            for (int i = 0; i < TestVector.Length; ++i)
                Present = IsPresent_LinearInstr(TestVector, TestVector.Length/2);
            Console.Write("\t" + ((double)OpComparisonEQ / (double)TestVector.Length).ToString("F1"));
        }
        static void LinearAvgTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                bool Present;
                long StartingTime = Stopwatch.GetTimestamp();
                for (int i = 0; i < (TestVector.Length); ++i)
                    Present = IsPresent_LinearInstr(TestVector, TestVector.Length);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t|" + ElapsedSeconds.ToString("F4"));
        }
        static void BinaryAvgInstr()
        {
            OpComparisonEQ = 0;
            bool Present = IsPresent_BinaryInstr(TestVector, (int)Math.Log(TestVector.Length + 1, 2));
            Console.Write("\t|" + ((double)OpComparisonEQ / (double)TestVector.Length).ToString("F1"));
        }*/



    }
}



