using System.Threading.Tasks;

namespace AoC.Core.Dep
{
    internal interface IShouldInit
    {
        Task Init();
    }
}
