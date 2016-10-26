using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordApplication
{
    class PasswordGenerator
    {
        private static int minPasswordLength;
        private static int maxPasswordLenth;

        public int MinPasswordLength
        {
            get { return minPasswordLength; }
            set { minPasswordLength = value;}
        }
        public int MaxPasswordLength
        {
            get { return maxPasswordLenth; }
            set { maxPasswordLenth = value;}
        }

        private static string passAlphabUpperCase="ABCDEFGHJKLMNPQRSTWXYZ";
        private static string passAlphabLowerCase="abcdefgijkmnopqrstwxyz";
        private static string passCharsNumeric= "23456789";
        private static string passCharsSpecial= "*$-+?_&=!%{}/";

        public string Generate()
        {
            return Generate(minPasswordLength, maxPasswordLenth);
        }
        public static string Generate (int length)
        {
            return Generate(length, length);
        }

        public static string Generate (int minLength, int maxLength)
        {
            if(minLength<=0 || maxLength <=0 || minLength>maxLength)
            {
                return null;
            }

            char[][] charGroups = new char[][]
            {
                passAlphabLowerCase.ToCharArray(),
                passAlphabUpperCase.ToCharArray(),
                passCharsNumeric.ToCharArray(),
                passCharsSpecial.ToCharArray()
                
            };

            int [] charsLeftInGroup=new int [charGroups.Length];
            for (int i=0; i<charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i]=charGroups[i].Length;
            }

            int [] leftGroupsOrder=new int [charGroups.Length];
            for (int i=0; i<leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i]=i;
            }

            byte[] randomBytes = new byte[4];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            int seed = BitConverter.ToInt32(randomBytes, 0);

            Random random = new Random(seed);

            char[] password = null;

            if(minLength<maxLength)
            {
                password = new char[random.Next(minLength, maxLength + 1)];
            }
            else
            {
                password = new char[minLength];
            }
            int nextPasswordCharIndex;
            int nextGroupIndex;
            int nextLeftGroupsOrderIndex;
            int lastUnprocessedCharIndex;
            int lastLeftGroupsOrderIndex = leftGroupsOrder.Length - 1;

            for (int i=0; i<password.Length; i++)
            {
                if (lastLeftGroupsOrderIndex == 0)
                    nextLeftGroupsOrderIndex = 0;
                else
                    nextLeftGroupsOrderIndex = random.Next(0, lastLeftGroupsOrderIndex);
                nextGroupIndex = leftGroupsOrder[nextLeftGroupsOrderIndex];
                lastUnprocessedCharIndex = charsLeftInGroup[nextGroupIndex] - 1;

                if (lastUnprocessedCharIndex == 0)
                    nextPasswordCharIndex = 0;
                else
                    nextPasswordCharIndex = random.Next(0, lastUnprocessedCharIndex + 1);

                password[i] = charGroups[nextGroupIndex][nextPasswordCharIndex];

                if (lastUnprocessedCharIndex == 0)
                    charsLeftInGroup[nextGroupIndex] = charGroups[nextGroupIndex].Length;
                else
                {
                    if (lastUnprocessedCharIndex!=nextPasswordCharIndex)
                    {
                        char temp = charGroups[nextGroupIndex][lastUnprocessedCharIndex];
                        charGroups[nextGroupIndex][lastUnprocessedCharIndex] = charGroups[nextGroupIndex][nextPasswordCharIndex];
                        charGroups[nextGroupIndex][nextPasswordCharIndex] = temp;
                    }
                    charsLeftInGroup[nextGroupIndex]--;
                }
            }
            return new string(password);
        }

    }
}
