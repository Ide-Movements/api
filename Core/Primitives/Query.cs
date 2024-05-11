using MediatR;

namespace Core.Primitives;

public abstract class Query<IParams, TResult> : IRequest<TResult>
{
  public IParams Params { get; set; }

  public Query (IParams _params)
  {
    Params = _params;
  }
}

public abstract class Query<IParams, TResult, TAggregateId> : Query<IParams, TResult>
{
  public TAggregateId? AggregateId { get; set; } = default;

  public Query (IParams _params, TAggregateId? aggregateId) : base(_params)
  {
    AggregateId = aggregateId;
  }
}
