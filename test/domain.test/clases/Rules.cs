using crossapp.rules;
using System.Threading.Tasks;

namespace domain.test.clases
{
    public class CorrectRule : IRule<TestUser>
    {
        public async Task<bool> Check(TestUser obj)
        {
            return await Task.Run(()=> true);
        }
    }

    public class WrongRule : IRule<TestUser>
    {
        public async Task<bool> Check(TestUser obj)
        {
            return await Task.Run(() => false);
        }
    }
}
