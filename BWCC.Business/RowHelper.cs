namespace BWCC.Business
{
    /// <summary>
    /// Helper for row-related business
    /// </summary>
    public class RowHelper
    {
        private readonly char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <summary>
        /// Converts number to Excel column name
        /// </summary>
        /// <param name="num">Integer number</param>
        /// <returns>Column name</returns>
        public string RowNumberToRowName(int num)
        {
            var multiplier = (int)(num / letters.Length) == 0 ? 1 : (int)(num / letters.Length) + (num > letters.Length && num % letters.Length != 0 ? 1 : 0);
            var characterIndex = 0;

            if (multiplier > 1)
                characterIndex = num - (letters.Length * (multiplier - 1)) - 1;
            else
                characterIndex = num - 1;
            
            var character = letters[characterIndex];

            return String.Concat(Enumerable.Repeat(character, multiplier));
        }
    }
}