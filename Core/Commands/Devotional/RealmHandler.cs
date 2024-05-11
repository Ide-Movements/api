using MediatR;

namespace Core.Commands.Devotional;

public class DevotionalRealmHandler :
  IPipelineBehavior<CreateDevotionalCommand, Unit>
{
  public async Task<Unit> Handle (CreateDevotionalCommand command, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
  {
    // verify if user can create a devotional

    return await next();
  }
}
