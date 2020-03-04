using entities._base;
using System;
using System.Linq;
using Xunit;
using util.test;

namespace model.entities.test
{
    public class CheckEntities
    {
        /// <summary>
        /// Check for a good entity structure
        /// </summary>
        [Fact]
        public void Introspection()
        {
            var types = typeof(IEntity).Assembly.GetTypes()
                                        .Where(x => !x.IsAbstract && x.IsClass)
                                        .ToList();

            foreach (var item in types)
            {
                Assert.True(item.Test().CheckDefaultConstructor());
                
                Assert.True(item.Test().CheckHasProps());

                Assert.True(item.Test().CheckNameAttributes());
                Assert.True(item.Test().CheckNameMethods());
                Assert.True(item.Test().CheckNameProps());
            }
        }
    }
}
