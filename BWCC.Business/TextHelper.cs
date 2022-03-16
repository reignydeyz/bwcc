using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWCC.Business
{
    /// <summary>
    /// Helper for row-related business
    /// </summary>
    public class TextHelper
    {
        private readonly string[] _abbreviations = new string[] { "mr.", "e.g.", "mrs." };
        private readonly string _text;
        private readonly string[] _sentences;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="text">Text to be read</param>
        public TextHelper(string text)
        {
            _text = text.ToLower();
            _sentences = GetSentences();
        }

        /// <summary>
        /// Gets the times the word is repeated
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Number</returns>
        public int GetWordCount(string word)
        {
            return GetWordCount(word, _text);
        }

        /// <summary>
        /// Gets the times the word is repeated
        /// </summary>
        /// <param name="word"></param>
        /// <param name="text"></param>
        /// <returns>Number</returns>
        private int GetWordCount(string word, string text, bool multipleSentences = true)
        {
            var words = text.Split(' ');

            var count = 0;
            var index = 0;

            // Captures word in between the sentence
            while ((index = text.IndexOf($" {word.ToLower()} ", index)) != -1)
            {
                index += word.Length;
                count++;
            }

            // Captures word in between the sentence
            index = 0;
            while ((index = text.IndexOf($" {word.ToLower()}, ", index)) != -1)
            {
                index += word.Length;
                count++;
            }

            // Captures word at the end of sentence
            index = 0;
            while ((index = text.IndexOf($" {word.ToLower()}. ", index)) != -1)
            {
                index += word.Length;
                count++;
            }

            // Captures word at the start of text
            if (words[0] == word.ToLower())
                return count + 1;
            // Captures word at the end of text
            else if (words[words.Length - 1] == word.ToLower() || (words[words.Length - 1].Contains(word)))
                return count + 1;
            else
                return count;
        }

        /// <summary>
        /// Gets the sentences from the text
        /// </summary>
        /// <returns>Array of strings</returns>
        public string[] GetSentences()
        {
            /* The method was already called by the contructor
             * The method was made public for the purpose of UnitTest
             * */
            if (_sentences != null)
                return _sentences;

            var chars = _text.ToCharArray();
            var sentences = new List<string>();

            var index = 0;  // Index of the chars (array)
            var cursor = 0; // Start index of the text for Substring()
            foreach(var c in chars)
            {
                // Looks for period
                if (c == '.')
                {
                    // Checks if text has abbreviation
                    var hasAbbreviation = _abbreviations.Any(abbreviation => _text.Substring(index - (abbreviation.Length - 1), abbreviation.Length) == abbreviation);

                    if (!hasAbbreviation)
                    {
                        // Checks if text ends with space or is end of entire text
                        if (index + 1 > _text.Length - 1 || _text.Substring(index + 1, 1) == " ")
                        {
                            // Cursor needs to adjust depends on the position of the text
                            var text = _text.Substring(cursor == 0 ? 0 : cursor + 2, index - (cursor == 0 ? 0 : cursor + 2) + 1);
                            sentences.Add(text);
                            cursor = index;
                        }
                    }
                }

                index++;
            }

            return sentences.ToArray();
        }

        /// <summary>
        /// Finds the word from the list of sentences
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Array of numbers (Sentence index starts at 1)</returns>
        public int[] GetSentenceIndices(string word)
        {
            var indices = new List<int>();

            foreach(var sentence in _sentences)
            {
                var multiplier = GetWordCount(word, sentence, false);

                if (multiplier > 0)
                {
                    while (multiplier != 0)
                    {
                        indices.Add(Array.IndexOf(_sentences, sentence) + 1);
                        multiplier--;
                    }
                }
            }

            return indices.ToArray();
        }
    }
}
