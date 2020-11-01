using System;
namespace MeowWorld.Infrastructure.data
{
    public interface IDBinitializer
    {
        public void InitData(MEOWcontext ctx);
    }
}
