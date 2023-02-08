
using MediatR;

namespace MySvc.Framework.Domain.Core.DomainEvents
{
    /// <summary>
    /// 领域事件接口
    /// </summary>
    public interface IDomainEvent  : INotification
    {

    }
}