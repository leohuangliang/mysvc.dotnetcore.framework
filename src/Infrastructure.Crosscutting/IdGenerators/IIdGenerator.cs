namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerators
{
    public interface IIdGenerator
    {
        /// <summary>
        /// 获取id
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        long NextId(int? workId = null);
    }
}