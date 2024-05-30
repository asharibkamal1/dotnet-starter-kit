﻿using FSH.Framework.Core.Persistence;
using FSH.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Catalog.Application.Products.Creation.v1;
public sealed class CreateProductHandler(ILogger<CreateProductHandler> logger, IRepository<Product> repository) : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        await Task.FromResult(0).ConfigureAwait(false);
        var product = Product.Create(request.Name!, request.Description, request.Price);
        await repository.AddAsync(product, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("product created {ProductId}", product.Id);
        return new CreateProductResponse(product.Id);
    }
}
