using LanguageExt;
using Microsoft.Extensions.Logging;
using proiectpssc.Commands;
using proiectpssc.DataTypes;
using proiectpssc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using static proiectpssc.DataTypes.Cart;
using static proiectpssc.Events.PayCartEvent;
using static proiectpssc.Operations.CartOperation;


namespace proiectpssc.Workflows
{
    public class AddProductsToCartWorkflow
    {
        private readonly IClientsRepository clientsRepository;
        private readonly IProductsRepository productsRepository;
        private readonly ILogger<AddProductsToCartWorkflow> logger;

        public AddProductsToCartWorkflow(IClientsRepository clientsRepository, IProductsRepository productsRepository, ILogger<AddProductsToCartWorkflow> logger)
        {
            this.clientsRepository = clientsRepository;
            this.productsRepository = productsRepository;
            this.logger = logger;
        }

        public async Task<IPayCartEvent> ExecuteAsync(PaymentCartCommand command)
        {
            UnvalidatedCart unvalidatedCart = new UnvalidatedCart(command.InputProducts);

            var result = from clients in clientsRepository.TryGetExistingClients(unvalidatedCart.ProductList.Select(product => product.clientId))
                                          .ToEither(ex => new FailedCart(unvalidatedCart.ProductList, ex) as ICart)
                         from existingProducts in productsRepository.TryGetExistingProducts()
                                          .ToEither(ex => new FailedCart(unvalidatedCart.ProductList, ex) as ICart)
                         let checkClientExists = (Func<ClientID, Option<ClientID>>)(client => CheckClientExists((IEnumerable<ClientID>)clients, client))
                         from paidCart in ExecuteWorkflowAsync(unvalidatedCart, existingProducts, checkClientExists).ToAsync()
                         from _ in productsRepository.TrySaveProducts(paidCart)
                                          .ToEither(ex => new FailedCart(unvalidatedCart.ProductList, ex) as ICart)
                         select paidCart;

            return await result.Match(
                    Left: examGrades => GenerateFailedEvent(examGrades) as IPayCartEvent,
                    Right: paidCart => new PayCartSucceededEvent(paidCart.Csv, paidCart.PaidDate, paidCart)
                );
        }

        private async Task<Either<ICart, PaidCart>> ExecuteWorkflowAsync(UnvalidatedCart unvalidatedCart,
                                                                                          IEnumerable<CalculatedProduct> existingProduct,
                                                                                          Func<ClientID, Option<ClientID>> checkProductExists)
        {

            ICart cart = await ValidateCart(checkProductExists, unvalidatedCart);
            cart = CalculateCart(cart);
            cart = MergeProducts(cart, existingProduct);
            cart = PayCart(cart);

            return cart.Match<Either<ICart, PaidCart>>(
                whenUnvalidatedCart: unvalidatedCart => Left(unvalidatedCart as ICart),
                whenCalculatedCart: calculatedCart => Left(calculatedCart as ICart),
                whenInvalidatedCart: invalidatedCart => Left(invalidatedCart as ICart),
                whenFailedCart: failedCart => Left(failedCart as ICart),
                whenValidatedCart: validatedCart => Left(validatedCart as ICart),
                whenPaidCart: paidCart => Right(paidCart)
            );
        }

        private Option<ClientID> CheckClientExists(IEnumerable<ClientID> clients, ClientID clientRegistrationNumber)
        {
            if (clients.Any(s => s == clientRegistrationNumber))
            {
                return Some(clientRegistrationNumber);
            }
            else
            {
                return None;
            }
        }

        private PayCartFailedEvent GenerateFailedEvent(ICart cart) =>
            cart.Match<PayCartFailedEvent>(
                whenUnvalidatedCart: unvalidatedCart => new PayCartFailedEvent($"Invalid state {nameof(unvalidatedCart)}"),
                whenInvalidatedCart: invalidatedCart => new PayCartFailedEvent(invalidatedCart.Reason),
                whenValidatedCart: validatedCart => new PayCartFailedEvent($"Invalid state {nameof(validatedCart)}"),
                whenFailedCart: failedCart =>
                {
                    logger.LogError(failedCart.Exception, failedCart.Exception.Message);
                    return new PayCartFailedEvent(failedCart.Exception.Message);
                },
                whenCalculatedCart: calculatedCart => new PayCartFailedEvent($"Invalid state {nameof(calculatedCart)}"),
                whenPaidCart: paidCart => new PayCartFailedEvent($"Invalid state {nameof(paidCart)}"));
    }
}
