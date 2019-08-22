namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class Entity
    {
        long _Id;
        public virtual long Id
        {
            get
            { return _Id; }
            set
            {
                _Id = value;
            }
        }
    }
}