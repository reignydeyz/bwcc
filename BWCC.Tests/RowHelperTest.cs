using BWCC.Business;
using Xunit;

namespace BWCC.Tests
{
    public class RowHelperTest
    {
        private readonly RowHelper _rowHelper;

        public RowHelperTest()
        {
            _rowHelper = new RowHelper();
        }

        [Theory]
        [InlineData(1, "a")]
        [InlineData(26, "z")]
        [InlineData(27, "aa")]
        [InlineData(40, "nn")]
        [InlineData(67, "ooo")]
        [InlineData(102, "xxxx")]
        [InlineData(52, "zz")]
        public void RowNumberToRowName_Success(int num, string expected)
        {
            // Arrange

            // Act
            var res = _rowHelper.RowNumberToRowName(num);

            // Assert
            Assert.Equal(expected, res);
        }
    }
}