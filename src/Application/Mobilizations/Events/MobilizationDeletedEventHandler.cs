using Domain.Entities.Mobilization.Events;
using MediatR;

namespace Application.Mobilizations.Events
{
    public class MobilizationDeletedEventHandler : INotificationHandler<MobilizationDeleted>
    {
        public Task Handle(MobilizationDeleted notification, CancellationToken cancellationToken)
        {
            // TODO get all the sections/questions and punches associated with this mob (and any other dependencies), and delete them
            throw new NotImplementedException();
        }
    }
}
