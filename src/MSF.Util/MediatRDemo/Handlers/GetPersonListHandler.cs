using MediatR;
using MSF.Util.MediatRDemo.DataAccess;
using MSF.Util.MediatRDemo.Models;
using MSF.Util.MediatRDemo.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSF.Util.MediatRDemo.Handlers
{
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, List<PersonModel>>
    {
        private readonly IDataAccess _data;

        public GetPersonListHandler(IDataAccess data)
        {
            _data = data;
        }

        public Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            var people = Task.FromResult(_data.GetPeople());
            return people;
        }
    }
}
