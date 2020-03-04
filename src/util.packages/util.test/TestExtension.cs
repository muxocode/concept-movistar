using System;
using System.Collections.Generic;
using System.Text;

namespace util.test
{
    public static class TestExtension
    {
        public static TestUtil Test(this Type type)
        {
            return new TestUtil(type);
        }
    }
}
