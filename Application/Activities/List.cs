using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{

    /*because this is logic inside our application project there is no way that our code on its own could get this information to our 
    API controller */
    public class List
    {
        // we tell our request this IRequest what object is going to be returning from this query and it's going to be a list of activites
        public class Query : IRequest<List<Activity>>
        {

            /*if we needed to send any data from the API such as an id of an activity or other information then
            then we will put them down as properties inside this class */
        }

        // first parameter A Query class the second parameter is what we return(return Type) from Handler class
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;

            public Handler(DataContext context, ILogger<List> logger)
            {
                _context = context;
                _logger = logger;
            }
            /* we pass our query which froms a request that we pass to our handler and then it returns the data that we specify 
             we are looking for insid this IRequest interface and eventuall we return the list of activites */
            public async Task<List<Activity>> Handle(Query request, CancellationToken token)
            {
                return await _context.Activities.ToListAsync();
            }

            // to apply cancellationToken
            // public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            // {
            //     try
            //     {
            //         for (var i = 0; i < 10; i++)
            //         {
            //             cancellationToken.ThrowIfCancellationRequested();

            //             //if we continue 
            //             await Task.Delay(1000, cancellationToken);
            //             _logger.LogInformation($"Task{i} has completed");
            //         }

            //     }
            //     catch (Exception)
            //     {
            //         _logger.LogInformation($"Task was cancelled");
            //     }
            //     return await _context.Activities.ToListAsync();
            // }
        }
    }
}