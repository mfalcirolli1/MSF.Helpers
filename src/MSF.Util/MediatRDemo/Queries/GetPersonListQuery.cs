using MediatR;
using MSF.Util.MediatRDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.MediatRDemo.Queries
{
    public class GetPersonListQuery : IRequest<List<PersonModel>>
    {

    }
}
