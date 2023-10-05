﻿using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to create document
    /// </summary>
    public record CreateDocumentCommand(Document Document) : IRequest<Document>
    {
    }
}
