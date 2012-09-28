using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    public static class ExtAssert
    {
        /// <summary>
        /// Checks wether the current subject under test throws the exception of type TException
        /// </summary>
        /// <typeparam name="TException">The type of the expected exception</typeparam>
        /// <param name="sut">Method with the subject under test</param>
        public static void Throws<TException>(Action sut)
        {
            if (sut == null) throw new ArgumentNullException("sut");

            bool failed = false;
            var exceptionType = typeof(TException);

            try
            {
                sut();
            }
            catch (Exception ex)
            {
                failed = true;
                Assert.IsInstanceOfType(ex, exceptionType);
            }

            if (!failed)
                Assert.Fail("The current method did not fail. Expected exception: {0}", exceptionType.Name);
        }
    }
}
