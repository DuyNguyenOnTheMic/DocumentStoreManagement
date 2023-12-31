﻿using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.DocumentQueries
{
    /// <summary>
    /// Query class to find document by id
    /// </summary>
    /// <param name="Id"></param>
    public record GetDocumentByIdQuery(string Id) : IRequest<Document>;
}
