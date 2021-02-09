using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class FixMultiplication
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public static int FindDigit(string equation)
        {
            int EQN_LENGTH = equation.Length;
            bool isAPresent = false;
            bool isBPresent = false;
            bool isCPresent = false;
            int QmarkIndex = -1;                            // Index of symbol "?"
            int SmarkIndex = -1;                            // Index of symbol "*"
            int EmarkIndex = -1;                            // Index of symbol "="
            for(int i = 0; i < EQN_LENGTH; i++)
            {
                if(equation[i] == '?')
                {
                    QmarkIndex = i;
                }
                if(equation[i] == '*')
                {
                    SmarkIndex = i;
                }
                if(equation[i] == '=')
                {
                    EmarkIndex = i;
                }
            }
            // checking A & C present
            if(QmarkIndex > SmarkIndex && QmarkIndex < EmarkIndex) 
            {
                isAPresent = true;
                isCPresent = true;
            }
            // checking A and B present
            if(QmarkIndex > EmarkIndex)
            {
                isAPresent = true;
                isBPresent = true;
            }
            // checking B and C present
            if(QmarkIndex < SmarkIndex)
            {
                isBPresent = true;
                isCPresent = true;
            }

            int numA = 0;
            int numB = 0;
            int numC = 0;
            
            int ActualAnswer = -1;        // Storing actual number suppose to be
            int ans = -1;
            
            if(isAPresent && isBPresent)             // Extracting A and B from equation
            {
                numA = FixMultiplication.ExtractA(equation);
                numB = FixMultiplication.ExtractB(equation, SmarkIndex);
                ActualAnswer = numA * numB;
                string GivenStringC = "";
                int i = EmarkIndex + 1;
                while(i != equation.Length)
                {
                    GivenStringC = GivenStringC  + equation[i];
                    ++i;
                }
                string ActualAnswerString = ActualAnswer.ToString();
                if(ActualAnswerString.Length != GivenStringC.Length)
                    return ans;
                
                ans = FindMissingDigit(ActualAnswerString, GivenStringC);
                return ans;
            }
            else if(isBPresent && isCPresent)        // Extracting B and C from equation
            {
                numB = FixMultiplication.ExtractB(equation, SmarkIndex);
                numC = FixMultiplication.ExtractC(equation, EmarkIndex);
                if(numB != 0 && numC % numB != 0 )
                {
                    return ans;
                }
                if(numB != 0)
                    ActualAnswer = numC/numB;
                string GivenStringA = "";
                int i = 0;
                while(equation[i] != '*')
                {
                    GivenStringA = GivenStringA + equation[i];
                    ++i;
                }
                string ActualAnswerString = ActualAnswer.ToString();
                if(ActualAnswerString.Length != GivenStringA.Length)
                    return ans;
                ans = FindMissingDigit(ActualAnswerString, GivenStringA);
                return ans;
            }
            else if(isAPresent && isCPresent)            // Extracting A and C from Eqation
            {
                numA = FixMultiplication.ExtractA(equation);
                numC = FixMultiplication.ExtractC(equation, EmarkIndex);
                if(numA != 0 && numC % numA != 0)
                {
                    return ans;
                }
                if(numA != 0)
                    ActualAnswer = numC/numA;
                string GivenStringB = "";
                int i = SmarkIndex + 1;
                while(equation[i] != '=')
                {
                    GivenStringB = GivenStringB + equation[i];
                    ++i;
                }
                string ActualAnswerString = ActualAnswer.ToString();
                if(ActualAnswerString.Length != GivenStringB.Length)
                    return ans;
                
                ans = FindMissingDigit(ActualAnswerString, GivenStringB);
                return ans;
            }
            else
                return ans;

            throw new NotImplementedException();
        }
        static int ExtractA(string equation)
        {
            int i = 0;
            int numA = 0;
            while(equation[i] != '*')
            {
                numA = numA*10 + equation[i] - '0';
                ++i;
            }
            return numA;
        }

        static int ExtractB(string equation, int SmarkIndex)
        {
            int i = SmarkIndex + 1;
            int numB = 0;
            while(equation[i] != '=')
            {
                numB = numB*10 + equation[i] - '0';
                ++i;
            }
            return numB;
        }
        static int ExtractC(string equation, int EmarkIndex)
        {
            int i = EmarkIndex+1;
            int numC = 0;
            while(i != equation.Length)
            {
                numC = numC*10 + equation[i] - '0';
                ++i;
            }
            return numC;
        }
        // Finding Missing Digit from the eqaution
        static int FindMissingDigit(string ActualAnswerString, string GivenString)
        {
            int TempAns = -1;
            for(int j = 0; j < GivenString.Length; ++j)
            {
                if(GivenString[j] != ActualAnswerString[j] && GivenString[j] != '?')
                {
                    return TempAns;
                }
                if(GivenString[j] == '?')
                {
                    TempAns = ActualAnswerString[j] - '0';
                }
            }
            return TempAns;
        }
    }
}
