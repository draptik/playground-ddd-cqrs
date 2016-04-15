using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Simple.Contracts;

namespace Simple.SnapshotJob
{
    public interface ICustomerSnapshotJob
    {
        void Run();
    }

    public class CustomerSnapshotJob : ICustomerSnapshotJob
    {
        private readonly ICustomerRepository _repository;

        public CustomerSnapshotJob(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void Run()
        {
            while (true)
            {
                foreach (var id in GetIds())
                {
                    var customer = _repository.FindById(id);
                    var snapshot = customer.GetCustomerSnapshot();
                    _repository.SaveSnapshot(snapshot, customer);
                }

                var snapshotIntervallInSeconds = int.Parse(ConfigurationManager.AppSettings["SnapshotIntervallInSeconds"]);
                Thread.Sleep(TimeSpan.FromSeconds(snapshotIntervallInSeconds));
            }
        }

        private IEnumerable<Guid> GetIds()
        {
            return _repository.GetAllIds();
        }
    }
}