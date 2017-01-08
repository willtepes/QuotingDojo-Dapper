using quotingDojo2.Models;
using System.Collections.Generic;
namespace quotingDojo2.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}