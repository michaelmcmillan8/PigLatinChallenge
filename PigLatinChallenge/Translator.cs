using System;
using System.Linq;

namespace PigLatinChallenge
{
    class Translator
    {
        const char LOWERCASE_A = 'a';
        const char LOWERCASE_Z = 'z';

        const char UPPERCASE_A = 'A';
        const char UPPERCASE_Z = 'Z';
        readonly string[] VOWELS = new string[] { "a", "e", "i", "o", "u" };

        const string YAY = "yay";
        const string AY = "ay";

        public string convertPhrase(string phrase)
        {
            string pigLatinPhrase = "";
            string[] words = phrase.Split(' ');

            foreach (string word in words)
            {
                pigLatinPhrase += convertWord(word).Trim() + " ";
            }

            return pigLatinPhrase;
        }

        /**
         * Converts a word to pig latin.
         */
        string convertWord(string word)
        {
            bool isCapitalized;
            bool isAllCaps;
            bool hasVowels;

            string beginningPunctuation = "";
            string endingPunctuation = "";

            string pigLatinWord;

            // if the string doesn't have letters, then don't try to convert it to pig latin; just return it
            if (!checkIfHasLetters(word))
            {
                return word;
            }

            // Check the beginning of the string for punctuation, and truncate it
            while (true)
            {
                char firstChar = word.ToLower().Substring(0, 1).ToCharArray()[0];

                if (firstChar < LOWERCASE_A || firstChar > LOWERCASE_Z)
                {
                    beginningPunctuation += firstChar;
                    word = word.Substring(1);
                }
                else
                {
                    break;
                }
            }

            // Check the end of the string for punctuation, and truncate it
            while (true)
            {
                char lastChar = word.ToLower().Substring(word.Length - 1, 1).ToCharArray()[0];
                if (lastChar < LOWERCASE_A || lastChar > LOWERCASE_Z)
                {
                    endingPunctuation += lastChar;
                    word = word.Substring(0, word.Length - 1);
                }
                else
                {
                    break;
                }
            }

            // Check if the word is capitalized, all caps, and has vowels. This affects how the word is translated
            isCapitalized = checkIfCapitalized(word);
            isAllCaps = checkIfAllCaps(word);
            hasVowels = checkIfHasVowels(word);

            word = word.ToLower();

            string firstLetter = word.Substring(0, 1);

            // If the word has no vowels, then move the first letter of the word to the end and add "ay"
            if (!hasVowels)
            {
                word = word.Substring(1) + firstLetter;
                pigLatinWord = beginningPunctuation + word + AY + endingPunctuation;
            }
            // If the word begins with a vowel, then add "yay" to the end of the word
            else if (VOWELS.Contains(firstLetter))
            {
                pigLatinWord = beginningPunctuation + word + YAY + endingPunctuation;
            }
            // Otherwise, move consonants at the beginning of the word to the end until hitting a vowel, then add "ay" to the end of the word
            else
            {
                word = word.Substring(1) + firstLetter;

                while (true)
                {
                    string nextLetter = word.Substring(0, 1);

                    if (VOWELS.Contains(nextLetter))
                    {
                        break;
                    }
                    else
                    {
                        word = word.Substring(1) + nextLetter;
                    }
                }

                pigLatinWord = beginningPunctuation + word + AY + endingPunctuation;
            }

            // Capitalize the word as needed
            if (isAllCaps)
            {
                pigLatinWord = pigLatinWord.ToUpper();
            }
            else if (isCapitalized)
            {
                pigLatinWord = pigLatinWord.Substring(0, 1).ToUpper() + pigLatinWord.Substring(1);
            }

            return pigLatinWord;
        }

        private bool checkIfHasLetters(string word)
        {
            bool hasLetters = false;

            foreach (char character in word.ToLower())
            {
                if (character >= LOWERCASE_A && character <= LOWERCASE_Z)
                {
                    hasLetters = true;
                }
            }

            return hasLetters;
        }

        private bool checkIfCapitalized(string word)
        {
            char firstLetter = word.Substring(0, 1).ToCharArray()[0];

            if (firstLetter >= UPPERCASE_A && firstLetter <= UPPERCASE_Z)
            {
                return true;
            }

            return false;
        }

        private bool checkIfAllCaps(string word)
        {
            bool isAllCaps = true;

            foreach (char character in word)
            {
                if (character < UPPERCASE_A || character > UPPERCASE_Z)
                {
                    isAllCaps = false;
                    break;
                }
            }

            return isAllCaps;
        }

        private bool checkIfHasVowels(string word)
        {
            bool hasVowels = false;

            foreach (char character in word.ToLower())
            {
                if (VOWELS.Contains(character.ToString()))
                {
                    hasVowels = true;
                    break;
                }
            }

            return hasVowels;
        }
    }
}
