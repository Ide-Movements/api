using Core.Primitives;

namespace Core.Commands.Devotional;

public record CreateDevotionalPayload(
  string Title,

  string Description,

  string Image
);

public class CreateDevotionalCommand : Command<CreateDevotionalPayload>
{
    public CreateDevotionalCommand(CreateDevotionalPayload payload) : base(payload)
    {
    }

    public override void ValidatePayload()
    {
    }
}
