using BWCC.Business;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BWCC.Tests
{
    public class TextHelperTest
    {
        private readonly string _text;
        private readonly TextHelper _textHelper;

        public TextHelperTest()
        {
            _text = "This is what I learned from Mr. Jones about a paragraph. A paragraph is a group of words put together to form a group that is usually longer than a sentence. Paragraphs are often made up of several sentences. There are usually between three and eight sentences. Paragraphs can begin with an indentation, or by missing a line out, and then starting again. This makes it easier to see when one paragraph ends and another begins. In most organized forms of writing, such as essays, paragraphs contain a topic sentence. This topic sentence of the paragraph tells the reader what the paragraph will be about. Essays usually have multiple paragraphs that make claims to support a thesis statement, which is the central idea of the essay. Paragraphs may signal when the writer changes topics. Each paragraph may have a number of sentences, depending on the topic. I can now write topics on sports e.g. basketball, football, baseball and submit it to Mrs. Smith.";
            _textHelper = new TextHelper(_text);
        }

        [Theory]
        [InlineData("a", 9)]
        [InlineData("this", 3)]
        [InlineData("paragraph", 6)]
        [InlineData("paragraphs", 5)]
        [InlineData("on", 2)]
        [InlineData("e.g.", 1)]
        [InlineData("smith", 1)]
        public async Task GetWordCount_Success(string word, int expected)
        {
            // Arrange

            // Act
            var res = await Task.Run(() => _textHelper.GetWordCount(word));

            // Assert
            Assert.Equal(expected, res);
        }

        [Fact]
        public void GetSentences_Success()
        {
            // Arrange

            // Act
            var res = _textHelper.GetSentences();

            // Assert
            Assert.True(res.Length == 12);
        }

        [Fact]
        public void GetSentences_Last_Success()
        {
            // Arrange

            // Act
            var res = _textHelper.GetSentences().Last();

            // Assert
            Assert.True("i can now write topics on sports e.g. basketball, football, baseball and submit it to mrs. smith." == res);
        }

        [Theory]
        //[InlineData("This is what I learned from Mr. Jones about a paragraph.", "this", 1)]
        [InlineData("There are usually between three and eight sentences. ", "the", 0)]
        //[InlineData("a paragraph is a group of words put together to form a group that is usually longer than a sentence.", "a", 4)]
        public void GetWordCount_WithTextParam_Success(string text, string word, int expected)
        {
            // Arrange

            // Act
            var res = _textHelper.GetWordCount(word, text);

            // Assert
            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData("this", new int[] {1, 6, 8})]
        [InlineData("a", new int[] { 1, 2, 2, 2, 2, 5, 7, 9, 11 })]
        [InlineData("paragraph", new int[] { 1, 2, 6, 8, 8, 11 })]
        [InlineData("smith", new int[] { 12 })]
        public void GetSentenceIndices_Success(string word, int[] expected)
        {
            // Arrange

            // Act
            var res = _textHelper.GetSentenceIndices(word);

            // Assert
            Assert.True(res.Any());

            var index = 0;
            foreach(var r in res)
            {
                Assert.Equal(expected[index], res[index]);
                index++;
            }
        }
    }
}
